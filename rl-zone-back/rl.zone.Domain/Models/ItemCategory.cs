using System;
using System.Collections.Generic;
using rl.zone.Domain.Abstract;

namespace rl.zone.Domain.Models
{
    public class ItemCategory : Category
    {
        public virtual ICollection<Item> Items { get; set; }
    }
}