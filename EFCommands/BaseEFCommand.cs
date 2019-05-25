using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands
{
    public abstract class BaseEFCommand
    {
        protected ProjectContext Context { get; set; }

        protected BaseEFCommand(ProjectContext context)
        {
            Context = context;
        }
    }
}
