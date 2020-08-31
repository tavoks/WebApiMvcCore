using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YugiohCollection.Models;

namespace Yugioh.Business.Intefaces
{
    public interface ICartaRepository : IRepository<Carta>
    {
        Task<Carta> ObterCarta(Guid id);
    }
}