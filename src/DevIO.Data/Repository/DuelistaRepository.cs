using System;
using System.Threading.Tasks;
using Yugioh.Business.Intefaces;
using Yugioh.Data.Context;
using Microsoft.EntityFrameworkCore;
using YugiohCollection.Models;
using System.Linq;

namespace Yugioh.Data.Repository
{
    public class DuelistaRepository : Repository<Duelista>, IDuelistaRepository
    {
        public DuelistaRepository(ApiDbContext context) : base(context)
        {
        }

        public async Task<Duelista> ObterCartasDuelista(Guid id)
        {
            return await Db.Duelistas
                .AsNoTracking()
                .Include(d => d.Cartas)
                .FirstAsync(d => d.Id == id);
        }
    }
}