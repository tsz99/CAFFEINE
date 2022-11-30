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

namespace CAFFEINE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CaffService _caffService;
        private readonly CaffRepository _caffRepository;

        public HomeController(ILogger<HomeController> logger,CaffService caffService, CaffRepository caffrepository)
        {
            _logger = logger;
            _caffService = caffService;
            _caffRepository = caffrepository;

        }

        public IActionResult Index()
        {

            //var caff = _caffService.ParseCaff();
            var caffs = _caffRepository.GetAllCaff();
            foreach (var item in caffs)
            {
                using (Image image = Image.FromStream(new MemoryStream(item.Ciffs[0].Pixels)))
                {
                    item.Ciffs[0].Png = image;
                }
            }
            return View();
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
            if (caff!= null && caff.Ciffs!=null)
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


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
