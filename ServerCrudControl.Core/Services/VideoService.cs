using Microsoft.Extensions.Configuration;
using ServerCrudControl.Commom;
using ServerCrudControl.Commom.DTO;
using ServerCrudControl.Infra.Model;
using ServerCrudControl.Infra.Repository;
using ServerCrudControl.Infra.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCrudControl.Core.Services
{
    public class VideoService
    {
        private IUnitOfWork _unitOfWork;
        public VideoService(IConfiguration configuration)
        {
            _unitOfWork = new UnitOfWork(Commom.BancoDados.SqlServer, configuration);
        }
        public void CadastrarVideo(Guid idServidor, VideoDTO video, string videobBase)
        {
            try
            {
                if (_unitOfWork.ServidorRepository.RetornarDadosServidor(idServidor).Count() == 0)
                {
                    throw new Exception("Esse servidor não é válido");
                }
                else
                {
                    var videoModel = new Video()
                    {
                        IdServidor = idServidor,
                        Descricao = video.Descricao,
                        Binario = videobBase,
                        DataCadastro = DateTime.Now
                    };
                    _unitOfWork.VideoRepository.InserirVideo(videoModel);
                    _unitOfWork.Commit(Commom.BancoDados.SqlServer);
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback(Commom.BancoDados.SqlServer);
                throw new Exception(ex.Message);
            }
        }
        public void DeletarVideo(Guid id, Guid serverId)
        {
            try
            {
                var servidores = _unitOfWork.ServidorRepository.RetornarDadosServidor(serverId);
                if (servidores.Count() == 0)
                    throw new ArgumentException($"Não há servidore com esse id {serverId}");
                var videos = _unitOfWork.VideoRepository.RetornarDadosVideo(id, serverId);
                if (videos.Count() == 0)
                    throw new ArgumentException($"Não há videos cadastrados para esse id {id}");
                _unitOfWork.VideoRepository.RemoverVideo(id);
                _unitOfWork.Commit(BancoDados.SqlServer);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback(BancoDados.SqlServer);
                throw new Exception(ex.Message);
            }
        }
        public VideoCustomDTO RetornarDadosVideo(Guid id, Guid serverId)
        {
            try
            {
                var servidores = _unitOfWork.ServidorRepository.RetornarDadosServidor(serverId);
                if (servidores.Count() == 0)
                    throw new ArgumentException($"Não há servidore com esse id {serverId}");
                var videos = _unitOfWork.VideoRepository.RetornarDadosVideo(id, serverId);
                if (videos.Count() == 0)
                    throw new ArgumentException($"Não há videos cadastrados para esse id {id}");
                _unitOfWork.Commit(BancoDados.SqlServer);
                var video = videos.First();
                return new VideoCustomDTO()
                {
                    Id = id,
                    IdServidor = serverId,
                    Descricao = video.Descricao,
                    Binario = video.Binario,
                    DataCadastro = video.DataCadastro
                };
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback(BancoDados.SqlServer);
                throw new Exception(ex.Message);
            }
        }
        public string RetornaBinariosBase64(Guid id, Guid serverId)
        {
            try
            {
                var servidores = _unitOfWork.ServidorRepository.RetornarDadosServidor(serverId);
                if (servidores.Count() == 0)
                    throw new ArgumentException($"Não há servidore com esse id {serverId}");
                var videos = _unitOfWork.VideoRepository.RetornarDadosVideo(id, serverId);
                if (videos.Count() == 0)
                    throw new ArgumentException($"Não há videos cadastrados para esse id {id}");
                _unitOfWork.Commit(BancoDados.SqlServer);
                var video = videos.First();
                return video.Binario;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback(BancoDados.SqlServer);
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<object> RetornarTodosVideosServidor(Guid serverId)
        {
            try
            {
                var videos = _unitOfWork.VideoRepository.RetornarTodosVideosServidor(serverId);
                _unitOfWork.Commit(BancoDados.SqlServer);
                var lista = from video in videos
                       select new 
                       {
                           Id = video.Id,
                           IdServidor = video.IdServidor,
                           Descricao = video.Descricao,
                           SizeInBytes = Convert.FromBase64String(video.Binario).Length,
                           DataCadastro = video.DataCadastro
                       };
                return lista;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback(BancoDados.SqlServer);
                throw new Exception(ex.Message);
            }
        }
        public void InicioProcessTask()
        {
            try
            {
                if (!_unitOfWork.VideoRepository.ValidarRegisotroTask())
                    _unitOfWork.VideoRepository.InserirExecucaoTask();
                else
                    _unitOfWork.VideoRepository.AtualizarExecucaoTask();
                _unitOfWork.Commit(BancoDados.SqlServer);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback(BancoDados.SqlServer);
                throw new Exception(ex.Message);
            }
        }
        public void ReciclarVideos(int dias)
        {
            try
            {                
                dias = dias - 1;
                var dataFiltro = DateTime.Now.Date.AddDays(-dias);
                _unitOfWork.VideoRepository.DeletarVideosDias(dataFiltro);
                _unitOfWork.Commit(BancoDados.SqlServer);
            }
            catch (Exception ex)   
            {
                _unitOfWork.Rollback(BancoDados.SqlServer);
                throw new Exception(ex.Message);
            }
            
        }
        public void FinalizarSituacaoTask()
        {
            try
            {
                _unitOfWork.VideoRepository.FinalizarExecucaoTask();
                _unitOfWork.Commit(BancoDados.SqlServer);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback(BancoDados.SqlServer);
                throw new Exception(ex.Message);
            }
        }
        public string RetornarSituacaoTask()
        {
            try
            {
                bool situacao = _unitOfWork.VideoRepository.RetornarSituacaoTask();
                _unitOfWork.Commit(BancoDados.SqlServer);
                return situacao ? "running" : "not running";
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback(BancoDados.SqlServer);
                throw new Exception(ex.Message);
            }
        }
    }
}
