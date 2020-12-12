using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotUrl.Models
{
    public class DeeplinkServiceModel : BaseServiceModel
    {
        public int CampaignId { get; set; }
        public string Page { get; set; }
    }
}
