using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YugiohCollection.Models
{
    public class Carta : Entity
    {
        public Guid DuelistaID { get; set; }
        public string Nome { get; set; }
        public TipoCarta Tipo { get; set; }
        public string Efeito { get; set; }
        public string Imagem { get; set; }
        public Duelista Duelista { get; set; }
    }
}
