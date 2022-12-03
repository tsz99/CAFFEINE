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



                foreach (var item in caffs)
                {
                    var gif = new AnimatedGif.AnimatedGifCreator(new MemoryStream(item.Ciffs[0].Pixels), item.Ciffs[0].Duration);
                    for (int i = 1; i < item.Ciffs.Count; i++)
                    {
                        gif.AddFrame(Image.FromStream(new MemoryStream(item.Ciffs[i].Pixels)), item.Ciffs[0].Duration);
                    }
                }
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

            var stream = new MemoryStream(rec.originalContent);
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = $"{rec.Creator}.caff"
            };
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upload()
        {
            if(this.Request.Form.Files.Count == 0)
            {
                return BadRequest();
            }
            IFormFile httpPostedFile = this.Request.Form.Files[0];
            BinaryReader b = new BinaryReader(httpPostedFile.OpenReadStream());
            byte[] binData = b.ReadBytes((int)httpPostedFile.Length);


            Caff caff = new Caff()
            {
                originalContent = binData
            };
            var CaffResult = Parsing.CaffProcessor.ParseCaff(binData, User.Identity.Name);
            caff.Year = CaffResult.Year;
            caff.Hour = CaffResult.Hour;
            caff.Creator = CaffResult.Creator;
            caff.Day = CaffResult.Day;
            caff.Month = CaffResult.Month;
            caff.Minute= CaffResult.Minute;
            caff.Ciffs = new List<Ciff>();
            for (int i = 0; i < CaffResult.Ciffs.Count; i++)
            {
                List<Tag> tags = CaffResult.Ciffs[i].Tags.Select(x => new Tag() { Text = x}).ToList();
                byte[] currentFile = System.IO.File.ReadAllBytes(@$"{User.Identity.Name}\{i+1}.bmp");
                System.IO.File.Delete(@$"{User.Identity.Name}\{i + 1}.bmp");
                caff.Ciffs.Add(new Ciff()
                {
                    Caption = CaffResult.Ciffs[i].Caption,
                    Pixels = currentFile,
                    Duration = CaffResult.Ciffs[i].Duration,
                    Height = CaffResult.Ciffs[i].Height,
                    Width = CaffResult.Ciffs[i].Width,
                    Tags = tags
                });  
            }
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
        public async Task<IActionResult> Users()
        {
            List<UserData> users = await _caffRepository.GetUsers();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUsers(List<UserData> users)
        {
            await _caffRepository.UpdateUsers(users);
            return RedirectToAction("Users", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
