using IGDB.Models;
using Plathub.APIs;
using Plathub.Data;
using System.Linq;
using static Plathub.Models.GenreData;
using static Plathub.Models.PlatformData;

namespace Plathub.Models;

public class GameData {

	public long id;
	public string title;
	public string image;
	public int? steamID;
	public string? releaseDate;
	public string? year;
	public GameGenre[] genres;
	public GamePlatform[] platforms;


    public GameData( Game game ) {

		if ( game.Id == null ) return;
		id = (long) game.Id;

        var task = GamesAPI.GetSteamID( game );

        title = game.Name;
		if ( game.Cover != null ) {

			var CoverTask = GamesAPI.GetCover( (long) game.Cover.Id );
			CoverTask.Wait();
			var cover = CoverTask.Result;

			image = cover.Url;
			

		}
		else image = "https://i.kym-cdn.com/entries/icons/original/000/028/315/cover.jpg";

		year = game.ReleaseDates.Ids.FirstOrDefault().ToString();

        if (game.Genres != null ) {

            var genresList = new List<GameGenre>();

            foreach ( var genre in game.Genres.Ids ) {

                if ( genre != null )
                    genresList.Append( (GameGenre) genre );

            }

            genres = genresList.ToArray();

        }

		if ( game.Platforms != null ) {

            var platformList = new List<GamePlatform>();

            foreach ( var platform in game.Platforms.Ids ) {

                if ( platform != null )
                    platformList.Append( (GamePlatform) platform );

            }

            platforms = platformList.ToArray();

        }

        task.Wait();

		steamID = task.Result;
		if ( steamID == -1 ) steamID = null;


    }

}
