using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    /// <summary>
    /// Represents the relationship between an Image and a Vote
    /// </summary>
    public class ImageVote
    {
        public int ImageVoteID { get; set; }
        public int VoteID { get; set; }
        public int ImageID { get; set; }

        public Image Image { get; set; }
        public Vote Vote { get; set; }
    }
}