using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YugiohCollection.Models;
using YugiohCollection.ViewModels;

namespace Yugioh.Api.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Duelista, DuelistaViewModel>().ReverseMap();
            CreateMap<Carta, CartaViewModel>().ReverseMap();

            //CreateMap<Produto, ProdutoViewModel>()
            //    .ForMember(dest => dest.NomeFornecedor, opt => opt.MapFrom(src => src.Fornecedor.Nome));
        }
    }
}
