using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Models.Models;

public class Cart
{
    public int Id { get; set; }
    public List<Products> Products { get; set; }
}
