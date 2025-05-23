using Microsoft.AspNetCore.Mvc;
using OdontoPrevAPI.Model;
using OdontoPrevAPI.Services;

namespace OdontoPrevAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecomendacaoController : ControllerBase
    {

        /// <summary>
        /// Recomenda uma clínica com base nos dados do paciente.
        /// </summary>
        /// <param name="dados">Dados do agendamento (especialidade, dia, hora, região)</param>
        /// <returns>Nome da clínica recomendada</returns>
        [HttpPost]
        public IActionResult Recomendar([FromBody] AgendamentoInput dados)
        {
            var predictor = new MLModelPrediction();
            var clinica = predictor.RecomendarClinica(dados);
            return Ok(new { ClinicaRecomendada = clinica });
        }
    }
}
