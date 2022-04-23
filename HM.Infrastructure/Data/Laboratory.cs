using System.ComponentModel.DataAnnotations;

namespace HM.Infrastructure.Data
{
    public class Laboratory
    {
        public Laboratory()
        {
            Operators = new List<Doctor>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public IEnumerable<Doctor> Operators { get; set; }
    }
}
