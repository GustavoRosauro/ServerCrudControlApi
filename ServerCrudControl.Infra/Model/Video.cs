using System;
using System.Collections.Generic;
using System.Text;

namespace ServerCrudControl.Infra.Model
{
    /// <summary>
    /// classe representa o cadastro de vídeos
    /// </summary>
    public class Video
    {
        /// <summary>
        /// Chave da tabela
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Descrição do vídeo
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Binário do vídeo 
        /// </summary>
        public string Binario { get; set; }
        /// <summary>
        /// Relacionamento do vídeo com  o servidor
        /// </summary>
        public Guid IdServidor { get; set; }
        /// <summary>
        /// Data de quando o vídeo foi cadastrado
        /// </summary>
        public DateTime? DataCadastro { get; set; }
    }
}
