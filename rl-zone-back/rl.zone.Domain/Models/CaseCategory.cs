using System.Collections.Generic;
using rl.zone.Domain.Abstract;

namespace rl.zone.Domain.Models
{
    public class CaseCategory : Category
    {
        public virtual ICollection<Case> Cases { get; set; }
    }
}