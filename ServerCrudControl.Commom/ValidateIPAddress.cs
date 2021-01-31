using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerCrudControl.Commom
{
    public class ValidateIPAddress
    {
        public static string ValidarEnderecoIP(string enderecoIP,int porta)
        {
            TcpClient tcpClient = new TcpClient();

            try
            {
                tcpClient.Connect(enderecoIP, porta);
                return "Conectado com sucesso!";
            }
            catch
            {
                return "Não foi possível conectar no servidor";
            }          
        }
    }
}
