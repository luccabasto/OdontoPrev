# Documentação de Entrega Final

## Projeto: OdontoPrevAPI

### Integrantes do Grupo

| Nome Completo                  | RM       |
|-------------------------------|----------|
| Lucas Emanuel da Silva Basto  | RM553771 |
| Marcelo Mendes Galli         | RM553654 |
| Erick Lopes Silva            | RM553927 |


### 📚 Descrição Geral

API RESTful desenvolvida em ASP.NET Core para gerenciamento de clínicas odontológicas, com integração de IA (ML.NET) para recomendação de clínica baseada no perfil do paciente.

---

## 🔧 Tecnologias Utilizadas

* ASP.NET Core 8
* Entity Framework Core + Oracle
* ML.NET (classificação com SDCA Maximum Entropy)
* xUnit para testes automatizados
* Swagger para documentação interativa

---

## 🔹 Principais Endpoints

### `POST /api/recomendacao`

* **Descrição:** Recomenda uma clínica com base em especialidade, dia, hora e região
* **Body Exemplo:**

```json
{
  "Especialidade": "Ortodontia",
  "DiaSemana": "Segunda",
  "Hora": "08h",
  "Regiao": "Zona Sul"
}
```

* **Resposta:**

```json
{
  "clinicaRecomendada": "Clinica A"
}
```

### `GET /api/paciente`

* Lista todos os pacientes

### `GET /api/agendamento`

* Lista todos os agendamentos

---

## 📅 Time e Contribuição

* Estrutura preparada para entregas colaborativas
* Segue boas práticas de projeto, padrões de código e testes modulares

---

## 🚀 Instruções de Execução

1. Restaurar dependências:

   ```bash
   dotnet restore
   ```
2. Executar API:

   ```bash
   dotnet run --project OdontoPrevAPI
   ```
3. Acessar Swagger:

   * `https://localhost:5001/swagger`

---

## ✅ Requisitos Atendidos

| Requisito                                              | Status | Implementação                                                                        |
| ------------------------------------------------------ | ------ | ------------------------------------------------------------------------------------ |
| Integração com serviço externo (RESTful API)           | OK     | Controllers: Paciente, Agendamento, Clinica, Dentista                                |
| Testes unitários, de integração e de sistema com xUnit | OK     | Pasta `/Tests/` com múltiplos testes abrangendo entidades e endpoint de recomendação |
| Clean Code + SOLID (com explicação se solicitada)      | OK     | Divisão clara entre Controllers, Services, Models, Repositories, Interfaces          |
| ML.NET integrado e funcionalidade de IA generativa     | OK     | Classe `MLModelPrediction`, modelo `MLModel.zip`, `POST /api/recomendacao`           |
| Treinamento automático do modelo em `Program.cs`       | OK     | Verifica a existência de `MLModel.zip` e treina a partir do CSV se necessário        |
| Dataset de treino incluso para IA                      | OK     | Arquivo `Data/agendamentos_treino.csv`                                               |




