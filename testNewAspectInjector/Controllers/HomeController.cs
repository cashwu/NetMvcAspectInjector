using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MediatR;
using testNewAspectInjector.Application;
using testNewAspectInjector.Services;

namespace testNewAspectInjector.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IPersonService _personService;

        public HomeController(IMediator mediator, IPersonService personService)
        {
            _mediator = mediator;
            _personService = personService;
        }
        
        public ActionResult Index()
        {
            ViewBag.P = _personService.Get();

            return View();
        }

        public async Task<ActionResult> About()
        {
            var testResponse = await _mediator.Send(new TestRequest());

            ViewBag.P = testResponse.Person;
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}