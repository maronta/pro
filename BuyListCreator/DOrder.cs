using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyListCreator
{
    public class DOrder
    {
        public int SystemId { get; set; }
        public int MarketId { get; set; }
        public String MarketNM { get; set; }
        public int BrandId { get; set; }
        public int Code { get; set; }
        public String BrandNM { get; set; }
        public DateTime OrderDate { get; set; }
        public int Price { get; set; }
        public int OrderNumber { get; set; }
        public int OrderType { get; set; }

    }
}
