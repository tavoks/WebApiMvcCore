using System;
using System.Threading.Tasks;
using YugiohCollection.Models;

namespace Yugioh.Business.Intefaces
{
    public interface ICartaService : IDisposable
    {
        Task<bool> Adicionar(Carta produto);
        Task<bool> Atualizar(Carta produto);
        Task<bool> Remover(Guid id);
    }
}