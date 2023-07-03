using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Infrastructure.Commands
{
    public class AddOrder
    {
        public Guid ProductId { get;  set; }
        public ushort Amount { get;  set; }
    }
}
