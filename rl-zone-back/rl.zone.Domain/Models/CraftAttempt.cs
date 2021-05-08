using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using rl.zone.Domain.Intermediary;

namespace rl.zone.Domain.Models
{
    public class CraftAttempt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttemptId { get; set; }
        public float Price { get; set; }
        public float Percent { get; set; }
        public string Result { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<CraftAttemptItem> CraftAttempItems { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}