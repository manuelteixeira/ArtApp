using System;
using System.Collections.Generic;

namespace ArtApp.Model
{
    public class Project
    {

        public int ProjectId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public ICollection<Work> Works { get; set; }
    }
}
