using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YugiohCollection.Models
{
    public class Duelista : Entity
    {
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public IEnumerable<Carta> Cartas { get; set; }
    }
}
