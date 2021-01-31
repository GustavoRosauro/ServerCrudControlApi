using System;
using System.Collections.Generic;
using System.Text;

namespace ServerCrudControl.Infra.Model
{
    /// <summary>
    /// classe que representa o cadastro de servidores
    /// </summary>
    public class Servidor
    {
        /// <summary>
        /// Chave da tabela
        /// </summary>
        public Guid Id { get; set; }
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
