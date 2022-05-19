using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Admin/[action]")]
    public class AdminController : Controller
    {
        private readonly SignInManager<UserAccount> _signInManager;
        private readonly UserManager<UserAccount> _userManager;
        private readonly FinalProjectContext _context;

        public AdminController(FinalProjectContext context, UserManager<UserAccount> userManager, SignInManager<UserAccount> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: Admin
        /// <summary>
        /// Starting page for Admin Users. By Default queries the context for all User Accounts
        /// </summary>
        /// <returns>List of all currently registered Users</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserAccounts.ToListAsync());
        }

        /// <summary>
        ///  Asyncronously queries the application context for all Images
        /// </summary>
        /// <returns>View with a list of Images as the Model</returns>
        public async Task<IActionResult> ImagesList()
        {
            return View(await _context.Images.Include(i => i.User).ToListAsync());
        }

        // GET: Admin/DeleteUser
        /// <summary>
        /// queries the context for the specified user entity to delete
        /// </summary>
        /// <param name="id">The Id of the User to delete</param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        /// <summary>
        /// Searches the context for all Comments assoiciated with a specified image ID
        /// </summary>
        /// <param name="id">The Id of the image</param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteComment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images.Include(i => i.Comments).FirstOrDefaultAsync(i => i.ImageID == id);

            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }
        /// <summary>
        /// Deletes the specified Comment from the Application Context
        /// </summary>
        /// <param name="id">The Id of the comment to delete</param>
        /// <returns></returns>
        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCommentConfirmed(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            var imageComment = await _context.ImageComments.Where((ic) => ic.CommentID == comment.CommentID).FirstOrDefaultAsync();
            var userComment = await _context.UserComments.Where((uc) => uc.CommentID == comment.CommentID).FirstOrDefaultAsync();

            _context.Comments.Remove(comment);
            _context.ImageComments.Remove(imageComment);
            _context.UserComments.Remove(userComment);

            await _context.SaveChangesAsync();

            return LocalRedirect("~/Images/Index");
        }

        // POST: Admin/DeleteUser
        /// <summary>
        /// Deletes the specified User from the Application Context
        /// </summary>
        /// <param name="id">The Id of the user to delete</param>
        /// <returns></returns>
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(int id)
        {
            var userAccount = await _context.UserAccounts.FindAsync(id);
            _context.UserAccounts.Remove(userAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccountExists(int id)
        {
            return _context.UserAccounts.Any(e => e.Id == id);
        }
    }
}
