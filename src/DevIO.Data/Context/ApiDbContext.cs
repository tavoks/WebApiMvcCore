using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YugiohCollection.Models;

namespace Yugioh.Data.Context
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Duelista> Duelistas { get; set; }
        public DbSet<Carta> Cartas { get; set; }
    }
}