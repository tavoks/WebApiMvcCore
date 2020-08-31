using System;
using System.Threading.Tasks;
using Yugioh.Business.Intefaces;
using YugiohCollection.Models;

namespace Yugioh.Business.Services
{
    public class CartaService : BaseService, ICartaService
    {
        private readonly ICartaRepository _cartaRepository;


        public CartaService(ICartaRepository cartaRepository)
        {
            _cartaRepository = cartaRepository;
        }

        public void Dispose()
        {
            _cartaRepository?.Dispose();
        }


        public async Task<bool> Adicionar(Carta carta)
        {
            await _cartaRepository.Adicionar(carta);
            return true;
        }

        public async Task<bool> Atualizar(Carta carta)
        {
            await _cartaRepository.Atualizar(carta);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            await _cartaRepository.Remover(id);
            return true;
        }
    }
}