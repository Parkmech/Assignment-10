using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SharpenTheSaw.Models;

namespace SharpenTheSaw.Components
{
    public class TeamNameViewComponent : ViewComponent
    {
        private BowlingLeagueContext context;

        public TeamNameViewComponent(BowlingLeagueContext ctx)
        {
            context = ctx;
        }

        public IViewComponentResult Invoke()
        {
            //Returns the info so the selected page will be highlighted
            ViewBag.Selected = RouteData?.Values["teamName"];

            return View(context.Teams
               .Distinct()
               .OrderBy(x => x));   
        }
    }
}
