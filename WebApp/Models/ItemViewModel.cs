using Application.Dto.Items;
using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ItemViewModel
    {
        public Paged<FullItemDto> Items { get; set; }
    }
}
