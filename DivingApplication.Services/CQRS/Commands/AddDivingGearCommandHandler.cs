using DivingApplication.Entities.Entities;
using DivingApplication.Services.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DivingApplication.Services.CQRS.Commands
{
    public class AddDivingGearCommandHandler : IHandleCommand<AddDivingGearCommand>
    {
        private IUnitOfWork UnitOfWork { get; }
        private IDatabaseSet DatabaseSet { get; }
        private DbSet<DivingGear> AccountSummarySet { get; }

        public AddDivingGearCommandHandler(IDatabaseSet databaseSet, IUnitOfWork unitOfWork)
        {
            DatabaseSet = databaseSet;
            AccountSummarySet = DatabaseSet.Set<DivingGear>();
            UnitOfWork = unitOfWork;
        }

        public void Handle(AddDivingGearCommand command)
        {
            AccountSummarySet.Add(new DivingGear()
            {
                Name = command.Name,
                DivingGearType = command.DivingGearType
            }
            );

            UnitOfWork.SaveAsync();
        }
    }
}