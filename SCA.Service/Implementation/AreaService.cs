using FluentValidation.Results;
using SCA.Model.Entities;
using SCA.Repository.Interfaces;
using SCA.Service.Interfaces;
using System.Threading.Tasks;

namespace SCA.Service.Implementation
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository areaRepository;

        public AreaService(IAreaRepository areaRepository)
        {
            this.areaRepository = areaRepository;
        }

        public async Task<string> Teste()
        {
            Area area = new Area();

            AreaValidator validationRules = new AreaValidator();

            ValidationResult result = validationRules.Validate(area);
            if (result.IsValid)
                return await areaRepository.Teste(area);
            return string.Empty;
        }
    }
}
