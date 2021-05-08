using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rl.zone.Domain.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}