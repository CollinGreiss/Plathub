using IGDB.Models;
using Plathub.APIs;
using Plathub.Data;

namespace Plathub.Models;

public class GameData {

	//IEnumerable<GenreData.GameGenre> Genres;

	public long ID;
	public string Title;
	public string Image;
	public int? SteamID;


	public GameData( Game game ) {

		if ( game.Id == null ) return;
		ID = (long) game.Id;

		Title = game.Name;
		if ( game.Cover != null ) {

			var CoverTask = GamesAPI.GetCover( (long) game.Cover.Id );
			CoverTask.Wait();
			var cover = CoverTask.Result;

			Image = cover.Url;
			

		}
		else Image = "https://i.kym-cdn.com/entries/icons/original/000/028/315/cover.jpg";

		//ReleaseDate = game.ReleaseDates.Values.FirstOrDefault().Date.ToString();

		var task = GamesAPI.GetSteamID( game );
		task.Wait();

		SteamID = task.Result;
		if ( SteamID == -1 ) SteamID = null;


		/*foreach ( var genre in game.Genres.Values ) {

			//if ( genre.Id != null)
				//genres.Append( (GenreData.GameGenre) genre.Id );

		}*/

	}

}
