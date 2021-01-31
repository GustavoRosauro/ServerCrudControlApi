using Dapper;
using ServerCrudControl.Commom;
using ServerCrudControl.Commom.DTO;
using ServerCrudControl.Infra.Model;
using ServerCrudControl.Infra.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCrudControl.Infra.Repository
{
    public class ServidorRepository : IServidorRepository
    {
        private SqlConnection _conn;
        private SqlTransaction _trans;
        public ServidorRepository(SqlConnection conn, SqlTransaction trans)
        {
            _conn = conn;
            _trans = trans;
        }
        public void InserirServidor(Servidor servidor)
        {
            string InsertQuery = GenericQuerys.GerarInsertQuery(servidor,"SERVIDOR");
            _conn.ExecuteScalar(InsertQuery,servidor,_trans);
        }
        public void DeletarServidor(Guid id, string tabela)
        {
            string sqlDelete = GenericQuerys.GerarDeleteQuery(id,tabela);
            _conn.ExecuteScalar(sqlDelete,null,_trans);
        }
        public IEnumerable<ServidorCustomDTO>  RetornarDadosServidor(Guid id)
        {
            var servidor = new Servidor() { Id = id };
            string sql = GenericQuerys.GerarSelectQuery(servidor, "SERVIDOR", "ORDER BY NOME");
            return  _conn.Query<ServidorCustomDTO>(sql,servidor,_trans);            
        }
        public IEnumerable<ServidorDTO> RetornarListaServidores()
        {
            string sql = GenericQuerys.GerarSelectQuery(new Servidor(),"SERVIDOR","ORDER BY NOME");
            return _conn.Query<ServidorDTO>(sql,null,_trans);
        }
    }
}
