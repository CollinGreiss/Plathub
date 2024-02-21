using Microsoft.AspNetCore.Mvc;
using Plathub.APIs;
using Plathub.Models;
using System.Diagnostics;

namespace Plathub.Controllers;
public class HomeController : Controller {

	private readonly ILogger<HomeController> _logger;

	public HomeController( ILogger<HomeController> logger ) {
    
		_logger = logger;

	}

	public IActionResult Index() {

		var search = GamesAPI.SearchGames("Code Vein");
		search.Wait();
		var games = search.Result;

		var search2 = GamesAPI.GetSteamID( games[0] );
		search2.Wait();
		var id = search2.Result;



		return View();

	}

	public IActionResult Privacy() {

		return View();

	}
  
  public IActionResult Search() {
    
    return View();
    
  }

	[ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
	public IActionResult Error() {

		return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );

	}

}