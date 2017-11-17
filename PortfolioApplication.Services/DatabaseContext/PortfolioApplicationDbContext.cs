﻿using PortfolioApplication.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace PortfolioApplication.Services.DatabaseContext
{
    public class PortfolioApplicationDbContext : DbContext
    {
        public PortfolioApplicationDbContext(DbContextOptions<PortfolioApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityMethod = typeof(ModelBuilder).GetMethods().FirstOrDefault(x => x.Name == "Entity" && x.IsGenericMethodDefinition);
            var entities = Assembly.Load("PortfolioApplication.Entities").GetTypes().Where(x => x.GetTypeInfo().BaseType == typeof(BaseEntity));

            foreach (var entity in entities)
            {
                var constructedMethod = entityMethod.MakeGenericMethod(entity);

                constructedMethod.Invoke(modelBuilder, null);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
