﻿using Application.Dto;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Races
{
    public interface IGetRaceCommand : ICommand<int, RaceDto>
    {
    }
}
