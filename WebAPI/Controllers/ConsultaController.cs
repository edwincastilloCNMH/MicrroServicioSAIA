using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.DTO;
using WebAPI.Application.UseCase.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/MicroOMEKA/[controller]")]
    public class ConsultaController : Controller
    {
        private readonly IConsultaUseCase _consultaUseCase;

        public ConsultaController(IConsultaUseCase consultaUseCase)
        {
            _consultaUseCase = consultaUseCase;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ConsultaBasica(ConsultaRequestDTO parametros)
        {
            var result = await _consultaUseCase.ConsultaBasica(parametros);
            if (!result.Succeeded)
                return BadRequest();

            return new JsonResult(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ConsultaBasicaAvanzada(ConsultaRequestDTO parametros)
        {
            var result = await _consultaUseCase.ConsultaAvanzada(parametros);
            if (!result.Succeeded)
                return BadRequest();

            return new JsonResult(result);
        }
    }
}
