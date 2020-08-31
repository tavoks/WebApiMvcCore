using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Microsoft.EntityFrameworkCore;
using Yugioh.Business.Intefaces;
using Yugioh.Data.Context;
using YugiohCollection.Models;
using YugiohCollection.ViewModels;

namespace Yugioh.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuelistasController : ControllerBase
    {
        private readonly IDuelistaRepository _duelistaRepository;
        private readonly IDuelistaService _duelistaService;
        private readonly IMapper _mapper;

        public DuelistasController(IMapper mapper,
            IDuelistaRepository duelistaRepository,
            IDuelistaService duelistaService)
        {
            _mapper = mapper;
            _duelistaRepository = duelistaRepository;
            _duelistaService = duelistaService;
        }

        // GET: api/Duelistas
        [HttpGet]
        public async Task<IEnumerable<DuelistaViewModel>> ObterTodos()
        {
            var duelistas = _mapper.Map<IEnumerable<DuelistaViewModel>>(await _duelistaRepository.ObterTodos());
            return duelistas;
        }

        // GET: api/Duelistas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DuelistaViewModel>> ObterPorId(Guid id)
        {
            var duelista = await ObterCartasDuelista(id);

            if (duelista == null) return NotFound();

            return duelista;
        }

        // POST: api/Duelistas
        [HttpPost]
        public async Task<ActionResult<DuelistaViewModel>> Adicionar(DuelistaViewModel duelistaViewModel)
        {

            if (!ModelState.IsValid) return NotFound();

            await _duelistaService.Adicionar(_mapper.Map<Duelista>(duelistaViewModel));

            return duelistaViewModel;

        }


        // PUT: api/Duelistas/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DuelistaViewModel>> Atualizar(Guid id, DuelistaViewModel duelistaViewModel)
        {
            if (id != duelistaViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return NotFound();

            await _duelistaService.Atualizar(_mapper.Map<Duelista>(duelistaViewModel));

            return duelistaViewModel;
        }

        // DELETE: api/Duelistas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DuelistaViewModel>> Excluir(Guid id)
        {
            var duelistaViewModel = await ObterCartasDuelista(id);

            if (duelistaViewModel == null) return NotFound();

            await _duelistaService.Remover(id);

            return duelistaViewModel;
        }

        private async Task<DuelistaViewModel> ObterCartasDuelista(Guid id)
        {
            return _mapper.Map<DuelistaViewModel>(await _duelistaRepository.ObterCartasDuelista(id));
        }
    }
}
