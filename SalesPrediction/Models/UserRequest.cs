using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesPrediction.Models {
    public class UserRequest {
        public int SalesId { get; set; }
        public DateTime Dos { get; set; }
        public double Sales { get; set; }
    }
}
