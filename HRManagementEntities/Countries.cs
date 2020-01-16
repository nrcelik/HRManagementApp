using System.ComponentModel.DataAnnotations;

namespace HRManagementEntities
{
    public class Countries
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(15)]
        public string Name { get; set; }
    
    }
}