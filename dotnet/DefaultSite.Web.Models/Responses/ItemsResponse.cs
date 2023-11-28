using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Web.Models.Responses
{
    public class ItemsResponse<T>: SuccessResponse
    {
        public List<T> Items { get; set; }
    }
}
