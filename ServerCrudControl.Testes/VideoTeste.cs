using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using ServerCrudControl.Commom.DTO;
using ServerCrudControl.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCrudControl.Testes
{
    public class VideoTeste
    {
        /// <summary>
        /// Classe responsável por testes unitários relacionados a rotina dos vídeos
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
        public void CadastrarVideoTeste()
        {
            Guid id = Guid.Parse("5C1BDC2F-033C-42D8-92CC-996294175C46");
            new VideoService(_configuration).CadastrarVideo(id,new VideoDTO() {Descricao = "Video para teste" }, "sem vídeo");
            Assert.Pass();
        }
        [Test]
        public void DeletarVideoTeste()
        {
            bool naoProcesar = true;
            if (!naoProcesar)
            {
                Guid idServidor = Guid.Parse("5C1BDC2F-033C-42D8-92CC-996294175C46");
                Guid idVideo = Guid.Parse("1291F1FE-4E01-4BF8-B8AA-FB51F1158EB1");
                new VideoService(_configuration).DeletarVideo(idVideo,idServidor);
            }
            Assert.Pass();
        }
        [Test]
        public void RetornarDadosVideoTeste()
        {
            Guid idServidor = Guid.Parse("5C1BDC2F-033C-42D8-92CC-996294175C46");
            Guid idVideo = Guid.Parse("1291F1FE-4E01-4BF8-B8AA-FB51F1158EB1");
            new VideoService(_configuration).RetornarDadosVideo(idVideo,idServidor);
            Assert.Pass();
        }
        [Test]
        public void RetornarBinariosBase64Teste()
        {
            Guid idServidor = Guid.Parse("5C1BDC2F-033C-42D8-92CC-996294175C46");
            Guid idVideo = Guid.Parse("1291F1FE-4E01-4BF8-B8AA-FB51F1158EB1");
            var binario = new VideoService(_configuration).RetornaBinariosBase64(idVideo,idServidor);
            Assert.AreEqual(string.IsNullOrEmpty(binario), false);
        }
        [Test]
        public void RetornarTodosVideosServidorTeste()
        {
            Guid idServidor = Guid.Parse("5C1BDC2F-033C-42D8-92CC-996294175C46");
            new VideoService(_configuration).RetornarTodosVideosServidor(idServidor);
            Assert.Pass();
        }
        [Test]
        public void ValidarExecucaoTaskTeste()
        {
            new VideoService(_configuration).InicioProcessTask();
            new VideoService(_configuration).ReciclarVideos(1);
            new VideoService(_configuration).FinalizarSituacaoTask();
        }

    }
}
