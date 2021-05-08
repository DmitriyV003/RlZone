using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using rl.zone.Domain.Abstract;

namespace rl.zone.Domain.Models
{
    public class Platform : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlatformId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Link> Links { get; set; }
        public virtual ICollection<ItemPrice> ItemPrices { get; set; }
        public virtual ICollection<CasePrice> CasePrices { get; set; }
    }
}