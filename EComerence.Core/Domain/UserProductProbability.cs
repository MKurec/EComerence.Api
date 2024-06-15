using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Core.Domain
{
   public class UserProductProbability
   {
      public Guid UserId { get; set; }
      public virtual User User { get; set; }
      public Guid ProductId { get; set; }
      public virtual Product Product { get; set; }
      public double Probablity { get; set; }
   }
}
