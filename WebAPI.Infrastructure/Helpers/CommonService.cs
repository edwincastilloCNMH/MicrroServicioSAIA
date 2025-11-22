using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace WebAPI.Infrastructure.Helpers
{
	public class CommonService
	{
		protected IConfiguration Configuration;


		protected enum TypeMethodtHttp
		{
			Get,
			Post,
			Put,
			Delete
		}

		public CommonService(IConfiguration configuration)
		{
			Configuration = configuration;

		}

		public async Task<string> GetRequestAsync(string url)
		{
			using (var httpClient = new HttpClient())
			{
				string response = string.Empty;
				return await httpClient.GetAsync(url).Result.Content.ReadAsStringAsync();
			}
		}

		protected async Task<string> PostContent<T>(string url, T objeto) where T : class, new()
		{
			string jsonObject = JsonConvert.SerializeObject(objeto);
			HttpContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
			return await GetReleases(url, method: (int)TypeMethodtHttp.Post, content: content);
		}

		protected async Task<string> PutContent<T>(string url, T objeto) where T : class, new()
		{
			string jsonObject = JsonConvert.SerializeObject(objeto);
			HttpContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
			return await GetReleases(url, method: (int)TypeMethodtHttp.Put, content: content);
		}

		protected async Task<string> PostMultipartContent(string url, MultipartFormDataContent content)
		{
			using (var client = new HttpClient())
			{
				var response = await client.PostAsync(url, content);
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadAsStringAsync();
			}
		}

		protected async Task<string> GetReleases(string url, int id = 0, int method = (int)TypeMethodtHttp.Get, HttpContent content = null)
		{
			using (var httpClient = new HttpClient())
			{
				string response = string.Empty;
				switch (method)
				{
					case 0:
						url = id == 0 ? url : url + id;

						response = await httpClient.GetAsync(url).Result.Content.ReadAsStringAsync();
						break;
					case 1:
						response = await httpClient.PostAsync(url, content).Result.Content.ReadAsStringAsync();
						break;
					case 2:
						response = await httpClient.PutAsync(url, content).Result.Content.ReadAsStringAsync();
						break;
					case 3:
						response = await httpClient.DeleteAsync(url).Result.Content.ReadAsStringAsync();
						break;
				}
				return response;
			}
		}
	}
}