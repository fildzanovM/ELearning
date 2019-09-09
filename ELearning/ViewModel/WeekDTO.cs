using ELearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.ViewModel
{
    public class WeekDTO
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<CoursePurchasedNumberByCategoryDTO> Categories { get; set; }
    }

    public class CoursePurchasedNumberByCategoryDTO
    {
        public string CategoryName { get; set; }

        public int PurchasedCount { get; set; }
    }
}
