using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rl.zone.Domain.Abstract
{
    public abstract class Color : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ColorId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }    
    }
}