using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using rl.zone.Domain.Abstract;
using rl.zone.Domain.Intermediary;

namespace rl.zone.Domain.Models
{
    public class User : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public float Balance { get; set; }
        public string? Avatar { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? SecretPhrase { get; set; }

        public virtual ICollection<UserLink> UserLinks { get; set; }
        public virtual ICollection<ItemSale> ItemSales { get; set; }
        public virtual ICollection<OpenCase> OpenCases { get; set; }
        public virtual ICollection<CraftAttempt> CraftAttempts { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}