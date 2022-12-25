using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Comments
    {
        public string Comment { get; set; }

        public string Name { get; set; }
        public int PostID { get; set; }
        public int CommentsUserID {get; set;}

        public string CreatedDate { get; set; }
    }
}
