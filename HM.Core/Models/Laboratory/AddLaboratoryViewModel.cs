using System.ComponentModel.DataAnnotations;

namespace HM.Core.Models.Laboratory
{
    public class AddLaboratoryViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Name should be no more than 30 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Description should be no more than 100 characters.")]
        public string Description { get; set; }
    }
}
