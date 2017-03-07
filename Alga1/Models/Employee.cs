using System.ComponentModel.DataAnnotations;

namespace Alga1.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Salary NET")]
        [DataType(DataType.Currency)]
        public decimal SalaryNet { get; set; }

        [Required]
        [Range(0, 10)]
        [Display(Name = "Number of Children")]
        public int ChildrenNo { get; set; }

        [Display(Name = "Raises Children Alone")]
        public bool RaisesChildrenAlone { get; set; }


        [Display(Name = "Salary Gross")]
        [DataType(DataType.Currency)]
        public decimal SalaryGross => Models.SalaryGross.Gross(SalaryNet, ChildrenNo, RaisesChildrenAlone);


        [Display(Name = "Avatar")]
        public byte[] EmployeePhoto { get; set; }
    }
}