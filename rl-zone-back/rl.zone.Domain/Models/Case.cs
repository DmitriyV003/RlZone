using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using rl.zone.Domain.Intermediary;

namespace rl.zone.Domain.Models
{
    public class Case
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CaseId { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public int? MaxOpens { get; set; }
        public int? CurrentOpens { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<CasePrice> CasePrices { get; set; }
        public int CategoryId { get; set; }
        public virtual CaseCategory Category { get; set; }

        public virtual ICollection<OpenCase> OpenCases { get; set; }
        public virtual ICollection<CaseItem> CaseItems { get; set; }
    }
}