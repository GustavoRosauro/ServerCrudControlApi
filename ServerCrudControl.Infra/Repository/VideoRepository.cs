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
    public class VideoRepository:IVideoRepository
    {
        private SqlConnection _conn;
        private SqlTransaction _trans;
        public VideoRepository(SqlConnection conn, SqlTransaction trans)
        {
            _conn = conn;
            _trans = trans;
        }

        public void InserirVideo(Video video)
        {
            string sqlInsert = GenericQuerys.GerarInsertQuery(video,"Video");
            _conn.ExecuteScalar(sqlInsert,video,_trans);
        }
        public void RemoverVideo(Guid id)
        {
            string sqlDelete = GenericQuerys.GerarDeleteQuery(id,"Video");
            _conn.Execute(sqlDelete,null,_trans);
        }
        public IEnumerable<Video> RetornarDadosVideo(Guid id,Guid serverId)
        {
            var video = new Video() { Id = id, IdServidor = serverId };
            string sql = GenericQuerys.GerarSelectQuery(video,"Video","");
            return _conn.Query<Video>(sql, video, _trans);
        }
        public IEnumerable<Video> RetornarTodosVideosServidor(Guid serverId)
        {
            var video = new Video() { IdServidor = serverId };
            string sql = GenericQuerys.GerarSelectQuery(video, "Video", "ORDER BY DATACADASTRO DESC");
            return _conn.Query<Video>(sql, video, _trans);
        }
        public void DeletarVideosDias(DateTime data)
        {
            string sqlDelete = @"DELETE FROM VIDEO WHERE DataCadastro <= @data";
            _conn.Execute(sqlDelete,new {data = data },_trans);
        }
        public bool ValidarRegisotroTask()
        {
            string sql = "SELECT COUNT(1) FROM TASKMONITOR";
            return _conn.ExecuteScalar<bool>(sql,null,_trans);
        }
        public void InserirExecucaoTask()
        {
            string sqlInsert = @"INSERT INTO TASKMONITOR (SITUACAO, DATAEXECUCAO) VALUES (1,GETDATE())";
            _conn.Execute(sqlInsert,null,_trans);
        }
        public void AtualizarExecucaoTask()
        {
            string sqlUpdate = @"UPDATE TASKMONITOR SET SITUACAO = 1, DATAEXECUCAO = GETDATE()";
            _conn.Execute(sqlUpdate,null,_trans);
        }
        public void FinalizarExecucaoTask()
        {
            string sqlUpdate = @"UPDATE TASKMONITOR SET SITUACAO = 0, DATAEXECUCAO = GETDATE()";
            _conn.Execute(sqlUpdate,null,_trans);
        }
        public bool RetornarSituacaoTask()
        {
            string sql = @"SELECT COUNT(1) FROM TASKMONITOR WHERE SITUACAO = 1";
            return _conn.ExecuteScalar<bool>(sql, null, _trans);
        }
    }
}
