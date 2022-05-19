using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    /// <summary>
    /// Represents a User, inherits from the IdentityUser Class
    /// </summary>
    public class UserAccount : IdentityUser<int>
    {
        /// <value> gets or sets the first name of the User </value>
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <value> gets or sets the Last name of the User </value>
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <value> Property that represents a User's ability to Access the App </value>
        public bool Privilage { get; set; }

        /// <value> The list of Images associated with this User.</value>
        public ICollection<Image> Images { get; set; }
        /// <value> The list of Comments associated with this User.</value>
        public ICollection<UserComment> Comments { get; set; }
        /// <value> The list of Votes associated with this User.</value>
        public ICollection<UserVote> Votes { get; set; }
    }
}
