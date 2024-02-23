using Microsoft.AspNetCore.Mvc;
using Plathub.APIs;
using Plathub.Data;
using Plathub.Models;
using System.Diagnostics;

namespace Plathub.Controllers;
public class HomeController : Controller {

    private readonly ILogger<HomeController> _logger;

    public HomeController( ILogger<HomeController> logger ) {

        _logger = logger;

    }

    public IActionResult Index() {



        return View();

    }

    public IActionResult Privacy() {

        return View();

    }

    public IActionResult Search() {

        return View();

    }

    public IActionResult FindGame( string searchQuery, string genre, string? platform ) {

        var task = GamesAPI.SearchGames( GenreData.GetGenres( Int32.Parse( genre ) ) );
        task.Wait();
        var games = task.Result;

        return View();

    }

    [ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
    public IActionResult Error() {

        return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );

    }

}