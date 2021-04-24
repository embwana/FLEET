using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FLEET.ViewModels
{
    public class LicenceViewModel
    {
        [Key]
        public int LicenceId { get; set; }

        [Required]
        [Display(Name = " Employee Number ")]
        [StringLength(50)]
        public string EmployeeNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Employee Name")]
        [StringLength(50)]
        public string EmployeeName { get; set; }

        [Required]
        [Display(Name = "Licence Number")]
        [StringLength(50)]
        public string LicenceNumber { get; set; }

        [Required]
        [Display(Name = "Issued Date")]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime IssuedDate { get; set; }

        [Required]
        [Display(Name = "Expired Date")]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime ExpiredDate { get; set; }

        [Required]
        [Display(Name = "Renewed Date")]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime RenewedDate { get; set; }

        [Required]
        [Display(Name = "Department")]
        [StringLength(50)]
        public string Department { get; set; }

        // add image file upload and download class

        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }
    }
}
