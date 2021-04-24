using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FLEET.Models
{
    public class COF
    {
        [Key]
        public int COFId { get; set; }
        [Required]
        [Display(Name = "COF Number")]
        [StringLength(50)]
        public string COFNumber { get; set; }

        [Required]
        [Display(Name = "Date Issued")]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime Issued { get; set; }

        [Required]
        [Display(Name = "Expiring Date")]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime Expired { get; set; }

        [Required]
        [Display(Name = "Remarks")]
        [StringLength(50)]
        public string Remarks { get; set; }
       
        //[Display(Name = "Fleet Category")]
        //public FleetCategory FleetCategory { get; set; }
        // public Insurance Insurance { get; set; }
        public ICollection<FleetCategory> FleetCategories { get; set; }
    }
}
