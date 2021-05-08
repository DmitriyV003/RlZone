using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using rl.zone.Domain.Intermediary;

namespace rl.zone.Domain.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public bool IsAvailableInCraft { get; set; }
        public bool IsAvailableInCase { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<ItemPrice> ItemPrices { get; set; }
        public virtual ItemCategory Category { get; set; }
        public int CategoryId { get; set; }

        public virtual ICollection<OpenCase> OpenCases { get; set; }
        public virtual ICollection<CaseItem> CaseItems { get; set; }
        public virtual ICollection<ItemSale> ItemSales { get; set; }
        public virtual ICollection<CraftAttemptItem> CraftAttempItems { get; set; }
    }
}