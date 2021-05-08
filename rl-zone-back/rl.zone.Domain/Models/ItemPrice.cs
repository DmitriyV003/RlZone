using rl.zone.Domain.Abstract;

namespace rl.zone.Domain.Models
{
    public class ItemPrice : Price
    {
        public virtual Platform Platform { get; set; }
        public int  PlatformId { get; set; }

        public virtual Item Item { get; set; }
        public int ItemId { get; set; }
    }
}