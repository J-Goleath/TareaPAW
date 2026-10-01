using System.Net.Http;

namespace PAW.Architecture.Extensions;

public static class HttpClientExtensions
{
	private static void AddDefaultRequestHeader(this HttpClient client, string name, string value)
	{
		var defaultHeaders = client.DefaultRequestHeaders;
		if (defaultHeaders.Contains(name))
			defaultHeaders.Remove(name);
		defaultHeaders.Add(name, value);
	}
}
