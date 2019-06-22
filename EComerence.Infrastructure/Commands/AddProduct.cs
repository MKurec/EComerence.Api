using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Infrastructure.Commands
{
    public class AddProduct
    {
        public string Name { get;  set; }
        public int Amount { get;  set; }
        public decimal Price { get;  set; }
        public string ProducerName { get;  set; }
        public string CategoryName { get;  set; }
    }
}
