using Microsoft.ML.Data;

namespace OdontoPrevAPI.Model
{
    public class AgendamentoTreino
    {
        [LoadColumn(0)] public string Especialidade { get; set; }
        [LoadColumn(1)] public string DiaSemana { get; set; }
        [LoadColumn(2)] public string Hora { get; set; }
        [LoadColumn(3)] public string Regiao { get; set; }
        [LoadColumn(4)] public string Clinica { get; set; }
    }
}
