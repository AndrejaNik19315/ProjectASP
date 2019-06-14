using Application.Dto;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Genders
{
    public interface IEditGenderCommand : ICommand<GenderDto, int, int>
    {
    }
}
