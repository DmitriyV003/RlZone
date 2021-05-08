using rl.zone.Domain.Models;

namespace rl.zone.Domain.Intermediary
{
    public class CraftAttemptItem
    {
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int CraftAttemptId { get; set; }
        public virtual CraftAttempt CraftAttempt { get; set; }
    }
}