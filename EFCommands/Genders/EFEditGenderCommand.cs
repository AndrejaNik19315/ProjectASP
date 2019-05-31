using Application.Commands.Genders;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Genders
{
    public class EFEditGenderCommand : BaseEFCommand, IEditGenderCommand
    {
        public EFEditGenderCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(GenderDto request, int id)
        {
            var gender = Context.Genders.Find(id);

            if (gender == null)
                throw new EntityNotFoundException("Gender with that Id doesn't exist");

            if (gender.Sex.ToLower() != request.Sex.ToLower())
                if (Context.Genders.Any(g => g.Sex.ToLower().Equals(request.Sex.ToLower())))
                    throw new EntityAlreadyExistsException("Sex already exists");

            gender.Sex = request.Sex;
            gender.UpdatedAt = DateTime.Now;

            Context.SaveChanges();
        }

        public void Execute(GenderDto request)
        {
            throw new NotImplementedException();
        }
    }
}
