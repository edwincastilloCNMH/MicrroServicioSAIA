using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAPI.Infrastructure.Helpers;
using WebAPI.Infrastructure.Models;

namespace WebAPI.Infrastructure.Contexts
{
	public class WebAPIContext : DbContext
	{
		public IConfiguration configuration { get; set; }

        public DbSet<ConsultaModel> ConsultasModel { get; set; }
        public DbSet<DocumentoSPModel> DocumentosSPModel { get; set; }
        public WebAPIContext()
        {
			configuration = AppSettings.GetConfiguration();
		}

		public WebAPIContext(DbContextOptions<WebAPIContext> options) : base(options)
		{
			configuration = AppSettings.GetConfiguration();
		}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = configuration.GetConnectionString("SqlServerConnection");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ConsultaModel>().HasNoKey().ToView(null);
            modelBuilder.Entity<DocumentoSPModel>().HasNoKey().ToView(null);
        }
    }
}
