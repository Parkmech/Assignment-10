using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharpenTheSaw.Models;
using SharpenTheSaw.Models.ViewModels;

namespace SharpenTheSaw.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //Database Context
        private BowlingLeagueContext _context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            _context = ctx;
        }

        //Index gets passed the teamId from the database
        public IActionResult Index(long? teamId, string teamName, int pageNum = 0)
        {
            //Sets the number of items on the page and which page is displayed
            int pageSize = 5;

            //Returns the data to the view
            return View(new IndexViewModel
            {
                Bowlers = (_context.Bowlers
                //LINQ METHOD
                .Where(x => x.TeamId == teamId || teamId == null)
                .OrderBy(x => x.Team.TeamName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList()),
                //SQL METHOD
                //.FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamId = {teamId} OR {teamId} IS NULL")


                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,

                    //If no team has been selected, get everything. Otherwise, only count the ones from the selected team
                    TotalNumItems = (teamId == null ? _context.Bowlers.Count() :
                        _context.Bowlers.Where(x => x.TeamId == teamId).Count())
                },
                TeamName = teamName
            }); 
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
