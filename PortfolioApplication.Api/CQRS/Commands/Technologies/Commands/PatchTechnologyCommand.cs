using PortfolioApplication.Api.DataTransferObjects.Technologies;

namespace PortfolioApplication.Api.CQRS.Commands.Technologies.Commands
{
    public class PatchTechnologyCommand : IPatchTechnologyCommand
    {
        public PatchTechnologyDto PatchechnologyDto { get; }
            
        public PatchTechnologyCommand(PatchTechnologyDto patchTechnologyDto)
        {
            PatchechnologyDto = patchTechnologyDto;
        }

        //public override string ToString()
        //{
        //    return $"Patch Technology entity identified by: name = '{_name}', Position = '{PositionId}'" +
        //        $" with values: Company name = '{CompanyName}', Position = '{Position}'";
        //}
    }
}
