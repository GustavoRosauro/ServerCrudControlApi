using ServerCrudControl.Commom.DTO;
using ServerCrudControl.Infra.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCrudControl.Infra.Repository.Interfaces
{
    public interface IServidorRepository
    {
        void InserirServidor(Servidor servidor);
        void DeletarServidor(Guid id, string tabela);
        IEnumerable<ServidorCustomDTO> RetornarDadosServidor(Guid id);
        IEnumerable<ServidorDTO> RetornarListaServidores();
    }
}
