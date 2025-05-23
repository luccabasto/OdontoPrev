using Microsoft.ML.Data;

namespace OdontoPrevAPI.Model
{
    public class AgendamentoPrediction
    {
        [ColumnName("PredictedLabel")]
        public string ClinicaRecomendada { get; set; }
    }
}
