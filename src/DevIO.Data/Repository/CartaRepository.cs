using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yugioh.Business.Intefaces;
using Yugioh.Data.Context;
using Microsoft.EntityFrameworkCore;
using YugiohCollection.Models;

namespace Yugioh.Data.Repository
{
    public class CartaRepository : Repository<Carta>, ICartaRepository
    {
        public CartaRepository(ApiDbContext context) : base(context) { }

        public async Task<Carta> ObterCarta(Guid CartaID)
        {
            return await Db.Cartas.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == CartaID);
        }
    }
}