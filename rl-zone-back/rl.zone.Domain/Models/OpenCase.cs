using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rl.zone.Domain.Models
{
    public class OpenCase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OpenCaseId { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }

        public virtual Item Item { get; set; }
        public int ItemId { get; set; }
        
        public virtual Case Case { get; set; }
        public int CaseId { get; set; }
        
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}