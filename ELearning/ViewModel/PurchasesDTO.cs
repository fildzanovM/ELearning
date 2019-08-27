using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.ViewModel
{
    public class PurchasesDTO
    {
        public int CourseId { get; set; }
        public DateTime  PurchaseDate { get; set; }
    }

    public class GetAllPurchases
    {
        public int PurchaseId { get; set; }
        public string CourseName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string CategoryName { get; set; }
    }
}
