using Application.Commands.GameClasses;
using Application.Dto;
using Application.Searches;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCommands.GameClasses
{
    public class EFGetGameClassesCommand : BaseEFCommand, IGetGameClassesCommand
    {
        public EFGetGameClassesCommand(ProjectContext context) : base(context)
        {
        }

        public IEnumerable<GameClassDto> Execute(GameClassSearch request)
        {
            var query = Context.GameClasses.AsQueryable();

            if (request.Name != null)
                query = query.Where(gc => gc.Name.ToLower().Contains(request.Name.ToLower()));

            return query.Select(gc => new GameClassDto
            {
                Id = gc.Id,
                Name = gc.Name,
                CreatedAt = gc.CreatedAt,
                UpdatedAt = gc.UpdatedAt
            });
        }
    }
}
