using System;
using System.Collections.Generic;

namespace ELearning.Models
{
    public partial class Category
    {
        public Category()
        {
            Course = new HashSet<Course>();
            SubCategory = new HashSet<SubCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }

        public ICollection<Course> Course { get; set; }
        public ICollection<SubCategory> SubCategory { get; set; }
    }
}
