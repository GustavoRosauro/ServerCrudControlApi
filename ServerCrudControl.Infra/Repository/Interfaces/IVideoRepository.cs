using ServerCrudControl.Infra.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCrudControl.Infra.Repository.Interfaces
{
    public interface IVideoRepository
    {
        void InserirVideo(Video video);
        void RemoverVideo(Guid id);
        IEnumerable<Video> RetornarDadosVideo(Guid id, Guid serverId);
        IEnumerable<Video> RetornarTodosVideosServidor(Guid serverId);
        void DeletarVideosDias(DateTime data);
        bool ValidarRegisotroTask();
        void InserirExecucaoTask();

        void AtualizarExecucaoTask();
        void FinalizarExecucaoTask();
        bool RetornarSituacaoTask();
    }
}
