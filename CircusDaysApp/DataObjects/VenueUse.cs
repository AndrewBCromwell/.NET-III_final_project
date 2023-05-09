using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel;


namespace DataObjects
{
    public class VenueUse
    {
        public int VenueUseId { get; set; }

        [Required]
        public int VenueId { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public int? AdCampain { get; set; }
        public int TotalTicketsSold { get; set; }
        public decimal TotalRevenue { get; set; }
        public int EmployeeId { get; set; }
        public override string ToString()
        {
            return VenueUseId + ": " + StartDate.ToShortDateString() + " - " + EndDate.ToShortDateString();
        }
    }
        
    
    public class UseDay
    {     
        [Key]
        public int? UseDayId { get; set; }

        public int UseId { get; set; }
        
        [Required]
        public DateTime UseDate { get; set; }

        [Required]
        [Display(Name = "Tickets Sold")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int TicketsSold { get; set; }

        [Required]
        [Display(Name = "Revenue")]
        [Range(0, int.MaxValue, ErrorMessage = "Value Must Bigger Than {1}")]
        public decimal Revenue { get; set; }

        [Required]
        public int EmployeeId { get; set; }
    }

    public class VenueUseVM : VenueUse
    {
        [Key]
        public int? VenueUseVMId { get; set; }
        public List<UseDay> useDays { get; set; }
        public string VenueName { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public override string ToString()
        {
            return VenueName + ": " + StartDate.ToShortDateString() + " - " + EndDate.ToShortDateString();
        }
    }
}
