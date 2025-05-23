using Microsoft.ML;
using OdontoPrevAPI.Model;

namespace OdontoPrevAPI.Services
{
    public class MLModelPrediction
    {
        private static readonly string ModelPath = "MLModel.zip";
        private readonly PredictionEngine<AgendamentoData, AgendamentoPrediction> _engine;

        public MLModelPrediction()
        {
            var mlContext = new MLContext();
            var model = mlContext.Model.Load(ModelPath, out _);
            _engine = mlContext.Model.CreatePredictionEngine<AgendamentoData, AgendamentoPrediction>(model);
        }

        public string RecomendarClinica(AgendamentoInput entrada)
        {
            var dados = new AgendamentoData
            {
                Especialidade = entrada.Especialidade,
                DiaSemana = entrada.DiaSemana,
                Hora = entrada.Hora,
                Regiao = entrada.Regiao,
                Clinica = ""
            };

            var prediction = _engine.Predict(dados);
            return prediction.ClinicaRecomendada;
        }
    }
}
