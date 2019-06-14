using Application.Commands.Genders;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCommands.Genders
{
    public class EFGetGenderCommand : BaseEFCommand, IGetGenderCommand
    {
        public EFGetGenderCommand(ProjectContext context) : base(context)
        {
        }

        public GenderDto Execute(int request)
        {
            var gender = Context.Genders.Find(request);

            if (gender == null)
                throw new EntityNotFoundException("Gender not found");

            return new GenderDto
            {
                Id = gender.Id,
                Sex = gender.Sex,
                CreatedAt = gender.CreatedAt,
                UpdatedAt = gender.UpdatedAt
            };
        }
    }
}
