using System.ComponentModel.DataAnnotations;

namespace Alga1.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$", ErrorMessage = "Characters are not allowed.")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$", ErrorMessage = "Characters are not allowed.")]
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

        [Range(0, 9999999999999999, ErrorMessage = "Alowed range is 0 ...9999999999999999")]
        [Display(Name = "Salary Gross")]
        [DataType(DataType.Currency)]
        public decimal SalaryGross => Models.SalaryGross.Gross(SalaryNet, ChildrenNo, RaisesChildrenAlone);


        [Display(Name = "Avatar")]
        public byte[] EmployeePhoto { get; set; }


    }
}