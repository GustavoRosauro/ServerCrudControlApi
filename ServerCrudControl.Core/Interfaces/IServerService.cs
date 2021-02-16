using ServerCrudControl.Commom.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCrudControl.Core.Interfaces
{
    public interface IServerService
    {
        public void CadastrarServidor(ServidorDTO servidor);
        public void RemoverServidor(Guid id);
        public ServidorCustomDTO RetornarDadosServidor(Guid id);
        public string ValidarDisponibilidadeServidor(Guid id);
        IEnumerable<ServidorDTO> RetornarListaServidores();
    }
}
