using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yugioh.Business.Intefaces;
using Yugioh.Data.Context;
using YugiohCollection.Models;
using YugiohCollection.ViewModels;

namespace Yugioh.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartasController : ControllerBase
    {
        private readonly ICartaRepository _CartaRepository;
        private readonly ICartaService _Cartaservice;
        private readonly IMapper _mapper;

        public CartasController(IMapper mapper,
            ICartaRepository CartaRepository,
            ICartaService Cartaservice)
        {
            _mapper = mapper;
            _CartaRepository = CartaRepository;
            _Cartaservice = Cartaservice;
        }

        // GET: api/Cartas
        [HttpGet]
        public async Task<IEnumerable<CartaViewModel>> ObterTodos()
        {
            var Cartas = _mapper.Map<IEnumerable<CartaViewModel>>(await _CartaRepository.ObterTodos());
            return Cartas;
        }

        // GET: api/Cartas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartaViewModel>> ObterPorId(Guid id)
        {
            var Carta = await ObterCarta(id);

            if (Carta == null) return NotFound();

            return Carta;
        }

        // POST: api/Cartas
        [HttpPost]
        public async Task<ActionResult<CartaViewModel>> Adicionar(CartaViewModel CartaViewModel)
        {

            if (!ModelState.IsValid) return NotFound();

            await _Cartaservice.Adicionar(_mapper.Map<Carta>(CartaViewModel));

            return CartaViewModel;

        }


        // PUT: api/Cartas/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CartaViewModel>> Atualizar(Guid id, CartaViewModel CartaViewModel)
        {
            if (id != CartaViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return NotFound();

            await _Cartaservice.Atualizar(_mapper.Map<Carta>(CartaViewModel));

            return CartaViewModel;
        }

        // DELETE: api/Cartas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CartaViewModel>> Excluir(Guid id)
        {
            var CartaViewModel = await ObterCarta(id);

            if (CartaViewModel == null) return NotFound();

            await _Cartaservice.Remover(id);

            return CartaViewModel;
        }

        private async Task<CartaViewModel> ObterCarta(Guid DuelistaID)
        {
            return _mapper.Map<CartaViewModel>(await _CartaRepository.ObterCarta(DuelistaID));
        }
    }
}
