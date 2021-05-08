using rl.zone.Domain.Models;

namespace rl.zone.Domain.Intermediary
{
    public class CaseItem
    {
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int CaseId { get; set; }
        public virtual Case Case { get; set; }

        public int ItemWeight { get; set; }
    }
}