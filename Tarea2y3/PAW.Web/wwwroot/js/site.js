// =====================================================================
//  site.js  -  JavaScript global de PAW.Web
//  Este archivo se carga en TODAS las páginas (lo incluye _Layout.cshtml).
// =====================================================================

// ---------------------------------------------------------------------
//  TIPO DE CAMBIO (Tarea 2)
//  - Se pide desde el NAVEGADOR (JavaScript), no desde el controller.
//  - Se guarda en caché en localStorage para no pedirlo en cada página.
// ---------------------------------------------------------------------

// URL oficial del Ministerio de Hacienda de Costa Rica.
const EXCHANGE_RATE_URL = "https://api.hacienda.go.cr/indicadores/tc";

// Plan B: endpoint propio de PAW.Web que devuelve el mismo JSON.
// Solo se usa si el navegador bloquea la llamada directa a Hacienda (CORS).
const EXCHANGE_RATE_FALLBACK_URL = "/ExchangeRate/Get";

// Llave con la que se guarda el dato en localStorage.
const EXCHANGE_RATE_CACHE_KEY = "paw.exchangeRate";

// Cuánto tiempo vale la caché: 1 hora (en milisegundos).
const EXCHANGE_RATE_CACHE_TTL = 60 * 60 * 1000;

/**
 * Lee el tipo de cambio guardado en caché.
 * Devuelve el objeto { savedAt, data } o null si no hay / está vencido.
 */
function readExchangeRateCache() {
    try {
        const raw = localStorage.getItem(EXCHANGE_RATE_CACHE_KEY);
        if (!raw) return null;

        const cached = JSON.parse(raw);
        const age = Date.now() - cached.savedAt;

        // Si ya pasó más de 1 hora, la caché "venció".
        return age < EXCHANGE_RATE_CACHE_TTL ? cached : null;
    } catch {
        // localStorage puede fallar (modo incógnito, bloqueado, JSON dañado).
        return null;
    }
}

/** Guarda el tipo de cambio en caché junto con la hora en que se guardó. */
function writeExchangeRateCache(data) {
    try {
        const entry = { savedAt: Date.now(), data: data };
        localStorage.setItem(EXCHANGE_RATE_CACHE_KEY, JSON.stringify(entry));
        return entry;
    } catch {
        return { savedAt: Date.now(), data: data };
    }
}

/** Hace la petición HTTP y devuelve el JSON ya convertido a objeto. */
async function fetchJson(url) {
    // GET simple, SIN headers personalizados: así el navegador no tiene
    // que hacer una petición "preflight" de CORS.
    const response = await fetch(url, { method: "GET" });
    if (!response.ok) {
        throw new Error("HTTP " + response.status + " en " + url);
    }
    return await response.json();
}

/**
 * MÉTODO PRINCIPAL: obtiene el tipo de cambio.
 *  1. Si hay caché vigente  -> la devuelve sin llamar a internet.
 *  2. Si no hay caché       -> llama a Hacienda (o al plan B) y guarda.
 *
 * @param {boolean} forceRefresh  true = ignorar la caché y pedirlo de nuevo.
 * @returns {Promise<{savedAt:number, data:object, fromCache:boolean}>}
 */
async function getExchangeRate(forceRefresh = false) {
    if (!forceRefresh) {
        const cached = readExchangeRateCache();
        if (cached) {
            return { ...cached, fromCache: true };
        }
    }

    let data;
    try {
        data = await fetchJson(EXCHANGE_RATE_URL);
    } catch (error) {
        console.warn("No se pudo llamar a Hacienda directo, usando plan B.", error);
        data = await fetchJson(EXCHANGE_RATE_FALLBACK_URL);
    }

    const entry = writeExchangeRateCache(data);
    return { ...entry, fromCache: false };
}

// ---------------------------------------------------------------------
//  Pintar el tipo de cambio en la tarjeta flotante (ExchangeRate.cshtml)
// ---------------------------------------------------------------------

/** Formatea un número como colones: 462.08 -> "₡462,08" */
function formatColones(value) {
    return "₡" + Number(value).toLocaleString("es-CR", {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    });
}

/** Pone un texto dentro del elemento que tenga ese id. */
function setText(id, text) {
    const element = document.getElementById(id);
    if (element) element.textContent = text;
}

/** Carga el tipo de cambio y lo muestra en la tarjeta. */
async function loadExchangeRate(forceRefresh = false) {
    const widget = document.getElementById("exchange-rate-widget");
    if (!widget) return; // esta página no tiene la tarjeta

    widget.classList.add("is-loading");
    setText("er-status", "Cargando…");

    try {
        const result = await getExchangeRate(forceRefresh);
        const tc = result.data;

        setText("er-dolar-compra", formatColones(tc.dolar.compra.valor));
        setText("er-dolar-venta", formatColones(tc.dolar.venta.valor));
        setText("er-euro-colones", formatColones(tc.euro.colones));
        setText("er-euro-dolares", "$" + Number(tc.euro.dolares).toFixed(4));
        setText("er-fecha", tc.dolar.venta.fecha);

        const hora = new Date(result.savedAt).toLocaleTimeString("es-CR", {
            hour: "2-digit", minute: "2-digit"
        });
        setText("er-status", (result.fromCache ? "Desde caché · " : "Actualizado · ") + hora);
        widget.classList.remove("has-error");
    } catch (error) {
        console.error("Error cargando el tipo de cambio:", error);
        setText("er-status", "No se pudo cargar");
        widget.classList.add("has-error");
    } finally {
        widget.classList.remove("is-loading");
    }
}

/** Botones de la tarjeta: refrescar y minimizar. */
function initExchangeRateWidget() {
    const widget = document.getElementById("exchange-rate-widget");
    if (!widget) return;

    // Recordar si el usuario la dejó minimizada.
    try {
        if (localStorage.getItem("paw.exchangeRate.collapsed") === "1") {
            widget.classList.add("is-collapsed");
        }
    } catch { /* sin localStorage, no pasa nada */ }

    document.getElementById("er-refresh")?.addEventListener("click", () => loadExchangeRate(true));

    document.getElementById("er-toggle")?.addEventListener("click", () => {
        widget.classList.toggle("is-collapsed");
        try {
            localStorage.setItem("paw.exchangeRate.collapsed",
                widget.classList.contains("is-collapsed") ? "1" : "0");
        } catch { }
    });

    loadExchangeRate();
}

// Cuando la página termina de cargar, arrancamos la tarjeta.
document.addEventListener("DOMContentLoaded", initExchangeRateWidget);
