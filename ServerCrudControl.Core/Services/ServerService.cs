using Microsoft.Extensions.Configuration;
using ServerCrudControl.Commom;
using ServerCrudControl.Commom.DTO;
using ServerCrudControl.Core.Interfaces;
using ServerCrudControl.Infra.Model;
using ServerCrudControl.Infra.Repository;
using ServerCrudControl.Infra.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerCrudControl.Core.Services
{
    public class ServerService:IServerService
    {
        private IUnitOfWork _unitOfWork;
        public ServerService(IConfiguration configuration)
        {
            _unitOfWork = new UnitOfWork(Commom.BancoDados.SqlServer,configuration);
        }
        public void CadastrarServidor(ServidorDTO servidor)
        {
            try
            {
                var servidorModel = new Servidor()
                {                
                    Nome = servidor.Nome,
                    EnderecoIP = servidor.EnderecoIP,
                    PortaIP = servidor.PortaIP
                };
                _unitOfWork.ServidorRepository.InserirServidor(servidorModel);
                _unitOfWork.Commit(Commom.BancoDados.SqlServer);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback(Commom.BancoDados.SqlServer);
                throw new Exception(ex.Message);
            }
        }
        public void RemoverServidor(Guid id)
        {
            try
            {
                _unitOfWork.ServidorRepository.DeletarServidor(id,"Servidor");
                _unitOfWork.Commit(Commom.BancoDados.SqlServer);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback(Commom.BancoDados.SqlServer);
                throw new Exception(ex.Message);
            }
        }
        public ServidorCustomDTO RetornarDadosServidor(Guid id)
        {
            try
            {
                var servidores = _unitOfWork.ServidorRepository.RetornarDadosServidor(id);
                _unitOfWork.Commit(Commom.BancoDados.SqlServer);
                if (servidores.Count() == 0)
                {
                    throw new ArgumentException($"Não foi encontrado servidor para esse id {id}");
                }
                else
                    return servidores.First();

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback(Commom.BancoDados.SqlServer);
                throw new Exception(ex.Message);
            }            
        }
        public string ValidarDisponibilidadeServidor(Guid id)
        {
            try
            {
                var servidores = _unitOfWork.ServidorRepository.RetornarDadosServidor(id);
                _unitOfWork.Commit(Commom.BancoDados.SqlServer);
                if (servidores.Count() == 0)
                {
                    throw new ArgumentException($"Servidor não encontrado por esse id {id}");
                }
                else
                {
                    var servidor = servidores.First();
                    return ValidateIPAddress.ValidarEnderecoIP(servidor.EnderecoIP,servidor.PortaIP);
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback(Commom.BancoDados.SqlServer);
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<ServidorDTO> RetornarListaServidores()
        {
            try
            {
                var servidores = _unitOfWork.ServidorRepository.RetornarListaServidores();
                _unitOfWork.Commit(BancoDados.SqlServer);
                return servidores;
            }
            catch(Exception ex)
            {
                _unitOfWork.Rollback(BancoDados.SqlServer);
                throw new Exception(ex.Message);
            }
        }
    }
}
