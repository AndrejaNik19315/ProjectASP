using Application.Dto.Items;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Items.WebApp
{
    public interface IGetItemWebCommand : ICommand<int, FullItemDto>
    {
    }
}
