using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApplicationAPI.Commands
{
    public interface IHandleCommand
    {
    }

    public interface IHandleCommand<T> : IHandleCommand where T : ICommand
    {
        void Handle(T command);
    }
}
