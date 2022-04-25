using System.ComponentModel.DataAnnotations;

namespace HM.Core.Models
{
    public class AddDiseaseViewModel
    {
        [Required]
        [Display(Name = "ICD")]
        public string ICD { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Disease Name")]
        public string Name { get; set; }
    }
}
