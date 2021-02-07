using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WashcoDropBox.Helper;
using WashcoDropBox.Models;

namespace WashcoDropBox.Controllers
{
    [Route("api/WashcoDrop")]
    [ApiController]
    public class XamarinController : ControllerBase
    {
        private readonly WashcoDbContext _context;
        public XamarinController(WashcoDbContext context)
        {
            _context = context;
        
        }

        // GET: api/Test
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WashcoBox>>> GetWashcoBoxes([FromQuery] string searchString, [FromQuery] string Person2, [FromQuery] string Section2, [FromQuery] PagingDto paging)
        {


            var wash2 = (from box in _context.WashcoBoxes
                          join  cus in _context.CusData on   box.OrderNo equals cus.CusConNo into Details
                          from m in Details.DefaultIfEmpty()

                         where box.Id > 0
                         select new WashcoBox
                         {
                            Customer_name = m.CusName,
                            DateAdd = box.DateAdd ,
                             FilePath = box.FilePath,
                              Id = box.Id ,
                               Notee  = box.Notee ,
                                OrderNo = box.OrderNo ,
                                 Person  = box.Person ,
                                  Section = box.Section ,
                         });


            //string cus_n = "" ; 
            if (Person2 == "All Persons") { Person2 = ""; }
            if (Section2 == "All Sections") { Section2 = ""; }

           //  var wash2 = _context.WashcoBoxes.AsQueryable();
            wash2 = wash2.Where(x => x.Id > 0);
            if (!string.IsNullOrEmpty(searchString))
            {
                // var cus = await _context.CusData.FirstOrDefaultAsync(m => m.CusConNo == searchString);
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
            for (int i = 0; i < TotalPage; i++)
            {
                lnk = Url.Action("Index", new
                {
                    searchString,
                    Person2,
                    Section2,
                    RowCount = paging.RowCount,
                    PageNumber = i + 1
                });
                PDet.Add(new PagingDetals { PageUrl = lnk, CurPage = i + 1 });
            }

 

            wash2 = wash2.Skip((paging.PageNumber - 1) * paging.RowCount).Take(paging.RowCount);




            var jos = await wash2.ToListAsync(); 

            
          //  return View(await wash2.ToListAsync());

            return jos;
        }
    }
}