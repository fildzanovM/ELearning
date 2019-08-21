using System;
using System.Collections.Generic;

namespace ELearning.Models
{
    public partial class CourseInfo
    {
        public int CourseInfoId { get; set; }
        public int? CourseRating { get; set; }
        public string CourseDuration { get; set; }
        public int CourseId { get; set; }
        public int? CourseLevelId { get; set; }
        public int? CoursePrice { get; set; }

        public virtual Course Course { get; set; }
        public virtual CourseLevel CourseLevel { get; set; }
    }
}
