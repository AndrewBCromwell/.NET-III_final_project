using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Venue
    {
        public int VenueID { get; set; }

        [Required]
        [Display(Name = "Venue Name")]
        [StringLength(50, ErrorMessage = "The venue name must be 50 characters or less.")]
        public string VenueName { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        [StringLength(50, ErrorMessage = "The street address must be 50 characters or less.")]
        public string StreetAddress { get; set; }

        [Required]
        [Display(Name = "ZipCode")]
        [StringLength(5, ErrorMessage = "The ZipCode must be 5 characters long.", MinimumLength = 5)]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "PhoneNumber")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Terms of Use")]
        [StringLength(500, ErrorMessage = "The terms of use must be 500 characters or less.")]
        public string TermsOfUse { get; set; }

        public override string ToString()
        {
            return VenueName;
        }
    }

    public class VenueVM : Venue
    {
        public int? AverageTicketsSold { get; set; }
        public decimal? AverageRevenue { get; set; }
        public DateTime? LastUsedOn { get; set; }

        [Required]
        [Display(Name = "City")]
        [StringLength(50, ErrorMessage = "The City must be 50 characters or less.")]
        public string City { get; set; }

        [Required]
        [Display(Name = "State (ST)")]
        [StringLength(2, ErrorMessage = "Must use the 2 letter state abbreviation.", MinimumLength = 2)]
        public string State { get; set; }
    }
}
