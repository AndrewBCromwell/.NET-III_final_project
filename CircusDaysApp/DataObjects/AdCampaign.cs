using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class AdCampaign
    {
        public int AdCampaignId { get; set; }

        [Required]
        public int AdCompany { get; set; }
        public decimal TotalCost { get; set; }
        public int EmployeeId { get; set; }
        public override string ToString()
        {
            return AdCampaignId.ToString();
        }
    }

    public class AdItem
    {
        public int CampaignId { get; set; }

        [Required]
        public string AdType { get; set; }
        public string FocusAct { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value Must Bigger Than {1}")]
        public decimal Cost { get; set; }
        [Display(Name = "List item in campaign")]
        public bool IsLastItemInCampaign { get; set; }

        public override string ToString()
        {
            string str;
            if (FocusAct == null)
            {
                str = "Type = " + AdType + ". Cost = " + Cost + ". No single focus.";
            }
            else
            {
                str = "Type = " + AdType + ". Focus Act = " + FocusAct + ". Cost = " + Cost + ". ";
            }
            return str;
        }
    }

    public class AdCampaignVM : AdCampaign
    {
        public List<AdItem> AdItems { get; set; }
        public string AdCompanyName { get; set; }
        public int? AverageTicketsSold { get; set; }
        public decimal? AverageRevenue { get; set; }
    }
}
