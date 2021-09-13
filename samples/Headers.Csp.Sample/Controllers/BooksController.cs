using Galebra.Security.Headers.Csp;
using Microsoft.AspNetCore.Mvc;

namespace Headers.Csp.Sample.Controllers;

[DisableCsp(EnforceMode = false)]
public class BooksController : Controller
{
    public ActionResult Index()
    {
        //CSP is disabled here
        return View();
    }

    // GET: BooksController/Details/5
    [EnableCsp(PolicyGroupName = "PolicyGroup3")]
    public ActionResult Details(int id)
    {
        //CSP works here owing to EnforceMode=false
        return View();
    }
}
