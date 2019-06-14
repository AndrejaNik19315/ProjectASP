using Application.Commands.Characters;
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
            var user = Context.Users.Find(request.UserId);

            if (user == null)
                throw new EntityNotFoundException("There is no User with that Id.");

            if (user.IsActive == false)
                throw new EntityNotActiveException("User must be active.");

            if (Context.Characters.Count(c => c.UserId == request.Id) >= 3)
                throw new EntityUnprocessableException("User cannot have more than 3 characters.");

            if (Context.Characters.Any(c => c.Name == request.Name))
                throw new EntityAlreadyExistsException("Character with that name already exists.");

            if (!(Context.Genders.Any(gen => gen.Id == request.GenderId)))
                throw new EntityUnprocessableException("There is no gender with that Id.");

            if(!(Context.GameClasses.Any(gc => gc.Id == request.GameClassId)))
                throw new EntityUnprocessableException("There is no gameclass with that Id.");

            if(!(Context.Races.Any(r => r.Id == request.RaceId)))
                throw new EntityUnprocessableException("There is no race with that Id.");

            var characterId = Context.Characters.Add(new Domain.Character
            {
                Name = request.Name,
                Level = request.Level,
                Funds = request.Funds,
                RaceId = request.RaceId,
                GenderId = request.GenderId,
                GameClassId = request.GameClassId,
                UserId = request.UserId
            }).Entity.Id;

            Context.Inventories.Add(new Domain.Inventory
            {
                CharacterId = characterId,
            });

            Context.SaveChanges();
        }
    }
}
