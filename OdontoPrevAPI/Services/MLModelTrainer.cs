using Microsoft.ML;
using OdontoPrevAPI.Model;

namespace OdontoPrevAPI.Services
{
    public class MLModelTrainer
    {
        private static readonly string ModelPath = "MLModel.zip";

        public static void TreinarModelo(string csvPath)
        {
            var mlContext = new MLContext();

            var data = mlContext.Data.LoadFromTextFile<AgendamentoData>(csvPath, hasHeader: true, separatorChar: ',');

            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(AgendamentoData.Clinica))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("Especialidade"))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("DiaSemana"))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("Hora"))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("Regiao"))
                .Append(mlContext.Transforms.Concatenate("Features", "Especialidade", "DiaSemana", "Hora", "Regiao"))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var model = pipeline.Fit(data);

            mlContext.Model.Save(model, data.Schema, ModelPath);
        }
    }
}
