using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        
        [ForeignKey("Department")]
        public int DeptartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
