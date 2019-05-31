using Application.Commands.Genders;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands.Genders
{
    public class EFDeleteGenderCommand : BaseEFCommand, IDeleteGenderCommand
    {
        public EFDeleteGenderCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var gender = Context.Genders.Find(request);

            if ( gender == null)
                throw new EntityNotFoundException("Gender with that Id doesn't exist");

            Context.Genders.Remove(gender);
            Context.SaveChanges();
        }

        public void Execute(int request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
