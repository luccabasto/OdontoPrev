﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdontoPrevAPI.Model;
using OdontoPrevAPI.Repository.Interface;

namespace OdontoPrevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicaController : ControllerBase
    {
        private readonly IClinicaRepository _IClinicaRepository;
        private static int _id = 0; 

        public ClinicaController(IClinicaRepository IClinicaRepository)
        {
            _IClinicaRepository = IClinicaRepository;
        }
        
        /// Retorna todas as clínicas cadastradas.
        /// <returns>Lista de clínicas.</returns>
        /// <response code="200">Listagem realizada com sucesso.</response>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return Ok(await _IClinicaRepository.Listar());
        }
        
        /// Retorna uma clínica específica pelo ID.
        /// <param name="id">ID da clínica.</param>
        /// <returns>Dados da clínica.</returns>
        /// <response code="200">Clínica encontrada com sucesso.</response>
        /// <response code="404">Clínica não encontrada.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            var clinica = await _IClinicaRepository.ObterPorId(id);
            if (clinica == null)
            {
                return NotFound(new { message = "Clínica não encontrada." });
            }
            return Ok(clinica);
        }
        
                /// Atualiza os dados de uma clínica existente.
        /// <param name="id">ID da clínica.</param>
        /// <param name="clinica">Objeto com os dados atualizados.</param>
        /// <returns>Clínica atualizada.</returns>
        /// <response code="200">Clínica atualizada com sucesso.</response>
        /// <response code="400">ID ou dados inválidos.</response>
        /// <response code="409">Erro de concorrência ao atualizar.</response>
        [HttpPut("atualizar/{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] Clinica clinica)
        {
            if (id != clinica.id_clinica)
            {
                return BadRequest(new { message = "O ID informado não corresponde à clínica." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            try
            {
                await _IClinicaRepository.Atualizar(clinica);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "Erro de concorrência ao atualizar a clínica." });
            }

            return Ok(new { message = "Clínica atualizada!", data = clinica });
        }
        
        /// Exclui uma clínica do sistema com base no ID.
        /// <param name="id">ID da clínica.</param>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Clínica excluída com sucesso.</response>
        /// <response code="404">Clínica não encontrada.</response>
        [HttpDelete("excluir/{id}")]
        public async Task<ActionResult> Excluir(int id)
        {
            var clinica = await _IClinicaRepository.ObterPorId(id);
            if (clinica == null)
            {
                return NotFound(new { message = "Clínica não encontrada." });
            }

            await _IClinicaRepository.Excluir(clinica);

            return Ok(new { message = "Clínica excluída com sucesso!" });
        }
        
        /// Adiciona uma nova clínica ao sistema.
        /// <param name="clinica">Objeto com os dados da clínica.</param>
        /// <returns>Clínica cadastrada com sucesso.</returns>
        /// <response code="200">Clínica cadastrada com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost("adicionar")]
        public async Task<ActionResult> Adicionar([FromBody] Clinica clinica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            clinica.id_clinica = ++_id;
            await _IClinicaRepository.Adicionar(clinica);

            return Ok(new { message = "Clínica cadastrada!", data = clinica });
        }
    }

}
