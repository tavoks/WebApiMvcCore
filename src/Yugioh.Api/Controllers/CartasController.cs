using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yugioh.Business.Intefaces;
using Yugioh.Data.Context;
using YugiohCollection.Models;
using YugiohCollection.ViewModels;

namespace Yugioh.Api.Controllers
{
    [Authorize]
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
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CartaViewModel>> ObterPorId(Guid id)
        {
            var Carta = await ObterCarta(id);

            if (Carta == null) return NotFound();

            return Carta;
        }

        // POST: api/Cartas
        //[HttpPost]
        //public async Task<ActionResult<CartaViewModel>> Adicionar(CartaViewModel CartaViewModel)
        //{

        //    if (!ModelState.IsValid) return NotFound();

        //    await _Cartaservice.Adicionar(_mapper.Map<Carta>(CartaViewModel));

        //    return CartaViewModel;

        //}

        [HttpPost]
        public async Task<ActionResult<CartaViewModel>> AdicionarAlternativo(CartaViewModel cartaViewModel)
        {
            if (!ModelState.IsValid) return NotFound();

            var imgPrefixo = Guid.NewGuid() + "_" + cartaViewModel.Imagem;
            if (!UploadArquivo(cartaViewModel.ImagemUpload, imgPrefixo))
            {
                return cartaViewModel;
            }

            cartaViewModel.Imagem = imgPrefixo + cartaViewModel.ImagemUpload;
            await _Cartaservice.Adicionar(_mapper.Map<Carta>(cartaViewModel));

            return cartaViewModel;
        }


        // PUT: api/Cartas/5
        [HttpPut("{id:Guid}")]
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
        [HttpDelete("{id:Guid}")]
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

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

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
