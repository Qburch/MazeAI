using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Web.Models.Responses
{
    public class SuccessResponse: BaseResponse
    {
        public SuccessResponse() {
            this.IsSuccessful = true;
        }
    }
}
