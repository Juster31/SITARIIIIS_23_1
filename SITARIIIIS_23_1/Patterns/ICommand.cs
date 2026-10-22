using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITARIIIIS_23_1.Patterns
{
    public interface ICommand
    {
        string Description { get; }
        void Execute();
        void Undo();
    }
}
