using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Schema.Entities.DTOs.Report
{
    public class TopSellingProductsDTO
    {
        public string ProductName { get; set; }
        public int QuantitySold { get; set; }
    }
}
