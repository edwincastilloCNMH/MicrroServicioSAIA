using Microsoft.Extensions.Configuration;

namespace WebAPI.Infrastructure.Helpers
{
	public class AppSettings
	{
		static private IConfiguration _configuration = null;

		static public IConfiguration GetConfiguration()
		{

			if (_configuration == null)
			{

				string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

				if (String.IsNullOrWhiteSpace(environment))
					environment = "Development";

				// Set up configuration sources.
				var builder = new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
					.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

				_configuration = builder.Build();
			}

			return _configuration;
		}
	}
}
