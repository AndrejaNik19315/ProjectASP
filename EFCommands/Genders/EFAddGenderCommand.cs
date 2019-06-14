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
    public class EFAddGenderCommand : BaseEFCommand, IAddGenderCommand
    {
        public EFAddGenderCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(GenderDto request)
        {
            if (Context.Genders.Any(g => g.Sex.ToLower() == request.Sex.ToLower()))
                throw new EntityAlreadyExistsException("Sex already exists");

            Context.Genders.Add(new Domain.Gender
            {
                Sex = request.Sex
            });

            Context.SaveChanges();
        }
    }
}
