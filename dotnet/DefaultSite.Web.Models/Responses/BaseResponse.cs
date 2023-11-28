using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Web.Models.Responses
{
    public class BaseResponse
    {
        public bool IsSuccessful { get; set; }

        public string TransactionId { get; set; }

        public BaseResponse() { 
            this.TransactionId = Guid.NewGuid().ToString();
        }

    }
}
