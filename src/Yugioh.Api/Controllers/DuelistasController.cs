using System;
using System.Collections.Generic;
using System.IO;
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
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<DuelistaViewModel>> ObterPorId(Guid id)
        {
            var duelista = await ObterCartasDuelista(id);

            if (duelista == null) return NotFound();

            return duelista;
        }

        [HttpPost]
        public async Task<ActionResult<DuelistaViewModel>> Adicionar(DuelistaViewModel duelistaViewModel)
        {
            if (!ModelState.IsValid) return NotFound();

            var imgPrefixo = Guid.NewGuid() + "_" + duelistaViewModel.Imagem;
            if (!UploadArquivo(duelistaViewModel.ImagemUpload, imgPrefixo))
            {
                return duelistaViewModel;
            }

            duelistaViewModel.Imagem = imgPrefixo + duelistaViewModel.ImagemUpload;
            await _duelistaService.Adicionar(_mapper.Map<Duelista>(duelistaViewModel));

            return duelistaViewModel;
        }


        // POST: api/Duelistas
        //[HttpPost]
        //public async Task<ActionResult<DuelistaViewModel>> Adicionar(DuelistaViewModel duelistaViewModel)
        //{

        //    if (!ModelState.IsValid) return NotFound();

        //    await _duelistaService.Adicionar(_mapper.Map<Duelista>(duelistaViewModel));

        //    return duelistaViewModel;

        //}


        // PUT: api/Duelistas/5
        [HttpPut("{id:Guid}")]
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
        [HttpDelete("{id:Guid}")]
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

        private bool UploadArquivo(string arquivo, string imgNome)
        {
            if (string.IsNullOrEmpty(arquivo))
            {
                return false;
            }

            var imageDataByteArray = Convert.FromBase64String(arquivo);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imgNome);

            if (System.IO.File.Exists(filePath))
            {
                return false;
            }

            try
            {
                System.IO.File.WriteAllBytes(filePath, imageDataByteArray);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            
            return true;
        }
        private async Task<bool> UploadArquivoAlternativo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                
                return false;
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/app/demo-webapi/src/assets", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }
    }
}
