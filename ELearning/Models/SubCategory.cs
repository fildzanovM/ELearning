using System;
using System.Collections.Generic;

namespace ELearning.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Course = new HashSet<Course>();
        }

        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public string SubCategoryImage { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Course> Course { get; set; }
    }
}
