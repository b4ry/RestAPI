﻿using DivingApplication.Entities.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace DivingApplication.Services.DatabaseContext
{
    public class DivingApplicationDbContext : IdentityDbContext<User, Role, string>
    {
        public DivingApplicationDbContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityMethod = typeof(ModelBuilder).GetMethods().FirstOrDefault(x => x.Name == "Entity" && x.IsGenericMethodDefinition);
            var entities = typeof(BaseEntity).GetTypeInfo().Assembly.GetTypes().Where(x => x.GetTypeInfo().BaseType == typeof(BaseEntity));

            foreach (var entity in entities)
            {
                var constructedMethod = entityMethod.MakeGenericMethod(entity);

                constructedMethod.Invoke(modelBuilder, null);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
