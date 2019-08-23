using System;
using System.Collections.Generic;

namespace ELearning.Models
{
    public partial class CourseModule
    {
        public int CourseModuleId { get; set; }
        public string CourseModuleName { get; set; }
        public int CourseId { get; set; }
        public string ModuleDuration { get; set; }
        public string ModuleVideo { get; set; }

        public Course Course { get; set; }
    }
}
