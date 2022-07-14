#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lecolesAI.Data;
using lecolesAI.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace lecolesAI.Controllers
{
    public class LecolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecolesController(ApplicationDbContext context)
        {
            _context = context;
        }
        #region API CALLS
        [HttpGet]
        // GET: Lecoles
        public async Task<IActionResult> Index()
        {

         
            return View(await _context.courses.ToListAsync());
        }
        #endregion

        // GET: Lecoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courses = await _context.courses
                .FirstOrDefaultAsync(m => m.index == id);
            if (courses == null)
            {
                return NotFound();
            }

            return View(courses);
        }

        // GET: Lecoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lecoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("index,course_id,course_title,url,price,num_subscribers,num_reviews,num_lectures,level,content_duration,published_timestamp,subject")] courses courses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courses);
        }

        // GET: Lecoles/Edit/5
        public async Task<IActionResult>Edit(int? id)
        {


            List<int> model = new List<int>();
            var title = _context.courses.FirstOrDefault(u => u.index == id);
            var title2 = title.course_title;
            ViewBag.find = title2;
	        List<string> reccourse = new List<string>();
            HttpClient client = new HttpClient();

            var viewModelt = new courses();
            var recommendedcourses = new List<viewModel>();
            var content = new MultipartFormDataContent();
        //var url = http://localhost/Recommend_Courses/;
        /* var uploadRequestResult =
             client.GetAsync(
                     $"{"http://localhost/Recommend_Courses/How%20To%20Maximize%20Your%20Profits%20Trading%20Options"}")
                 .Result;
                 */
    
            var uploadRequestResult = client.GetAsync("https://lecoles.azurewebsites.net/Recommend_Courses/" + title2).Result;

            if (uploadRequestResult.IsSuccessStatusCode)
            {

                var tes = uploadRequestResult.Content.ReadAsStringAsync().Result;



                for (int i = 0; i < 10; i++)
                {
                    var f = JsonConvert.DeserializeObject<dynamic>(tes)[i]["course_id"].Value;

                    var t = int.Parse(f);
                    model.Add(t);
                }





                foreach (var a in model)
                {
                    var courses1 = _context.courses.FirstOrDefault(u => u.course_id == a);
                    
                 
                    
                    if (courses1 == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        /*viewModelt = new()
                        {
                            url = ViewBag.courses.url,
                            course_title = ViewBag.courses.course_title, 
                       
                
                        };
                        */
                        
                        recommendedcourses.Add(new viewModel() { course_title = courses1.course_title, url = courses1.url,price=courses1.price });
                 

                    }
                }


                ViewBag.recommended = recommendedcourses;




            }
           
            return View();
        }

        // POST: Lecoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       

        // GET: Lecoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var courses = await _context.courses
                .FirstOrDefaultAsync(m => m.index == id);
            if (courses == null)
            {
                return NotFound();
            }

            return View(courses);
        }

        // POST: Lecoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courses = await _context.courses.FindAsync(id);
            _context.courses.Remove(courses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool coursesExists(int id)
        {
            return _context.courses.Any(e => e.index == id);
        }
        #region API CALLS
        [HttpGet]
        // GET: Lecoles
        public async Task<IActionResult> GetCourses()
        {

            var courselist = await _context.courses.ToListAsync();
           
            return Json(new { data = courselist });
        }
        #endregion
    }

}
