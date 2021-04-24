using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FLEET.Models
{
    public class FleetCategory
    {
        [Key]
        public int FleetCategoryId { get; set; }

        [Required]
        [Display(Name = "Number Plate")]
        [StringLength(50)]
        public string NumberPlate { get; set; }

        [Required]
        [Display(Name = "Model")]
        [StringLength(50)]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Year")]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime Year { get; set; }

        [Required]
        [Display(Name = "Cost")]
        public double Cost { get; set; }

        [Required]
        [Display(Name = "Tag Number")]
        [StringLength(50)]
        public string TagNumber { get; set; }

        [Required]
        [Display(Name = "Mileage")]
        [StringLength(50)]
        public string Mileage { get; set; }


        public int? COFId { get; set; }
        [Display(Name = "Number")]
        public COF COFNumber { get; set; }

        public int? DepartmentId { get; set; }
        [Display(Name = "Department")]
        public Department Department { get; set; }

        public int? InsuranceId { get; set; }
        [Display(Name = "Insurance Number")]
        public Insurance InsuranceNumber { get; set; }

        public int? StationId { get; set; }

        [Display(Name = "Station")]
        public Station Station { get; set; }

        [Required]
        [Display(Name = "COMUL")]
        public int COMUL { get; set; }
        public ICollection<FleetCustodian> FleetCustodians { get; set; }
        public ICollection<FleetSize> FleetSize { get; set; }
        public ICollection<Grounded> Grounded { get; set; }

    }
}
