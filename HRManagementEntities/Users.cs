using System.ComponentModel.DataAnnotations;

namespace HRManagementEntities
{
    public class Users
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        public bool RememberMe { get; set; }
    }
}