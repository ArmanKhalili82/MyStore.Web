using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Models.Models;

public class OrderDetails
{
    public int Id { get; set; }
    [DisplayName("Order")]
    public int OrderId { get; set; }
    [DisplayName("Product")]
    public int ProductId { get; set; }
    [ForeignKey("OrderId")]
    public Order Order { get; set; }
    [ForeignKey("ProductId")]
    public Products Products { get; set; }
}
