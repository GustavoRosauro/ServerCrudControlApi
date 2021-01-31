using Microsoft.Extensions.Configuration;
using ServerCrudControl.Commom;
using ServerCrudControl.Infra.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ServerCrudControl.Infra.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private IServidorRepository _servidorRepository;
        private IConfiguration _configuration;
        private IVideoRepository _videoRepository;
        private SqlConnection _conn;
        private SqlTransaction _trans;
        public UnitOfWork(BancoDados banco, IConfiguration configuration)
        {
            _configuration = configuration;
            if (banco == BancoDados.SqlServer)
            {
                AbrirConexaoSqlServer();
            }
        }
        private void AbrirConexaoSqlServer()
        {
            _conn = new SqlConnection(_configuration.GetConnectionString("SqlConn"));
            _conn.Open();
            _trans = _conn.BeginTransaction();
        }
        public void Commit(BancoDados banco)
        {
            if (banco == BancoDados.SqlServer)
            {
                if(_trans.Connection != null)
                    _trans.Commit();
            }
        }
        public void Rollback(BancoDados banco)
        {
            if (banco == BancoDados.SqlServer)
            {
                if(_trans.Connection != null)
                    _trans.Rollback();
            }
        }
        public IServidorRepository ServidorRepository
        {
            get
            {
                _servidorRepository = new ServidorRepository(_conn,_trans);
                return _servidorRepository;
            }
        }
        public IVideoRepository VideoRepository
        {
            get
            {
                _videoRepository = new VideoRepository(_conn,_trans);
                return _videoRepository;
            }
        }
    }
}
