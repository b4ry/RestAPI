using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace PortfolioApplication.Services.CQRS.Commands
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