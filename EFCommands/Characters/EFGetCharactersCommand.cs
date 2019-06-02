using Application.Commands.Characters;
using Application.Dto;
using Application.Searches;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.Characters
{
    public class EFGetCharactersCommand : BaseEFCommand, IGetCharactersCommand
    {
        public EFGetCharactersCommand(ProjectContext context) : base(context)
        {
        }

        public IEnumerable<CharacterDto> Execute(CharacterSearch request)
        {
            var query = Context.Characters.AsQueryable();

            if (request.Name != null) {
                query = query.Where(c => c.Name.ToLower().Contains(request.Name.ToLower()));
            }

            if (request.Level != null) {
                query = query.Where(c => c.Level.Equals(request.Level));
            }

            if (request.MinFunds != null && request.MaxFunds != null) {
                query = query.Where(c => c.Funds >= request.MinFunds && c.Funds <= request.MaxFunds);
            }

            return query.Select(u => new CharacterDto
            {
                Id = u.Id,
                Name = u.Name,
                Level = u.Level,
                Funds = u.Funds,
                GameClassId = u.GameClassId,
                GenderId = u.GenderId,
                RaceId = u.RaceId
            }).OrderBy(u => u.Id);
        }

        public IEnumerable<CharacterDto> Execute(CharacterSearch request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
