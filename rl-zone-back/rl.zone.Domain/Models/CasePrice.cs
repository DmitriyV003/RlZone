using rl.zone.Domain.Abstract;

namespace rl.zone.Domain.Models
{
    public class CasePrice : Price
    {
        public virtual Platform Platform { get; set; }
        public int PlatformId { get; set; }

        public virtual Case Case { get; set; }
        public int CaseId { get; set; }
    }
}