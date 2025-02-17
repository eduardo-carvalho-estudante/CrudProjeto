﻿using CrudProjeto.Data;
using CrudProjeto.Models;
using CrudProjeto.Services;
using Dominio.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        //Injeção de dependência
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Obter lista de clientes.
        /// </summary>
        /// <response code="200">A lista foi obtida com sucesso.</response>
        /// <response code="500">Ocorreu um erro ao obter a lista de  clientes.</response>
        [HttpGet]
        public List<ClienteDto> Get()
        {
            return _clienteService.Obter();
        }


        /// <summary>
        /// Obter um cliente por ID.
        /// </summary>
        /// <param name="id">Id do cliente.</param>
        /// <response code="200">O cliente foi obtido com sucesso.</response>
        /// <response code="404">Não foi encontrado cliente com ID especificado.</response>
        /// <response code="500">Ocorreu um erro ao obter o cliente.</response>
        [HttpGet("{id}")]
        public ActionResult<ClienteDto> Get(int id)
        {

            var c = _clienteService.ObterPorId(id);

            if(c == null)
            {
                return NotFound();
            }

            return Ok(c);
        }


        /// <summary>
        /// Atualizar um cliente por ID.
        /// </summary>
        /// <param name="id">Id do cliente.</param>
        /// <param name="cliente">Modelo do usuário.</param>
        ///<response code="200">O cliente foi atualizado com sucesso.</response>
        /// <response code="204">O cliente foi atualizado com sucesso.</response>
        /// <response code="400">Não foi encontrado cliente com ID especificado.</response>
        /// <response code="500">Ocorreu um erro ao obter o cliente.</response>
        [HttpPut("{id}")]
        public ActionResult<ClienteDto> Put(int id, ClienteDto cliente)
        {

            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _clienteService.Atualizar(cliente);

            return NoContent();

        }


        /// <summary>
        /// Criar um cliente.
        /// </summary>
        /// <param name="cliente">Objeto cliente.</param>
        ///<response code="200">O cliente foi criado com sucesso.</response>
        /// <response code="201">O cliente foi criado com sucesso.</response>
        /// <response code="500">Ocorreu um erro ao obter o cliente.</response>
        [HttpPost]
        public ActionResult<ClienteDto> Post(ClienteDto cliente)
        {
            _clienteService.Criar(cliente);
            return CreatedAtAction("Get", new { id = cliente.Id }, cliente);
        }


        /// <summary>
        /// Deletar um cliente por ID.
        /// </summary>
        /// <param name="id">Id do cliente.</param>
        /// <response code="200">O cliente foi deletado com sucesso.</response>
        /// <response code="204">O cliente foi deletado com sucesso.</response>
        /// <response code="404">Não foi encontrado cliente com ID especificado.</response>
        /// <response code="500">Ocorreu um erro ao obter o cliente.</response>
        [HttpDelete("{id}")]
        public ActionResult<Cliente> Delete(int id)
        {
            var cliente = _clienteService.ObterPorId(id);
            if(cliente == null)
            {
                return NotFound();
            }

            _clienteService.Deletar(cliente);
            return NoContent();
        }
    }
}
