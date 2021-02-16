using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ServerCrudControl.Commom.DTO;
using ServerCrudControl.Core.Interfaces;
using ServerCrudControl.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerCrudControlApi.Controllers
{
    /// <summary>
    /// API de gerenciamento dos servidores
    /// </summary>
    [Route("api")]
    [ApiController]
    public class ServerController : ControllerBase
    {   
        /// <summary>
        /// parametro usado em todo o código
        /// </summary>
        private IServerService _server;
        /// <summary>
        /// injeção de dependencia no parametro mencionado acima
        /// </summary>
        /// <param name="server"></param>
        public ServerController(IServerService server)
        {
            this._server = server;
        }
        /// <summary>
        /// API responsável pelo cadastro de servidores
        /// </summary>
        /// <param name="servidor"></param>
        /// <returns></returns>
        [HttpPost("server")]
        public IActionResult CadastrarServidor([FromBody]ServidorDTO servidor)
        {

            _server.CadastrarServidor(servidor);
            return Ok("Criado com sucesso!");
        }
        /// <summary>
        /// API responsável pela remoção dos servidores
        /// </summary>
        /// <param name="serverId"></param>
        [HttpDelete("servers/{serverId}")]
        public IActionResult RemoverServidor(Guid serverId)
        {
            _server.RemoverServidor(serverId);
            return Ok("Removido com sucesso!");
        }
        /// <summary>
        /// Api responsável por retornar dados de um servidor
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        [HttpGet("servers/{serverId}")]
        public IActionResult RetornarServidor(Guid serverId)
        {
            var servidor = _server.RetornarDadosServidor(serverId);
            return Ok(servidor);
        }
        /// <summary>
        /// Api para Validar Disponbilidade do Servidor
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        [HttpGet("servers/aviable/{serverId}")]
        public IActionResult ValidarDisponibilidadeServidor(Guid serverId)
        {
            var mensagem = _server.ValidarDisponibilidadeServidor(serverId);
            if (mensagem.ToLower().Contains("sucesso"))
            {
                return Ok(mensagem);
            }
            else
                return NotFound(mensagem);
        }
        /// <summary>
        /// Api responsável por retornar lista de servidores cadastrados 
        /// </summary>
        /// <returns></returns>
        [HttpGet("servers")]
        public IActionResult RetornaListaServidores()
        {
            var servidores = _server.RetornarListaServidores();
            return Ok(servidores);
        }
    }
}
