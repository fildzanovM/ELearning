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

        public Category Category { get; set; }
        public ICollection<Course> Course { get; set; }
    }
}
