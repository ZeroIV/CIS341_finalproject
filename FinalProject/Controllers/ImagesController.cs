using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Controllers
{
    [Route("Images/[action]")]
    public class ImagesController : Controller
    {
        private readonly FinalProjectContext _context;
        private readonly SignInManager<UserAccount> _signInManager;
        private readonly UserManager<UserAccount> _userManager;

        public ImagesController(SignInManager<UserAccount> signInManager, UserManager<UserAccount> userManager, FinalProjectContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        // GET: Images
        /// <summary>
        /// Default behavior is to query the Database for a list of images.
        /// Excluding images posted by the currently signed in User
        /// <remarks>
        /// Returns all Images in Database if no user signed in
        /// </remarks>
        /// </summary>
        /// <param name="id">The id of the User currently signed in to the application</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(int? id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var finalProjectContext = _context.Images.Include(i => i.User).Where((i) => i.UserAccountID != currentUser.Id);
                return View(await finalProjectContext.ToListAsync());
            }
            else
            {
                var finalProjectContext = _context.Images.Include(i => i.User);
                return View(await finalProjectContext.ToListAsync());
            }         
        }

        /// <summary>
        /// Queries the database for a list of Images based on user role
        /// </summary>
        /// <param name="id">The Id of the Currently Signed in User</param>
        /// <returns></returns>
        public async Task<IActionResult> Images(int id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (User.IsInRole("Admin"))
                {
                    var adminContext = _context.Images.Include(i => i.User);
                    return View("Index", await adminContext.ToListAsync());
                }
                var currentUser = await _userManager.GetUserAsync(User);
                var context = _context.Images.Include(i => i.User).Where((i) => i.UserAccountID == currentUser.Id);

                return View("Index",await context.ToListAsync());
            }
            return LocalRedirect("~/");
        }

        // GET: Images/Details/5
        /// <summary>
        /// Displays more info on a single User Post/Image.
        /// <remarks>This includes comments, votes, etc.</remarks>
        /// </summary>
        /// <param name="id">The id of the image</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {        
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                                      .Include(i => i.User)
                                      .Include(i => i.Comments)
                                      .FirstOrDefaultAsync(m => m.ImageID == id);
            if (image == null)
            {
                return NotFound();
            }


            var x = 0;
            string[,] array = new string[image.Comments.Count(), 2];
            foreach (var item in image.Comments)
            {
                array[x, 0] = _context.Comments.Find(item.CommentID).Content;

                var userComment = _context.UserComments.Find(item.CommentID);
                var user = _context.UserAccounts.Where((u) => u.Id == userComment.UserAccountID).FirstOrDefault();

                array[x, 1] = user.UserName;
                x++;
            }

            ViewData.Add("Comments", array);

            return View(image);
        }

        // GET: Images/Create
        /// <summary>
        /// Ensures a user is signed in before they attempt to create a new post
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            if(_signInManager.IsSignedIn(User))
            return View();

            return View("Error", "You have to be signed in to create a new post!");
        }

        // POST: Images/Create
        /// <summary>
        /// Uploads the Provided Image file to the local server(the app in this case)
        /// </summary>
        /// <remarks>Validates the file using the HttpContext Form ContentType property</remarks>
        /// <param name="image">Entity object to save to the context</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageID,UserAccountID,Title,FileName")] Image image)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Request.Form.Files[0].ContentType.Contains("image/"))
                {
                    IFormFile file = Request.Form.Files[0];
                    image.User = await _userManager.GetUserAsync(User);
                    image.FileName = file.FileName;

                    _context.Add(image);
                    await _context.SaveChangesAsync();

                    Task.WaitAll(UploadFile(file));

                    return RedirectToAction(nameof(Index));
                }
                return View("UploadError", "Invalid filetype. Only image files can be uploaded!");
            }
            return View(image);
        }

        //Save uploaded image to server
        /// <summary>
        /// Method that copies and saves the uploaded image file to the server
        /// </summary>
        /// <param name="ufile">The file to be saved</param>
        /// <returns>True on success, False on failure</returns>
        private async Task<bool> UploadFile(IFormFile ufile)
        {
            if (ufile != null && ufile.Length > 0)
            {
                var fileName = Path.GetFileName(ufile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ufile.CopyToAsync(fileStream);
                }
                return true;
            }
            return false;
        }

        // GET: Images/Delete/5
        /// <summary>
        /// Image Action to delete an image from the Application context
        /// </summary>
        /// <param name="id">The id of the Image</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.ImageID == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        /// <summary>
        /// Ensures the image is removed from the context and the Database
        /// </summary>
        /// <param name="id">The id of the Image</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.Images.FindAsync(id);
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Action that saves a Comment entity to the database, and sets the proper Navigation Properties
        /// </summary>
        /// <param name="comment">The Comment entity that with be saved</param>
        /// <param name="id">The Id of the image the Comment is associated with</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment([Bind("CommentID, Content")] Comment comment, int id)
        {
            if (_userManager.GetUserId(User) == null)
            {
                return View("Error", "You must be logged in to comment!");
            }

            if (ModelState.IsValid)
            {
                var image = _context.Images
                                    .Include(i => i.Comments)
                                    .FirstOrDefault(im => im.ImageID == id);

                var user = await _userManager.GetUserAsync(User);
                user.Comments =  _context.UserComments.Where((userCom) => userCom.UserAccountID == user.Id).ToList();

                comment.Content = Request.Form["Comments"];
                comment.Content = comment.Content.ToString().Trim();

                _context.Comments.Add(comment);

                await _context.SaveChangesAsync();

                ImageComment ic = new ImageComment { ImageID = image.ImageID, CommentID = comment.CommentID, };
                UserComment uc = new UserComment { UserAccountID = user.Id, CommentID = comment.CommentID, };

                image.Comments.Add(ic);
                user.Comments.Add(uc);

                await _context.SaveChangesAsync();

                var returnUrl = "~/Images/Details/?id=" + image.ImageID.ToString();
                return LocalRedirect(returnUrl);
            }
            return View("Error", "Server error");
        }

        private bool ImageExists(int id)
        {
            return _context.Images.Any(e => e.ImageID == id);
        }
    }
}
