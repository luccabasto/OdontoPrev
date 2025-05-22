﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdontoPrevAPI.Model;
using OdontoPrevAPI.Repository.Interface;

namespace OdontoPrevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DentistaController : ControllerBase
    {
        private readonly IDentistaRepository _IDentistaRepository;
        private static int _id = 0; 

        public DentistaController(IDentistaRepository IDentistaRepository)
        {
            _IDentistaRepository = IDentistaRepository;
        }
        
        /// Retorna todos os dentistas cadastrados no sistema.
        /// <returns>Lista de dentistas.</returns>
        /// <response code="200">Dentistas retornados com sucesso.</response>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return Ok(await _IDentistaRepository.Listar());
        }
        
        /// Retorna um dentista específico pelo ID.
        /// <param name="id">ID do dentista.</param>
        /// <returns>Dados do dentista.</returns>
        /// <response code="200">Dentista encontrado com sucesso.</response>
        /// <response code="404">Dentista não encontrado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            var dentista = await _IDentistaRepository.ObterPorId(id);
            if (dentista == null)
            {
                return NotFound(new { message = "Dentista não encontrado." });
            }
            return Ok(dentista);
        }
        
                /// Atualiza os dados de um dentista existente.
        /// <param name="id">ID do dentista.</param>
        /// <param name="dentista">Objeto contendo os novos dados do dentista.</param>
        /// <returns>Dentista atualizado.</returns>
        /// <response code="200">Dentista atualizado com sucesso.</response>
        /// <response code="400">ID inválido ou dados incorretos.</response>
        /// <response code="409">Erro de concorrência ao atualizar o dentista.</response>
        [HttpPut("atualizar/{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] Dentista dentista)
        {
            if (id != dentista.id_dentista)
            {
                return BadRequest(new { message = "O ID informado não corresponde ao do Dentista." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            try
            {
                await _IDentistaRepository.Atualizar(dentista);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "Erro de concorrência ao atualizar o Dentista." });
            }

            return Ok(new { message = "Dentista atualizado!", data = dentista });
        }
        
        /// Exclui um dentista do sistema com base no ID.
        /// <param name="id">ID do dentista.</param>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Dentista excluído com sucesso.</response>
        /// <response code="404">Dentista não encontrado.</response>
        [HttpDelete("excluir/{id}")]
        public async Task<ActionResult> Excluir(int id)
        {
            var dentista = await _IDentistaRepository.ObterPorId(id);
            if (dentista == null)
            {
                return NotFound(new { message = "dentista não encontrado." });
            }

            await _IDentistaRepository.Excluir(dentista);

            return Ok(new { message = "dentista excluído com sucesso!" });
        }
        
        /// Adiciona um novo dentista ao sistema.
        /// <param name="dentista">Objeto contendo os dados do dentista.</param>
        /// <returns>Dentista cadastrado.</returns>
        /// <response code="200">Dentista cadastrado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost("adicionar")]
        public async Task<ActionResult> Adicionar([FromBody] Dentista dentista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            dentista.id_dentista = ++_id;
            await _IDentistaRepository.Adicionar(dentista);

            return Ok(new { message = "Dentista cadastrado!", data = dentista });
        }
    }

}

