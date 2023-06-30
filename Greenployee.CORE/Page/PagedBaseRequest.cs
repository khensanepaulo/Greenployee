using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.CORE.Page
{
    public class PagedBaseRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderByProperty { get; set; }

        public PagedBaseRequest()
        {
            Page = 1;
            PageSize = 10;
            OrderByProperty = "id";
        }
    }
}
