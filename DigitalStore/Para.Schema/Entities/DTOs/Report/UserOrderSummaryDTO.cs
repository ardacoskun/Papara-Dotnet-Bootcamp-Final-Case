using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Schema.Entities.DTOs.Report
{
    public class UserOrderSummaryDTO
    {
        public int OrderId { get; set; }          
        public DateTime OrderDate { get; set; }  
        public string CouponCode { get; set; }    
        public decimal CouponAmount { get; set; } 
        public int PointsUsed { get; set; }       
        public decimal TotalAmount { get; set; } 
    }
}
