using Humanizer.Localisation;
using IGDB.Models;
using Microsoft.AspNetCore.Authorization;
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
        if (User.Identity.IsAuthenticated) ViewBag.IsGameInLibrary = dal.IsGameInLibrary(User.FindFirstValue(ClaimTypes.NameIdentifier), id);
        return View( game );
    
    }


    public IActionResult Privacy() {

        return View();

    }

    public IActionResult Search() {

        return View();

    }
    [Authorize]
    public IActionResult Library( string? UserId ) {

        if ( UserId == null ) UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var model = dal.GetGameDataByUserId( UserId );

        return View( model );

    }

    public IActionResult Friends( string? UserId )
    {
        if (UserId == null) UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var model = dal.GetFriendsProfileData(UserId);
        ViewBag.NotAccepted = false;
        return View("UserList", model);

    }
    [Authorize]
    public IActionResult FriendRequests(string? UserId)
    {
        if (UserId == null) UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var model = dal.GetFriendRequests(UserId);
        ViewBag.NotAccepted = true;
        return View("UserList", model);
    }
    [Authorize]
    public IActionResult PendingRequests(string? UserId)
    {
        if (UserId == null) UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var model = dal.GetPendingFriends(UserId);
        ViewBag.Pending = true;
        return View("UserList", model);
    }
    public IActionResult Profile ( string? UserId )
    {
        if (UserId == null) UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var model = dal.GetProfileData(UserId);
        return View(model);
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
    [Authorize]
    public IActionResult AddGameToLibrary( long gameId )
    {
        dal.AddGameToLibrary(User.FindFirstValue(ClaimTypes.NameIdentifier), gameId);
        return RedirectToAction("GameDetails", new { gameId = gameId });
    }

    public IActionResult LaunchGame ( int steamId )
    {
        return Redirect("steam://launch/" + steamId);
    }
    [Authorize]
    public IActionResult AddFriend( string userId )
    {
        dal.AddFriend(User.FindFirstValue(ClaimTypes.NameIdentifier), userId);
        return RedirectToAction("Profile", new { userId = userId });
    }
    [Authorize]
    public IActionResult AcceptFriend(string userId)
    {
        dal.AcceptFriendship(User.FindFirstValue(ClaimTypes.NameIdentifier), userId);
        return RedirectToAction("Profile", new { userId = userId });
    }

    public IActionResult UserSearch(string search)
    {
        var model = dal.SearchProfilesByUsername(search);
        ViewBag.PendingFriend = false;
        return View("UserList", model);
    }
    [Authorize]
    public IActionResult AddToLibrary(long id)
    {
        dal.AddGameToLibrary(User.FindFirstValue(ClaimTypes.NameIdentifier), id);
        return RedirectToAction("GameDetails", new { id = id });
    }
    [Authorize]
    public IActionResult RemoveFromLibrary(long id)
    {
        dal.RemoveFromLibrary(User.FindFirstValue(ClaimTypes.NameIdentifier), id);
        return RedirectToAction("GameDetails", new { id = id });
    }
    [Authorize]
    public IActionResult SetFavoriteGame(long id)
    {
        dal.SetFavoriteGame(User.FindFirstValue(ClaimTypes.NameIdentifier), id);
		return RedirectToAction("GameDetails", new { id = id });
	}
    [HttpGet, Authorize]
    public IActionResult EditProfile(string userId)
    {
        var model = dal.GetProfile(userId);
        if (model == null) return RedirectToAction("Index", "Home");
        return View(model);
    }
    [HttpPost, Authorize]
    public IActionResult EditProfile(Profile profile)
    {
        if (ModelState.IsValid)
        {
            dal.UpdateProfile(profile);
            return RedirectToAction("Profile", new { userId = profile.UserId });
        }
        return View(profile);
    }

}