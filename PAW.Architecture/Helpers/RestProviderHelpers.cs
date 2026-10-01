using System.Net.Http.Headers;
using System.Text;

namespace APW.Architecture.Helpers;

internal static class RestProviderHelpers
{
	internal static HttpClient CreateHttpClient(string endpoint)
	{
		var client = new HttpClient { BaseAddress = new Uri(endpoint) };
		client.DefaultRequestHeaders.Accept.Clear();
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		return client;
	}

	internal static StringContent CreateContent(string content) => new(content, Encoding.UTF8, "application/json");

	internal static async Task<string> GetResponse(HttpResponseMessage response)
	{
		response.EnsureSuccessStatusCode();
		return await response.Content.ReadAsStringAsync();
	}

	internal static Exception ThrowError(string endpoint, Exception ex) => new ApplicationException($"Error getting data from {endpoint}", ex);
}
