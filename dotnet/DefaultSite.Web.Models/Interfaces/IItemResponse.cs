using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Web.Models
{
    public interface IItemResponse
    {
        bool IsSuccessful { get; set; }

        string TransactionId { get; set; }

        object Item { get; }
    }
}
