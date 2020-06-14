using FluentValidation;

namespace SCA.Model.Entidades
{
    //https://fluentvalidation.net/start
    public class Area : Entity
    {
        public string Nome { get; set; }
    }
    public class AreaValidator : AbstractValidator<Area>
    {
        public AreaValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("É obrigatório informar o Nome");
        }
    }
}
