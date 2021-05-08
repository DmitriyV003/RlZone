using rl.zone.Domain.Models;

namespace rl.zone.Domain.Intermediary
{
    public class UserLink
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int LinkId { get; set; }
        public virtual Link Link { get; set; }
    }
}