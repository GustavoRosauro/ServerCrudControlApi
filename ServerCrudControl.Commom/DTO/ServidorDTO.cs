using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCrudControl.Commom.DTO
{
    public class ServidorDTO
    {
        /// <summary>
        /// Nome do servidor
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Endereço IP do Servidor
        /// </summary>
        public string EnderecoIP { get; set; }
        /// <summary>
        /// Porta para acesso
        /// </summary>
        public int PortaIP { get; set; }
    }
}
