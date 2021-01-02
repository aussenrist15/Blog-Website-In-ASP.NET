using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog_Website.Models {
    public class Blog {
        public int blogID { get; set; }
        public int userID { get; set; }
        public string userName { get; set; }
        public string blogTitle { get; set; }
        public string blogBody { get; set; }
        public DateTime blogDate { get; set; }
    }
}
