using System.ComponentModel.DataAnnotations;

namespace Alga1.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Vardas { get; set; }

        [Required]
        [StringLength(50)]
        public string Pavarde { get; set; }

        [Required]
        [Display(Name = "Salary NET")]
        [DataType(DataType.Currency)]
        public decimal AlgaNet { get; set; }

        [Required]
        [Range(0, 10)]
        [Display(Name = "Number of Children")]
        public int VaikuSkaicius { get; set; }

        [Display(Name = "Raises Children Alone")]
        public bool AuginaVaikusVienas { get; set; }


        [Display(Name = "Salary Gross")]
        [DataType(DataType.Currency)]
        public decimal AlgaGross => Models.SalaryGross.Gross(AlgaNet, VaikuSkaicius, AuginaVaikusVienas);


        [Display(Name = "Avatar")]
        public byte[] AsmuoImage { get; set; }
    }
}