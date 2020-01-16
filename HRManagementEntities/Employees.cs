using System.ComponentModel.DataAnnotations;

namespace HRManagementEntities
{
    public class Employees
    {
       
        public int Id { get; set; }

        [Required, MaxLength(15)]
        public string Name { get; set; }

        [Required, MaxLength(15)]
        public string Surname { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        public virtual Departments Department { get; set; }


    }
}