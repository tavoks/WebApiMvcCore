using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yugioh.Data.Context
{
    public class TokenContext : IdentityDbContext
    {
        public TokenContext(DbContextOptions<TokenContext> options) : base(options)
        {

        }
    }
}
