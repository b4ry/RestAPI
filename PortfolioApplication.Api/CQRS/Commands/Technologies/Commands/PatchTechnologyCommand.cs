using PortfolioApplication.Api.DataTransferObjects.Technologies;

namespace PortfolioApplication.Api.CQRS.Commands.Technologies.Commands
{
    public class PatchTechnologyCommand : IPatchTechnologyCommand
    {
        public PatchTechnologyDto PatchTechnologyDto { get; }
            
        public PatchTechnologyCommand(PatchTechnologyDto patchTechnologyDto)
        {
            PatchTechnologyDto = patchTechnologyDto;
        }

        public override string ToString()
        {
            return $"Patch Technology entity identified by: name = '{PatchTechnologyDto.Name}'";
        }
    }
}
