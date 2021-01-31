using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ServerCrudControl.Commom;
using ServerCrudControl.Commom.DTO;
using ServerCrudControl.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServerCrudControlApi.Controllers
{
    /// <summary>
    /// Api responsável por gerenciar os vídeos
    /// </summary>
    [Route("api")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        /// <summary>
        /// Configuraçãoes de arquivos
        /// </summary>
        public IWebHostEnvironment _environment;
        private IConfiguration _configuration;
        /// <summary>
        /// Configurações da api de vídeos
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        public VideoController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }
        /// <summary>
        /// Api para cadastrar Vídeos
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="video"></param>       
        /// <returns></returns>
        [HttpPost("servers/{serverId}/videos")]
        public async Task<IActionResult> CadastrarVideo(Guid serverId, [FromForm] VideoDTO video)
        {
            string videoBase = "";
            string caminho = "";
            if (video.File != null)
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\videos\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\videos\\");
                }
                using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\videos\\" + video.File.FileName))
                {
                    await video.File.CopyToAsync(filestream);
                    filestream.Flush();
                    caminho = "\\videos\\" + video.File.FileName;
                }
            }
            videoBase = VideoConversions.RetornarVideoBase64(caminho);
            if (!string.IsNullOrEmpty(caminho.Trim()))
                System.IO.File.Delete(caminho);
            new VideoService(_configuration).CadastrarVideo(serverId, video, videoBase);
            return Ok("Vídeo inserido com sucesso!");
        }
        /// <summary>
        /// Api responsável por remover os vídeos inseridos na base
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        [HttpDelete("servers/{serverId}/videos/{videoId}")]
        public IActionResult RemoverVideo(Guid serverId, Guid videoId)
        {
            new VideoService(_configuration).DeletarVideo(videoId, serverId);
            return Ok("Removido com sucesso!");
        }
        /// <summary>
        /// Api responsável por retornar dados cadastrais do vídeo
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        [HttpGet("servers/{serverId}/videos/{videoId}")]
        public IActionResult RetornarDadosVideo(Guid serverId, Guid videoId)
        {
            var video = new VideoService(_configuration).RetornarDadosVideo(videoId, serverId);
            var objectView = new
            {
                Id = video.Id,
                IdServidor = video.IdServidor,
                Descricao = video.Descricao,
                SizeInBytes = Convert.FromBase64String(video.Binario).Length,
                DataCadastro = video.DataCadastro
            };
            return Ok(objectView);
        }
        /// <summary>
        /// Api responsável por disponibilizar binarios do vídeo
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        [HttpGet("servers/{serverId}/videos/{videoId}/binary")]
        public IActionResult RetornaBinarioVideo(Guid serverId, Guid videoId)
        {
            var binario = new VideoService(_configuration).RetornaBinariosBase64(videoId, serverId);
            return Ok(binario);
        }
        /// <summary>
        /// Api responsável por retornar lista de vídeos por servidor
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        [HttpGet("servers/{serverId}/videos")]
        public IActionResult RetornarTodosVideosServidor(Guid serverId)
        {
            var videos = new VideoService(_configuration).RetornarTodosVideosServidor(serverId);
            return Ok(videos);
        }
        /// <summary>
        /// API responsável por chamar a tarefa de reciclagem que é executada em segundo plano
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        [HttpGet("recycler/process/{days}")]
        public IActionResult ReciclarVideosAntigos(int days)
        {
            ExecuteBackGroundTask(days);
            return StatusCode(202);
        }
        /// <summary>
        /// API responsável por retornar a situação da execução da TASK
        /// </summary>
        /// <returns></returns>
        [HttpGet("recycler/status")]
        public IActionResult RetornarStatusTask()
        {
            var mensagem = new VideoService(_configuration).RetornarSituacaoTask();
            return Ok(mensagem);
        }
        /// <summary>
        /// Foi adicionado um delay de 50 segundos para poder realizar a simulação da execução da task
        /// </summary>
        /// <param name="dias"></param>
        private void ExecuteBackGroundTask(int dias)
        {
            new VideoService(_configuration).InicioProcessTask();
            Task t = Task.Run(() => 
            {
                Task.Delay(50000).Wait();
                new VideoService(_configuration).ReciclarVideos(dias);
                new VideoService(_configuration).FinalizarSituacaoTask();
            });                        
;        }
    }
}
