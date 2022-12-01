using CAFFEINE.Data;
using CAFFEINE.Models;
using CAFFEINE.Repositories;
using CAFFEINE.Services;
using CAFFEINE.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CAFFEINE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CaffService _caffService;
        private readonly CaffRepository _caffRepository;

        public HomeController(ILogger<HomeController> logger, CaffService caffService, CaffRepository caffrepository)
        {
            _logger = logger;
            _caffService = caffService;
            _caffRepository = caffrepository;

        }

        public IActionResult Index(string creator = "", string caption = "")
        {

            if (string.IsNullOrEmpty(creator) && string.IsNullOrEmpty(caption))
            {
                var caffs = _caffRepository.GetAllCaff();
                return View(new IndexVM()
                {
                    creator = creator,
                    caption = caption,
                    caffs = caffs
                });
            }
            else
            {
                List<Caff> caffsFiltered = _caffRepository.GetCaffsByFilter(creator, caption);
                return View(new IndexVM()
                {
                    creator = creator,
                    caption = caption,
                    caffs = caffsFiltered
                });
            }

            /*  var caff = _caffService.ParseCaff();
              _caffRepository.SaveCaff(caff);*/

            /*foreach (var item in caffs)
            {
                Stream stream = new MemoryStream();
                stream.Write(item.Ciffs[0].Pixels, 0, item.Ciffs[0].Pixels.Length);
                using (Image image = Image.FromStream(stream, true))
                {
                    item.Ciffs[0].Png = image;
                  
                }
            }*/

        }

        


        [HttpGet]
        public IActionResult Comment(int id)
        {
            if (_caffRepository.GetCaffFromId(id) != null)
            {
                return PartialView("~/Views/Home/PartialViews/AddComment.cshtml", new AddCommentVM() { CaffId = id });
            }
            else
            {
                return BadRequest();
            }


        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var caff = _caffRepository.GetCaffFromId(id);
            if (caff != null && caff.Ciffs != null)
            {
                return PartialView("~/Views/Home/PartialViews/Details.cshtml", new DetailsVM() { Comments = _caffRepository.GetAllCommentToCaff(id), CaffCreator = caff.Creator, Caption = caff.Ciffs[0].Caption, Tags = caff.Ciffs[0].Tags });
            }
            else
            {
                return BadRequest();
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Comment(AddCommentVM addCommentVM)
        {
            if (_caffRepository.GetCaffFromId(addCommentVM.CaffId) != null)
            {
                var user = User.Identity.Name;
                _caffRepository.AddCommentToCaff(addCommentVM.CaffId, user, addCommentVM.Text);
                return Json(new { success = true });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var caff = _caffRepository.GetCaffFromId(id);
            if (caff != null && caff.Ciffs != null)
            {
                return PartialView("~/Views/Home/PartialViews/Delete.cshtml", new DeleteVM() { CaffId = id });
            }
            else
            {
                return BadRequest();
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DeleteVM deleteVM)
        {
            if (_caffRepository.GetCaffFromId(deleteVM.CaffId) != null)
            {
                _caffRepository.DeleteCaff(deleteVM.CaffId);
                return Json(new { success = true });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public FileResult Download(int id)
        {
            Caff rec = _caffRepository.GetCaffFromId(id);
            StringBuilder builder = new StringBuilder();

            //TODO: giffé


            var stream = new MemoryStream(Encoding.ASCII.GetBytes(""));
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = $"{rec.Creator}.txt"
            };
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upload()
        {
            IFormFile httpPostedFile = this.Request.Form.Files[0];
            BinaryReader b = new BinaryReader(httpPostedFile.OpenReadStream());
            byte[] binData = b.ReadBytes((int)httpPostedFile.Length);
            

            Parsing.CaffProcessor.ParseCaff(binData, "caffs");
            Caff caff = new Caff()
            {
                originalContent = binData,
                Creator = User.Identity.Name
            };
            _caffRepository.SaveCaff(caff);
            return Json(new { success = true });
        }



        [HttpGet]
        public IActionResult UploadGet()
        {
            return PartialView("~/Views/Home/PartialViews/Upload.cshtml");
        }


        [HttpPost]
        public IActionResult Search()
        {
            var creator = this.Request.Form["Creator"].ToString();
            var caption = this.Request.Form["Caption"].ToString();
            return RedirectToAction("Index", "Home", new { creator = creator, caption = caption });
        }


        [HttpGet]
        public IActionResult Users()
        {
            var users = _caffRepository.GetUsers();
            return View(new UsersVM() { users = users});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
