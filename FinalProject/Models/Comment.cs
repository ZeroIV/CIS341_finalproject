using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <value>Gets or Sets the Comment ID.</value>
        public int CommentID { get; set; }

        ///<value>Gets or Sets the content of the Comment.</value>
        public string Content { get; set; }
    }
}
