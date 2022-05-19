using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    /// <summary>
    /// Represents the relationship between a User and a Vote
    /// </summary>
    public class UserVote
    {
        public int UserVoteID { get; set; }
        public int VoteID { get; set; }
        public int UserAccountID { get; set; }

        public UserAccount User { get; set; }
        public Vote Vote { get; set; }
    }
}
