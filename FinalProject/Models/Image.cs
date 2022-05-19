using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Image
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <value>Gets or Sets the Id of the Image </value>
        public int ImageID { get; set; }

        /// <value>Gets or Sets The ID of the Associated User </value>
        public int UserAccountID { get; set; }

        /// <value>Gets or Sets Title given to the Image after Posting</value>
        public string Title { get; set; }

        /// <value>Gets or Sets the name of the image file</value>
        [Display(Name = "File Name")]
        public string FileName { get; set; }

        /// <value>Gets or Sets The User Account associated with this Image</value>
        public UserAccount User { get; set; }

        /// <value> The list of Comments associated with this Image.</value>
        public ICollection<ImageComment> Comments { get; set; }

        public ICollection<ImageVote> Votes { get; set; }
    }
}
