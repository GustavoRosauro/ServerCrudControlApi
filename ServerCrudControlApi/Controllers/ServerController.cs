using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ServerCrudControl.Commom.DTO;
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
        private IConfiguration _configuration;
        /// <summary>
        /// Carregar as configurações do sistema que estão no arquivo appsetings.json
        /// </summary>
        /// <param name="configuration"></param>
        public ServerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// API responsável pelo cadastro de servidores
        /// </summary>
        /// <param name="servidor"></param>
        /// <returns></returns>
        [HttpPost("server")]
        public IActionResult CadastrarServidor([FromBody]ServidorDTO servidor)
        {
            new ServerService(_configuration).CadastrarServidor(servidor);
            return Ok("Criado com sucesso!");
        }
        /// <summary>
        /// API responsável pela remoção dos servidores
        /// </summary>
        /// <param name="serverId"></param>
        [HttpDelete("servers/{serverId}")]
        public IActionResult RemoverServidor(Guid serverId)
        {
            new ServerService(_configuration).RemoverServidor(serverId);
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
            var servidor = new ServerService(_configuration).RetornaDadosServidor(serverId);
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
            var mensagem = new ServerService(_configuration).ValidarDisponibilidadeServidor(serverId);
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
            var servidores = new ServerService(_configuration).RetornarListaServidores();
            return Ok(servidores);
        }
    }
}
