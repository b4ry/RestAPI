using DivingApplication.Entities.Entity;

namespace DivingApplication.Services.CQRS.Commands
{
    public class AddDivingGearCommand : ICommand
    {
        public string Name { get; set; }

        public DivingGearType DivingGearType { get; set; }

        public AddDivingGearCommand(string name, DivingGearType divingGearType)
        {
            Name = name;
            DivingGearType = divingGearType;
        }
    }
}
