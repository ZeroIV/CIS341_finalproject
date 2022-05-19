using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    /// <summary>
    /// Represents the relationship between an Image and a Comment
    /// </summary>
    public class ImageComment
    {
        public int ImageCommentID { get; set; }

        ///<value>Gets or Sets the associated Image id</value>
        public int ImageID { get; set; }
        ///<value>Gets or Sets the assoiciated Comment id </value>
        public int CommentID { get; set; }


        public Image Image { get; set; }
        public Comment Comment { get; set; }
    }
}
