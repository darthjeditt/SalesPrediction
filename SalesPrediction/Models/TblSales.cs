using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesPrediction.Models
{
    public partial class TblSales
    {
        [Key]
        [Column("SalesID")]
        public int SalesId { get; set; }
        [Column("DOS", TypeName = "datetime")]
        public DateTime Dos { get; set; }
        public double Sales { get; set; }
    }
}
