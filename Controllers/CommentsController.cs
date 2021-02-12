using AppEtizer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppEtizer.Controllers
{
    public class CommentsController : Controller
    {
        public IActionResult Comment(CommentViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return 
            }
            return View();
            // Finish the comment section & think about admin
        }
    }
}
