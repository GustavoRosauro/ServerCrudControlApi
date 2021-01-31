using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCrudControl.Commom
{
    public class VideoConversions
    {
        public static string RetornarVideoBase64(string caminho)
        {            
            using (FileStream file = new FileStream(caminho, FileMode.Open, FileAccess.Read))
            {                
                var bytes = new byte[file.Length];
                file.Read(bytes,0,(int)file.Length);
                return Convert.ToBase64String(bytes,0,bytes.Length);
            }
        }
    }
}
