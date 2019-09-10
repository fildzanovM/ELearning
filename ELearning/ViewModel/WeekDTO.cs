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

        public List<CategoryCoursesPurchasesCountDTO> Categories { get; set; }
    }

    public class CategoryCoursesPurchasesCountDTO
    {
        public string CategoryName { get; set; }

        public int CoursesPurchasesCount { get; set; }
    }

    public class CoursesPurchasesCountDTO
    {
        public string CourseName { get; set; }

        public int PurchasesCount { get; set; }
    }
}
