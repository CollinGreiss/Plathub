using Humanizer.Localisation;
using IGDB.Models;
using Microsoft.AspNetCore.Mvc;
using Plathub.APIs;
using Plathub.Data;
using Plathub.Interfaces;
using Plathub.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Plathub.Controllers;

public class HomeController : Controller {

    IDataAccessLayer dal;

    public HomeController( IDataAccessLayer dal ) {

        this.dal = dal;
    }

    public IActionResult Index() {

        return View();

    }

    public IActionResult GameDetails( int id ) {

        var task = GamesAPI.GetGame(id);
        task.Wait();
        var game = new GameData( task.Result );

        if ( game == null ) {

            return NotFound();

        }

        return View( game );
    
    }


    public IActionResult Privacy() {

        return View();

    }

    public IActionResult Search() {

        return View();

    }
    public IActionResult Library( Game?[] games ) {

        /*List<GameData> model = new List<GameData>();

		foreach ( Game game in games ) {
		
			model.Add( new GameData(game) );
		
		}*/

        var model = dal.GetGameDataByUserId( User.FindFirstValue( ClaimTypes.NameIdentifier ) );

        return View( model );

    }

    public IActionResult FindGame( string searchQuery, string? genre, string? platform ) {

        var genres = ( genre == null ) ? null : GenreData.GetGenres( Int32.Parse( genre ) );
        var platforms = ( platform == null ) ? null : PlatformData.GetPlatforms( Int32.Parse( platform ) );

        var task = GamesAPI.SearchGames( searchQuery, genres, platforms, 25 );
        task.Wait();
        var games = task.Result;

        List<GameData> model = new List<GameData>();

        foreach ( Game game in games ) {

            model.Add( new GameData( game ) );

        }

        return View( "Library", model );

    }

    [ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
    public IActionResult Error() {

        return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );

    }

}