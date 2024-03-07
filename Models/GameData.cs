using IGDB.Models;
using Microsoft.CodeAnalysis;
using Plathub.APIs;
using Plathub.Data;
using System.Linq;
using System.Threading.Tasks;
using static Plathub.Models.GenreData;
using static Plathub.Models.PlatformData;

namespace Plathub.Models;

public class GameData {

	public long id;

	private int? SteamID;
	public int? steamID {


		get {

			if ( SteamID != null && SteamID == -1 ) return null;

			var task = GamesAPI.GetSteamID( game );
			task.Wait();
			SteamID = task.Result;
			if ( SteamID == -1 ) return null;
			return SteamID;

		}
		set {

			SteamID = value;

		}

	}

	public string? year;
	public GameGenre[] genres;
	public GamePlatform[] platforms;
	private Game game;

	public string title;
	public string image;
	public string description;
	public string platformsName = "";
	public string genresName = "";

	private GameData[]? Dlcs = null;
	private long[]? dlcIds;
	public GameData[]? dlcs {


		get {

			if ( Dlcs != null && Dlcs.Length > 0 ) return Dlcs;
			if ( Dlcs != null ) return null;

			if ( dlcIds == null ) {

				Dlcs = new GameData[0];
				return null;

			}

			var dlcList = new List<GameData>();

			foreach ( var Id in dlcIds ) {

				var dlcTask = GamesAPI.GetGame( (int) Id );
				dlcTask.Wait();
				var dlc = dlcTask.Result;
				if (dlc != null ) dlcList.Add( new GameData( dlc ) );

			}

			Dlcs = dlcList.ToArray();
			if ( Dlcs.Length == 0 ) return null;

			return Dlcs;

		}
		set {

			Dlcs = value;

		}

	}


	public GameData( Game game ) {

		this.game = game;
		if ( game.Id == null ) return;
		id = (long) game.Id;

		description = game.Summary;

		title = game.Name;
		if ( game.Cover != null ) {

			var CoverTask = GamesAPI.GetCover( (long) game.Cover.Id );
			CoverTask.Wait();
			var cover = CoverTask.Result;

			image = cover.Url;


		} else image = "https://i.kym-cdn.com/entries/icons/original/000/028/315/cover.jpg";

		if ( game.FirstReleaseDate != null ) {

			year = game.FirstReleaseDate.Value.ToString( "dd/MM/yyyy " );

		}

		if ( game.Genres != null ) {

			var genresList = new List<GameGenre>();

			foreach ( var genre in game.Genres.Ids ) {

				if ( genre != null ) {

					genresList.Append( (GameGenre) genre );
					genresName += ( (GamePlatform) genre ).ToString() + ", ";

				}

			}

			genres = genresList.ToArray();
			if ( genresName.Length > 0 ) genresName = genresName.Substring( 0, genresName.Length - 2 );

		}

		if ( game.Platforms != null ) {

			var platformList = new List<GamePlatform>();

			foreach ( var platform in game.Platforms.Ids ) {

				if ( platform != null ) {

					platformList.Append( (GamePlatform) platform );
					platformsName += ( (GamePlatform) platform ).ToString() + ", ";

				}
			}

			dlcIds = game.Dlcs?.Ids;

			platforms = platformList.ToArray();
			if ( platformsName.Length > 0 ) platformsName = platformsName.Substring( 0, platformsName.Length - 2 );

		}


	}

}