namespace WebAPI.Domain.Entities
{
    public class ConsultaRequestEntity
    {
        public string PalabraClave { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Serie { get; set; }
        public string Descripcion { get; set; }
        public string Tema { get; set; }
        public string Fecha { get; set; }
        public string Lugar { get; set; }
        public string Documento { get; set; }
        public string Codigo { get; set; }
        public int Inicio { get; set; }
        public int Cantidad { get; set; }
    }
}
