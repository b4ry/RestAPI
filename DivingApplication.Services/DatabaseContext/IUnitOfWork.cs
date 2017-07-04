using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DivingApplication.Services.DatabaseContext
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
