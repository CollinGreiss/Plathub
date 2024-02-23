namespace Plathub.Data;

using IGDB;
using IGDB.Models;
using Plathub.APIs;

public class Game {

    long ID;
    string? ReleaseDate;
    Genre[] genres;
    string? Rating;
    string? Publisher;


    int steamID;
    

    Game( IGDB.Models.Game game ) {

        if ( game.Id == null ) return;
        ID = (long) game.Id;

        ReleaseDate = game.ReleaseDates.Values.FirstOrDefault().Date.ToString();

        var task = GamesAPI.GetSteamID( game );
        task.Wait();
        steamID = task.Result;

        genres = game.Genres.Values;
        

    }

}
