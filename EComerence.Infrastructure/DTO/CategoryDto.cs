using EComerence.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Infrastructure.DTO
{
   public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<CategoryDto> SubCategories { get; set; }

    }
}
