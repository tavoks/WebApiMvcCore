using Yugioh.Business.Intefaces;
using FluentValidation;
using FluentValidation.Results;
using YugiohCollection.Models;

namespace Yugioh.Business.Services
{
    public abstract class BaseService
    {

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if(validator.IsValid) return true;

            return false;
        }
    }
}