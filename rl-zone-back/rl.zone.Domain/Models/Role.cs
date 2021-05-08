using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using rl.zone.Domain.Abstract;

namespace rl.zone.Domain.Models
{
    public class Role : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}