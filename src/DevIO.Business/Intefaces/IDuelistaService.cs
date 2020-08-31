using System;
using System.Threading.Tasks;
using YugiohCollection.Models;

namespace Yugioh.Business.Intefaces
{
    public interface IDuelistaService : IDisposable
    {
        Task<bool> Adicionar(Duelista duelista);
        Task<bool> Atualizar(Duelista duelista);
        Task<bool> Remover(Guid id);
    }
}