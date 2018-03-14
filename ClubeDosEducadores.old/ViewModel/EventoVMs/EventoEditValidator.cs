using FluentValidation;
using Model;
using System;
using System.Data.Entity;
using System.Linq;

namespace ViewModel.EventoVMs
{
    internal class EventoEditValidator : ViewModelValidator<Evento, EventoEditVM>
    {
        public EventoEditValidator()
        {
            RuleFor(a => a.Titulo).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.TipoEvento).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.Resumo).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(a => a.Descricao).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(a => a.Titulo).Must((model, titulo) =>
            {
                var data = model.DtEvento.Date;
                return !Data.Any(a => a.Titulo.ToLower() == titulo.ToLower() && a.TipoEvento == model.TipoEvento && a.Id != model.Id && DbFunctions.TruncateTime(a.DtEvento) == data);
            }).WithMessage("Evento já cadastrado");

            //When(a => !string.IsNullOrWhiteSpace(a.Url), () =>
            //{
            //    RuleFor(a => a.Url).Must(a =>
            //    {
            //        return Uri.IsWellFormedUriString(a, UriKind.RelativeOrAbsolute);
            //    }).WithMessage("Url inválida");
            //});
        }
    }
}
