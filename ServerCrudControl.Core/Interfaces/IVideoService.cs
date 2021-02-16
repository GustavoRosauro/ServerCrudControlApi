using ServerCrudControl.Commom.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCrudControl.Core.Interfaces
{
    public interface IVideoService
    {
        void CadastrarVideo(Guid idServidor, VideoDTO video, string videobBase);
        void DeletarVideo(Guid id, Guid serverId);
        VideoCustomDTO RetornarDadosVideo(Guid id, Guid serverId);
        string RetornaBinariosBase64(Guid id, Guid serverId);
        IEnumerable<object> RetornarTodosVideosServidor(Guid serverId);
        void InicioProcessTask();
        void ReciclarVideos(int dias);
        void FinalizarSituacaoTask();
        string RetornarSituacaoTask();
    }
}
