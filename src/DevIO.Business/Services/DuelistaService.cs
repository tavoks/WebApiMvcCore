using System;
using System.Linq;
using System.Threading.Tasks;
using Yugioh.Business.Intefaces;
using YugiohCollection.Models;

namespace Yugioh.Business.Services
{
    public class DuelistaService : BaseService, IDuelistaService
    {
        private readonly IDuelistaRepository _duelistaRepository;
        private readonly ICartaRepository _cartaRepository;


        public DuelistaService(
            IDuelistaRepository duelistaRepository,
            ICartaRepository cartaRepository)
        {
            _duelistaRepository = duelistaRepository;
            _cartaRepository = cartaRepository;
        }

        public void Dispose()
        {
            _duelistaRepository?.Dispose();
            _cartaRepository?.Dispose();
        }

        public async Task<bool> Adicionar(Duelista duelista)
        {
            await _duelistaRepository.Adicionar(duelista);
            return true;
        }

        public async Task<bool> Atualizar(Duelista duelista)
        {
            await _duelistaRepository.Atualizar(duelista);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            await _duelistaRepository.Remover(id);
            return true;
        }
    }
}