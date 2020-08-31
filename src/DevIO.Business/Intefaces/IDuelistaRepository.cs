using System;
using System.Threading.Tasks;
using YugiohCollection.Models;

namespace Yugioh.Business.Intefaces
{
    public interface IDuelistaRepository : IRepository<Duelista>
    {
        Task<Duelista> ObterCartasDuelista(Guid id);
    }
}