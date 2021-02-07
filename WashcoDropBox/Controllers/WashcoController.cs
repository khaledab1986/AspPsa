using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WashcoDropBox.Helper;
using WashcoDropBox.Models;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
 
namespace WashcoDropBox.Controllers
{
     
    public class WashcoController : Controller
    {

        private readonly IFileProvider _fileProvider;

        public static int log = 0 ;
        private readonly WashcoDbContext _context;
        private readonly IHostingEnvironment hosting;

        public WashcoController(WashcoDbContext context,IHostingEnvironment hosting)
        {
            _context = context;
            this.hosting = hosting;
        }


        //public async Task<IActionResult> Index()
        //{
        //    ViewBag.ShowDiv = false;
        //    ViewBag.Message = "";
        //    ViewBag.Message1 = "";
        //    ViewBag.Message2 = "";
        //    ViewBag.Message3 = "";
        //    var washcoDbContext = _context.WashcoBoxes;//.Include(w => w.CusNoNavigation);
        //    return View(await washcoDbContext.ToListAsync());

        //}


        //    [HttpGet]
        //    public async Task<IActionResult> Index( string  searchString,string Person2, string Section2,int PageNumber,int PageRow)
        //    {

        //        var wash2 = _context.WashcoBoxes.AsQueryable();
        //        wash2 = wash2.Where(x => x.Id > 0); 


        //        if (PageNumber == 0) { PageNumber = 1; }
        //        if (PageRow == 0) { PageRow = 3; }


        //        if (Person2 =="All Persons") { Person2 = ""; }
        //        if (Section2 == "All Sections") { Section2 = ""; }

        //        ViewBag.Searchselect1 = Person2;
        //        ViewBag.Searchselect2 = Section2;


        //        ViewBag.Showlogs = false;
        //        if (log == 1)
        //        {
        //            ViewBag.Showlogs = true;
        //        }




        //       if (!string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(Person2) & string.IsNullOrEmpty(Section2))
        //        {
        //            ViewBag.SearchText = searchString;

        //            var cus = await _context.CusData.FirstOrDefaultAsync(m => m.CusConNo == searchString);
        //            if (cus == null)
        //            {
        //                var wash = from m in _context.WashcoBoxes
        //                           select m;
        //                wash = wash.Where(s => s.OrderNo == searchString);
        //                ViewBag.Message = "";
        //                ViewBag.Message1 = "";
        //                ViewBag.Message2 = "";
        //                ViewBag.Message3 = "";
        //                ViewBag.ShowDiv = false;
        //                return View(await wash.ToListAsync());
        //            }
        //            else
        //            {
        //                var wash = from m in _context.WashcoBoxes
        //                           select m;
        //                wash = wash.Where(s => s.OrderNo == searchString);
        //                ViewBag.Message = cus.CusConNo;
        //                ViewBag.Message1 = cus.CusName;
        //                ViewBag.Message2 = cus.CusMobile1;
        //                ViewBag.Message3 = cus.CusSalesman;
        //                ViewBag.ShowDiv = true;
        //                return View(await wash.ToListAsync());

        //            }

        //        }
        //       if (string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(Person2) & string.IsNullOrEmpty(Section2))
        //        {
        //            ViewBag.SearchText = searchString;


        //                var wash = from m in _context.WashcoBoxes
        //                           select m;
        //                wash = wash.Where(s => s.Person == Person2);
        //                ViewBag.Message = "";
        //                ViewBag.Message1 = "";
        //                ViewBag.Message2 = "";
        //                ViewBag.Message3 = "";
        //                ViewBag.ShowDiv = false;

        //                return View(await wash.ToListAsync());


        //        }
        //       if (string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(Person2) & !string.IsNullOrEmpty(Section2))
        //        {
        //            ViewBag.SearchText = searchString;


        //            var wash = from m in _context.WashcoBoxes
        //                       select m;
        //            wash = wash.Where(s => s.Section == Section2);
        //            ViewBag.Message = "";
        //            ViewBag.Message1 = "";
        //            ViewBag.Message2 = "";
        //            ViewBag.Message3 = "";
        //            ViewBag.ShowDiv = false;
        //            return View(await wash.ToListAsync());


        //        }
        //       if (string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(Person2) & !string.IsNullOrEmpty(Section2))
        //        {
        //            ViewBag.SearchText = searchString;


        //            var wash = from m in _context.WashcoBoxes
        //                       select m;
        //            wash = wash.Where(s => s.Section == Section2 && s.Person== Person2);
        //            ViewBag.Message = "";
        //            ViewBag.Message1 = "";
        //            ViewBag.Message2 = "";
        //            ViewBag.Message3 = "";
        //            ViewBag.ShowDiv = false;
        //            return View(await wash.ToListAsync());


        //        }



        //       if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(Person2) & !string.IsNullOrEmpty(Section2))
        //        {
        //            ViewBag.SearchText = searchString;

        //            var cus = await _context.CusData.FirstOrDefaultAsync(m => m.CusConNo == searchString);
        //            if (cus == null)
        //            {
        //                var wash = from m in _context.WashcoBoxes
        //                           select m;
        //                wash = wash.Where(s => s.OrderNo == searchString && s.Person== Person2 && s.Section==Section2);
        //                ViewBag.Message = "";
        //                ViewBag.Message1 = "";
        //                ViewBag.Message2 = "";
        //                ViewBag.Message3 = "";
        //                ViewBag.ShowDiv = false;
        //                return View(await wash.ToListAsync());
        //            }
        //            else
        //            {
        //                var wash = from m in _context.WashcoBoxes
        //                           select m;
        //                wash = wash.Where(s => s.OrderNo == searchString && s.Person == Person2 && s.Section == Section2);
        //                ViewBag.Message = cus.CusConNo;
        //                ViewBag.Message1 = cus.CusName;
        //                ViewBag.Message2 = cus.CusMobile1;
        //                ViewBag.Message3 = cus.CusSalesman;
        //                ViewBag.ShowDiv = true;
        //                return View(await wash.ToListAsync());

        //            }

        //        }
        //       if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(Person2) & string.IsNullOrEmpty(Section2))
        //        {
        //            ViewBag.SearchText = searchString;

        //            var cus = await _context.CusData.FirstOrDefaultAsync(m => m.CusConNo == searchString);
        //            if (cus == null)
        //            {
        //                var wash = from m in _context.WashcoBoxes
        //                           select m;
        //                wash = wash.Where(s => s.OrderNo == searchString && s.Person == Person2 );
        //                ViewBag.Message = "";
        //                ViewBag.Message1 = "";
        //                ViewBag.Message2 = "";
        //                ViewBag.Message3 = "";
        //                ViewBag.ShowDiv = false;
        //                return View(await wash.ToListAsync());
        //            }
        //            else
        //            {
        //                var wash = from m in _context.WashcoBoxes
        //                           select m;
        //                wash = wash.Where(s => s.OrderNo == searchString && s.Person == Person2 );
        //                ViewBag.Message = cus.CusConNo;
        //                ViewBag.Message1 = cus.CusName;
        //                ViewBag.Message2 = cus.CusMobile1;
        //                ViewBag.Message3 = cus.CusSalesman;
        //                ViewBag.ShowDiv = true;
        //                return View(await wash.ToListAsync());

        //            }

        //        }
        //       if (!string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(Person2) & !string.IsNullOrEmpty(Section2))
        //        {
        //            ViewBag.SearchText = searchString;

        //            var cus = await _context.CusData.FirstOrDefaultAsync(m => m.CusConNo == searchString);
        //            if (cus == null)
        //            {
        //                var wash = from m in _context.WashcoBoxes
        //                           select m;
        //                wash = wash.Where(s => s.OrderNo == searchString  && s.Section == Section2);
        //                ViewBag.Message = "";
        //                ViewBag.Message1 = "";
        //                ViewBag.Message2 = "";
        //                ViewBag.Message3 = "";
        //                ViewBag.ShowDiv = false;
        //                return View(await wash.ToListAsync());
        //            }
        //            else
        //            {
        //                var wash = from m in _context.WashcoBoxes
        //                           select m;
        //                wash = wash.Where(s => s.OrderNo == searchString  && s.Section == Section2);
        //                ViewBag.Message = cus.CusConNo;
        //                ViewBag.Message1 = cus.CusName;
        //                ViewBag.Message2 = cus.CusMobile1;
        //                ViewBag.Message3 = cus.CusSalesman;
        //                ViewBag.ShowDiv = true;
        //                return View(await wash.ToListAsync());

        //            }

        //        }



        //        ViewBag.SearchText = "";
        //        ViewBag.Searchselect1 = "";
        //        ViewBag.Searchselect2 = "";


        //        ViewBag.ShowDiv = false;
        //        ViewBag.Message = "";
        //        ViewBag.Message1 = "";
        //        ViewBag.Message2 = "";
        //        ViewBag.Message3 = "";
        //        var washcoDbContext = _context.WashcoBoxes;//.Include(w => w.CusNoNavigation);
        //        return View(await washcoDbContext.ToListAsync());


        //}

        [HttpGet(Name = "Index")]
        public async Task<IActionResult> Index([FromQuery]string searchString, [FromQuery] string Person2, [FromQuery] string Section2, [FromQuery] PagingDto paging)
        {
            if (log == 1)
            {
                ViewBag.Showlogs = true;
            }
            else
            {
                ViewBag.Showlogs = false;
            }
            ViewBag.Searchselect1 = Person2;
            ViewBag.Searchselect2 = Section2;
            if (Person2 == "All Persons") { Person2 = ""; }
            if (Section2 == "All Sections") { Section2 = ""; }
            ViewBag.SearchText = "";
        
            ViewBag.ShowDiv = false;
            ViewBag.Message = "";
            ViewBag.Message1 = "";
            ViewBag.Message2 = "";
            ViewBag.Message3 = "";

            var wash2 = _context.WashcoBoxes.AsQueryable();
            wash2 = wash2.Where(x => x.Id > 0);
            if (!string.IsNullOrEmpty(searchString))
            {
                ViewBag.SearchText = searchString;
                var cus = await _context.CusData.FirstOrDefaultAsync(m => m.CusConNo == searchString);
                if (cus != null)
                {
                    ViewBag.Message = cus.CusConNo;
                    ViewBag.Message1 = cus.CusName;
                    ViewBag.Message2 = cus.CusMobile1;
                    ViewBag.Message3 = cus.CusSalesman;
                    ViewBag.ShowDiv = true;
                }
                    wash2 = wash2.Where(x => x.OrderNo == searchString);
            }
            if (!string.IsNullOrEmpty(Person2))
            {
                wash2 = wash2.Where(x => x.Person == Person2);
            }
            if (!string.IsNullOrEmpty(Section2))
            {
                wash2 = wash2.Where(x => x.Section == Section2);
            }

            int TotalPage = (int)(Math.Ceiling((double)(wash2.Count() / paging.RowCount)));

            var PDet = new List<PagingDetals>();
            string lnk = "";
            for (int i=0;i< TotalPage; i++)
            {
                lnk = Url.Action ("Index", new
                {
                    searchString,
                    Person2,
                    Section2,
                    RowCount = paging.RowCount,
                    PageNumber = i + 1
                });
                PDet.Add( new PagingDetals {PageUrl= lnk,CurPage= i + 1 }  ); 
            }

            ViewBag.TotalPage  = (int)(Math.Ceiling((double)(wash2.Count() / paging.RowCount)));
            ViewBag.pdetl = PDet;
            ViewBag.CusPage = paging.PageNumber;

            wash2 =  wash2.Skip((paging.PageNumber - 1) * paging.RowCount).Take(paging.RowCount);
            

            ViewBag.Searchselect1 = Person2;
            ViewBag.Searchselect2 = Section2;


          


       
            return View(await wash2.ToListAsync());


        }



        [HttpGet]
        public async Task<IActionResult> Login(string UserName,string PassWord)
        {
            var User = await _context.WashcoBoxUsers.FirstOrDefaultAsync(m => m.UserName == UserName && m.PassWord==PassWord);

            //var User = from m in _context.WashcoBoxUsers select m;
            //User = User.Where(s => s.UserName == UserName && s.PassWord== PassWord);
            log = 0; 
            if (User != null)
            {
                log = 1;
            }
            return RedirectToAction(nameof(Index));
 
        }

        [HttpPost]
        public async Task<IActionResult> Create2(  string Person2, string Section2, string Notee2, string OrderNo,IFormFile ImageFile)
        {
            string FilePath2 = "" ; 
            if (ImageFile != null)
            { // Upload File
                String imagefolder = Path.Combine(hosting.WebRootPath, "Images");
                string imagepath = Path.Combine(imagefolder, ImageFile.FileName);
               // ImageFile.CopyTo(new FileStream(imagepath, FileMode.Create));
                FilePath2 = ImageFile.FileName;

                 await Upload(ImageFile); 
         
                    //
                    FilePath2 = "http://breaktime-sa.com//WashcoBox/" + ImageFile.FileName;

                    DateTime DateAdd2 = DateTime.Now;
                    var tt = new WashcoBox { FilePath = FilePath2, DateAdd = DateAdd2, Person = Person2, Section = Section2, Notee = Notee2, OrderNo = OrderNo };

                    if (ModelState.IsValid)
                    {
 
                        _context.Add(tt);
                        await _context.SaveChangesAsync();
                        var washcoDbContext = _context.WashcoBoxes;//.Include(w => w.CusNoNavigation);
                                                                   //return View(await washcoDbContext.ToListAsync());
                        return RedirectToAction(nameof(Index));
                    }
              


            }
            return RedirectToAction(nameof(Index));

           

        }
        public async Task<bool> Upload(IFormFile ImageFile)
{




 




            //Create an FtpWebRequest
            var request = (FtpWebRequest)WebRequest.Create("ftp://ftp.breaktime-sa.com/" + ImageFile.FileName);
            //Set the method to UploadFile
            request.UseBinary = true;
            request.UsePassive = true;
            request.KeepAlive = true;

            request.Method = WebRequestMethods.Ftp.UploadFile;
            //Set the NetworkCredentials
            request.Credentials = new NetworkCredential("WAshcoDropBox@breaktime-sa.com", ";?Ae*x{~Ekon");

 

            //Set buffer length to any value you find appropriate for your use case
            byte[] buffer = new byte[1024];
            var stream = ImageFile.OpenReadStream();
            byte[] fileContents;
            //Copy everything to the 'fileContents' byte array
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                    
                }
                fileContents = ms.ToArray();
                
            }


 


            //Upload the 'fileContents' byte array 
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }

            //Get the response
          
            var response = (FtpWebResponse)request.GetResponse();
            return response.StatusCode == FtpStatusCode.FileActionOK;

        }

 

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {

            var washcoBox = await _context.WashcoBoxes.FindAsync(id);

            _context.WashcoBoxes.Remove(washcoBox);
            await _context.SaveChangesAsync();
            ;

            return Json(new { redirectToUrl = Url.Action("Index", "Washco") });

        }



        [HttpGet]
        public PartialViewResult GetDeletePartial(int id)
        {

            var deleteItem = _context.WashcoBoxes.Find(id);  // I'm using 'Items' as a generic term for whatever item you have

            return PartialView("Delete", deleteItem);  // assumes your delete view is named "Delete"
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var washcoBox = await _context.WashcoBoxes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (washcoBox == null)
            {
                return NotFound();
            }

            return View(washcoBox);
        }

        // POST: Washco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var washcoBox = await _context.WashcoBoxes.FindAsync(id);
            _context.WashcoBoxes.Remove(washcoBox);
            await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
            //return Json(new { redirectToUrl = Url.Action("Index", "Washco") });

        }

        private bool WashcoBoxExists(long id)
        {
            return _context.WashcoBoxes.Any(e => e.Id == id);
        }



















    }
}
