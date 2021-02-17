using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Infrastructure.Commands
{
    public class UpdateProduct
    {
        public string Description { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
