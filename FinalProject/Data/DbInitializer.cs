using FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    public class DbInitializer
    {
        public static void Initialize(FinalProjectContext context)
        {
            context.Database.EnsureCreated();

            // Look for any user accounts.
            if (context.UserAccounts.Any())
            {
                return;   // DB has been seeded
            }

            PasswordHasher<UserAccount> hasher = new PasswordHasher<UserAccount>();
            // Create UserAccount object
            var userAccounts = new UserAccount[]
            {
                new UserAccount{UserName="JDoe", FirstName="John",LastName="Doe",Email="JDoe@acme.com", PasswordHash= "JDoe2314",Privilage=true},
                new UserAccount{UserName="JSchmidt", FirstName="Jane",LastName="Schmidt",Email="JSchmidt@hotmail.com",PasswordHash= "JSchmidt2314",Privilage=true},
                new UserAccount{UserName="AMorgan", FirstName="Albert", LastName="Morgan", Email="AMorgan@example.com", PasswordHash= "AMorgan2314",Privilage=true},
                new UserAccount{UserName="BNelson", FirstName="Bonnie", LastName="Nelson", Email="BNelson@example.com", PasswordHash= "BNelson2314",Privilage=true},
            };

            foreach (UserAccount ua in userAccounts)
            {
                ua.PasswordHash = hasher.HashPassword(ua, ua.PasswordHash);
                ua.NormalizedUserName = ua.UserName.ToUpper();
                ua.NormalizedEmail = ua.Email.ToUpper();
                ua.SecurityStamp = (ua.LastName.ToString().GetHashCode()).ToString();
                context.UserAccounts.Add(ua);
            }

            //create the admin user object
            var adminUser = new UserAccount { UserName = "Power Admin", FirstName = "Clodagh", LastName = "Powers", Email = "CPowers@example.com", PasswordHash = "CPowers2314", Privilage = true };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, adminUser.PasswordHash);
            adminUser.NormalizedUserName = adminUser.UserName.ToUpper();
            adminUser.NormalizedEmail = adminUser.Email.ToUpper();
            adminUser.SecurityStamp = (adminUser.LastName.ToString().GetHashCode()).ToString();
            context.UserAccounts.Add(adminUser);

            context.SaveChanges();
             
            //create role objects
            var roles = new ApplicationRole[]
            {
                new ApplicationRole { Name = "User", NormalizedName = "USER"},
                new ApplicationRole { Name = "Admin", NormalizedName = "ADMIN" },
            };

            foreach (ApplicationRole role in roles)
            {
                context.Roles.Add(role);
            }

            context.SaveChanges();

            //assign roles to users
            foreach (UserAccount ua in userAccounts)
            {
                var ur = new IdentityUserRole<int> { UserId = ua.Id, RoleId = context.Roles.FirstOrDefault((r) => r.Id == 1).Id };
                context.UserRoles.Add(ur);
            }

            //assign admin role
            var admin = new IdentityUserRole<int> { UserId = adminUser.Id, RoleId = context.Roles.FirstOrDefault((r) => r.Id == 2).Id };
            context.UserRoles.Add(admin);

            context.SaveChanges();

            // Create Image object
            var images = new Image[]
            {
                new Image{Title="Kleenex....for men",User=userAccounts[0],FileName="tissues.jpg"},
                new Image{Title="As a chess player, I find this to be hilarious", User=userAccounts[1],FileName="chess.jpg"},
            };

            foreach (Image im in images)
            {
                context.Images.Add(im);
            }

            context.SaveChanges();

        }
    }
}
