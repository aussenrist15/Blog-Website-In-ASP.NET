﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog_Website.Models {
    public class User {
       public int userID { get; set; }
       public string FName { get; set; }
       public string LName { get; set; }

       public string Email { get; set; }
       public string Password { get; set; }

       public string ErrMessage { get; set; } // JUGAR. 

       public List<Blog> userBlogs { get; set; }
    }
}
