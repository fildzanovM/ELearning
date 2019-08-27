using System;
using System.Collections.Generic;

namespace ELearning.Models
{
    public partial class Purchases
    {
        public int PurchasesId { get; set; }
        public int CourseId { get; set; }
        public DateTime PurchaseDate { get; set; }

        public Course Course { get; set; }
    }
}
