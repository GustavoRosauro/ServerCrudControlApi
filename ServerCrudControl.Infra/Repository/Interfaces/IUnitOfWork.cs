using ServerCrudControl.Commom;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerCrudControl.Infra.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit(BancoDados banco);
        void Rollback(BancoDados banco);
        IServidorRepository ServidorRepository { get; }
        IVideoRepository VideoRepository { get; }
    }
}
