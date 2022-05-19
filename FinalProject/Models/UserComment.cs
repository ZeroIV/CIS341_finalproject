using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    /// <summary>
    /// Represents the relationship between a User and a Comment
    /// </summary>
    public class UserComment
    {
        public int UserCommentID { get; set; }
        public int UserAccountID { get; set; }
        public int CommentID { get; set; }

        public UserAccount User { get; set; }
        public Comment Comment { get; set; }
    }
}
