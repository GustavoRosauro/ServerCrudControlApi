using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCrudControl.Commom.DTO
{
    public class VideoDTO
    {
        /// <summary>
        /// Descrição do vídeo
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Caminho do vídeo que será convertido em base64
        /// </summary>
        public IFormFile File { get; set; }
    }
}
