using DivingApplication.Entities.Entity;
using DivingApplication.Services.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

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

        public async void Handle(AddDivingGearCommand command)
        {
            AccountSummarySet.Add(new DivingGear()
            {
                Name = command.Name,
                Id = Guid.NewGuid(),
                DivingGearType = command.DivingGearType
            }
            );

            await UnitOfWork.SaveAsync();
        }
    }
}