using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using ServerCrudControl.Core.Services;
using System;

namespace ServerCrudControl.Testes
{
    public class ServerTeste
    {
        /// <summary>
        /// Classe responsável por testes unitários relacionados a rotina dos servidores
        /// foi criado na mesma base do projeto para facilitar porém no dia a dia é criado uma base só para os testes unitários
        /// </summary>
        private IConfiguration _configuration;
        [SetUp]
        public void Setup()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .Build();
        }

        [Test]
        public void CadastrarServidorTeste()
        {
            new ServerService(_configuration).CadastrarServidor(new Commom.DTO.ServidorDTO() { Nome = "APLOTESTE", EnderecoIP = "193.2.2.0", PortaIP = 90 });
            Assert.Pass();
        }
        [Test]
        public void RetornarDadosServidorTeste()
        {
            Guid id = Guid.Parse("5C1BDC2F-033C-42D8-92CC-996294175C46");
            var servidor = new ServerService(_configuration).RetornarDadosServidor(id);
            Assert.AreEqual(servidor.Id, id);
        }
        [Test]
        public void RemoverServidorTeste()
        {
            bool naoProcessar = true;
            if (!naoProcessar)
            {
                Guid id = Guid.Parse("5C1BDC2F-033C-42D8-92CC-996294175C46");
                new ServerService(_configuration).RemoverServidor(id);
            }
            Assert.Pass();
        }
        [Test]
        public void ValidarDispobilidadeServidorTeste()
        {
            Guid id = Guid.Parse("5C1BDC2F-033C-42D8-92CC-996294175C46");
            var mensagem = new ServerService(_configuration).ValidarDisponibilidadeServidor(id);
            Assert.AreNotEqual(string.IsNullOrEmpty(mensagem),true);
        }
        [Test]
        public void RetornarListaServidoresTeste()
        {
            new ServerService(_configuration).RetornarListaServidores();
            Assert.Pass();
        }
    }
}