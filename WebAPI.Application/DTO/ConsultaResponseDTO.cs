namespace WebAPI.Application.DTO
{
    public class ConsultaResponseDTO
    {
        public string Origen { get; set; }
        public List<ConsultaDTO> Respuesta { get; set; }
    }
}
