using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Models.Models;

public class Order
{
    public Order()
    {
        OrderDetails = new List<OrderDetails>();
    }
    public int Id { get; set; }
    public string OrderNo { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    [DisplayName("Card Number")]
    public string CardNumber { get; set; }
    [Required]
    public string CCV { get; set; }
    [Required]
    public string ExpiredDateYear { get; set; }
    [Required]
    public string ExpiredDateMonth { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderDetails> OrderDetails { get; set; }
}
