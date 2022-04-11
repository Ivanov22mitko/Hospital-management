using System.ComponentModel.DataAnnotations;

namespace HM.Infrastructure.Data
{
    public class Disease
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string ICD { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    }
}
