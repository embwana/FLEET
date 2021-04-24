using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FLEET.Models
{
    public class Licence
    {
        [Key]
        public int LicenceId { get; set; }

        [Required]
        [Display(Name = "EmployeeNo")]
        [StringLength(50)]
        public string EmployeeNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Employee")]
        [StringLength(50)]
        public string EmployeeName { get; set; }

        [Required]
        [Display(Name = "LicenceNo")]
        [StringLength(50)]
        public string LicenceNumber { get; set; }

        [Required]
        [Display(Name = "Issued")]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime IssuedDate { get; set; }

        [Required]
        [Display(Name = "Expired")]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime ExpiredDate { get; set; }

        [Required]
        [Display(Name = "Renewed")]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime RenewedDate { get; set; }

        [Required]
        [Display(Name = "Department")]
        [StringLength(50)]
        public string Department { get; set; }

        
        [Display(Name = " Image")]
        [Required(ErrorMessage = "Please choose profile image")]
        public string ProfilePicture { get; set; }

        public ICollection<FleetCustodian> FleetCustodians { get; set; }

    }
}
