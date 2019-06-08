using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Character : BaseEntity
    {
        public Character()
        {
            Inventory = new Inventory();
        }

        public string Name { get; set; }
        public int? Level { get; set; }
        public decimal? Funds { get; set; }
        public int GameClassId { get; set; }
        public GameClass GameClass { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        public int RaceId { get; set; }
        public Race Race { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Inventory Inventory { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
