using System;
using System.Collections.Generic;

namespace ELearning.Models
{
    public partial class CourseLevel
    {
        public CourseLevel()
        {
            CourseInfo = new HashSet<CourseInfo>();
        }

        public int CourseLevelId { get; set; }
        public string CourseLevelName { get; set; }

        public virtual ICollection<CourseInfo> CourseInfo { get; set; }
    }
}
