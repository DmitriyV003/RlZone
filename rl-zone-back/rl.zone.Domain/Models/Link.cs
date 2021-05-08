using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using rl.zone.Domain.Abstract;
using rl.zone.Domain.Intermediary;

namespace rl.zone.Domain.Models
{
    public class Link : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LinkId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        public virtual ICollection<UserLink> UserLinks { get; set; }

        public int PlatformId { get; set; }
        public virtual Platform Platform { get; set; }
    }
}