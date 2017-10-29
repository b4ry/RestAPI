using DivingApplication.Entities.Entities;
using DivingApplication.Services.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace DivingApplication.Tests.Services.DatabaseContext
{
    public class TestDatabaseContext
    {

        [Fact]
        public void DbContextMustCreateDynamicallyAllModelEntitiesOnItsOwnCreation()
        {
            var builder = new DbContextOptionsBuilder<PortfolioApplicationDbContext>().UseInMemoryDatabase();
            var options = builder.Options;
            var portfolioApplicationDbContext = new PortfolioApplicationDbContext(options);
            var allModelEntitiesCreated = true;

            var modelEntities = typeof(BaseEntity).GetTypeInfo().Assembly.GetTypes().Where(x => x.GetTypeInfo().BaseType == typeof(BaseEntity));

            foreach (Type modelEntity in modelEntities)
            {
                var modelEntityTypeName = modelEntity.GetTypeInfo().FullName;
                var foundEntityType = portfolioApplicationDbContext.Model.FindEntityType(modelEntityTypeName);

                if (foundEntityType == null)
                {
                    allModelEntitiesCreated = false;
                    break;
                }
            }

            Assert.True(allModelEntitiesCreated, "Did not create all of the application's model entities");
        }
    }
}
