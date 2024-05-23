using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsletter.Core.Entities
{
    public class Newsletter
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
