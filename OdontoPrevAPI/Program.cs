using Microsoft.EntityFrameworkCore;
using System.Reflection;
using OdontoPrevAPI.Data;
using OdontoPrevAPI.Repository;
using OdontoPrevAPI.Repository.Interface;
using OdontoPrevAPI.Services;

namespace ChallengeOdontoprevSprint3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var stringConexao = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))) (CONNECT_DATA=(SERVER=DEDICATED)(SID=ORCL)));User Id=Password=;";

            var modeloPath = "MLModel.zip";
            if (!File.Exists(modeloPath))
            {
                var csvPath = Path.Combine("Data", "agendamentos_treino.csv");
                if (File.Exists(csvPath))
                {
                    MLModelTrainer.TreinarModelo(csvPath);
                }
            }

            builder.Services.AddDbContext<Context>
                (options => options.UseOracle(stringConexao));

            builder.Services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddScoped<IPacienteRepository, RepositoryPaciente>();
            builder.Services.AddScoped<IAgendamentoRepository, RepositoryAgendamento>();
            builder.Services.AddScoped<IClinicaRepository, RepositoryClinica>();
            builder.Services.AddScoped<IDentistaRepository, RepositoryDentista>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
