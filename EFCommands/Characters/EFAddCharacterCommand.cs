﻿using Application.Commands.Characters;
using Application.Dto;
using Application.Exceptions;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Characters
{
    public class EFAddCharacterCommand : BaseEFCommand, IAddCharacterCommand
    {
        public EFAddCharacterCommand(ProjectContext context) : base(context)
        {
        }

        public void Execute(CharacterDto request)
        {
            if (Context.Users.Find(request.UserId) == null)
                throw new EntityNotFoundException("There is no User with that Id.");

            if (Context.Users.Find(request.UserId).IsActive == false)
                throw new EntityNotActiveException();

            if (Context.Characters.Any(c => c.Name == request.Name))
                throw new EntityAlreadyExistsException();

            if (!(Context.Genders.Any(gen => gen.Id == request.GenderId)))
                throw new EntityNotFoundException("There is no gender with that Id.");

            if(!(Context.GameClasses.Any(gc => gc.Id == request.GameClassId)))
                throw new EntityNotFoundException("There is no gameclass with that Id.");

            if(!(Context.Races.Any(r => r.Id == request.RaceId)))
                throw new EntityNotFoundException("There is no race with that Id.");


            //if (Context.Genders.Find(request.GenderId) == null ||
            //    Context.GameClasses.Find(request.GameClassId) == null ||
            //    Context.Races.Find(request.RaceId) == null)
            //    throw new EntityNotFoundException("One or more of the following is out of format or don't exist: GenderId, GameClassId or RaceId"); 

            //if(request.InventoryId != null) {

            //}

            Context.Characters.Add(new Domain.Character
            {
               Name = request.Name,
               Level = request.Level,
               Funds = request.Funds,
               RaceId = request.RaceId,
               GenderId = request.GenderId,
               GameClassId = request.GameClassId,
               InventoryId = request.InventoryId,
               UserId = request.UserId
            });

            Context.SaveChanges();
        }

        public void Execute(CharacterDto request, int id)
        {
            throw new NotImplementedException();
        }
    }
}