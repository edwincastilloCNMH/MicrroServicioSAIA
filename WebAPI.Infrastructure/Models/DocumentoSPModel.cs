namespace WebAPI.Infrastructure.Models
{
    public class DocumentoSPModel
    {
        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string TituloAtribuido { get; set; }

        public int? FechaCreacionI1 { get; set; }
        public int? FechaCreacionI2 { get; set; }
        public int? FechaCreacionI3 { get; set; }

        public int? FechaCreacionF1 { get; set; }
        public int? FechaCreacionF2 { get; set; }
        public int? FechaCreacionF3 { get; set; }

        public string ObservacionesFechas { get; set; }
        public int? NivelDescripcion { get; set; }

        public string CantidadVolumenSop { get; set; }
        public string VolumenSoporte { get; set; }
        public int? Soporte { get; set; }

        public string NombreProductores { get; set; }
        public string AlcanceContenido { get; set; }
        public string TipoDocumental { get; set; }

        public string Personal { get; set; }
        public string Corporativos { get; set; }
        public string Tematicos { get; set; }
        public string Geograficos { get; set; }
        public string Cronologicos { get; set; }

        public string CondicionAcceso { get; set; }
        public string CondicionReproducci { get; set; }
        public string Idioma { get; set; }

        public string NotasContenido { get; set; }
        public string NotasGenerales { get; set; }
        public string ReglasNormas { get; set; }

        public DateTime? FechasDescripcion { get; set; }
        public string EstadoElaboracion { get; set; }
        public string IdiomaDescripcion { get; set; }

        public string Ruta { get; set; }
        public string DigitalFiles { get; set; }
    }
}
