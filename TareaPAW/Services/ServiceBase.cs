namespace TareaPAW.Services
{
    public abstract class ServiceBase
    {
        private readonly string _baseUrl;
        private readonly string _apiController;

        protected ServiceBase(IConfiguration configuration, string apiController)
        {
            _baseUrl = configuration["ApiSettings:BaseUrl"]
                ?? throw new InvalidOperationException("Falta ApiSettings:BaseUrl en appsettings.json");
            _apiController = apiController;
        }

        protected string Endpoint => $"{_baseUrl.TrimEnd('/')}/api/{_apiController}/";
    }
}
