using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class AdCompany
    {
        public int AdCompanyId { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        [StringLength(50, ErrorMessage = "The company name must be 50 characters or less.")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Street Address")]
        [StringLength(150, ErrorMessage = "The venue name must be 150 characters or less.")]
        public string StreetAdress { get; set; }
        [Required]
        [Display(Name = "ZipCode")]
        [StringLength(5, ErrorMessage = "The ZipCode must be 5 characters long.", MinimumLength = 5)]
        public string ZipCode { get; set; }
        [Required]
        [Display(Name = "PhoneNumber")]
        [Phone]
        public string PhoneNumber { get; set; }
        public override string ToString()
        {
            return CompanyName;
        }
    }

    public class AdCompanyVM : AdCompany
    {
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

