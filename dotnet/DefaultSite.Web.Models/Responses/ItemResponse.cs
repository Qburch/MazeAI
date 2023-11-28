using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Web.Models.Responses
{
    public class ItemResponse<T>: SuccessResponse, IItemResponse
    {
        public T Item { get; set; }

        object IItemResponse.Item { get { return this.Item; } }
    }
}
