using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Models.Models;

public class Products
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
    public bool isAvailable { get; set; }
    [DisplayName("Category")]
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
    public int? CartId { get; set; }
    [ForeignKey("CartId")]
    public Cart? Cart { get; set; }
    public int? OrderId { get; set; }
    [ForeignKey("OrderId")]
    public Order? Order { get; set; }
}
