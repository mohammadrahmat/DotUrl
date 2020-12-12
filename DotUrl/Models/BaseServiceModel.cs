using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotUrl.Models
{
    public class BaseServiceModel
    {
        public string Request { get; set; }
        public string Response { get; set; }
        public int ContentId { get; set; }
        public int MerchantId { get; set; }
        public string SearchQuery { get; set; }
        public PageType PageType { get; set; }
        public ServiceType Type { get; set; }
        public DateTime RecordDate { get; set; }
        public bool HasError { get; set; }
        public string ErrorDetails { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
