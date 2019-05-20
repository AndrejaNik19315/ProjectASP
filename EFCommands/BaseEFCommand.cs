using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands
{
    public class BaseEFCommand
    {
        protected ProjectContext Context { get; set; }

        public BaseEFCommand(ProjectContext context)
        {
            Context = context;
        }
    }
}
