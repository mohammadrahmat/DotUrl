using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotUrl.Models
{
    public class UrlServiceModel : BaseServiceModel
    {
        public int BoutiqueId { get; set; }
        public string BrandCategoryName { get; set; }
        public string ProductName { get; set; }
    }
}
