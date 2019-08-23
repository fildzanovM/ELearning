using System;
using System.Collections.Generic;

namespace ELearning.Models
{
    public partial class Author
    {
        public Author()
        {
            Course = new HashSet<Course>();
        }

        public int AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorTelephone { get; set; }
        public string AuthorImage { get; set; }

        public ICollection<Course> Course { get; set; }
    }
}
