using System.Dynamic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using IGDB;
using IGDB.Models;
using Plathub.Models;
using static Plathub.Models.GenreData;
using static Plathub.Models.PlatformData;

namespace Plathub.APIs;

public class GamesAPI {

	// https://apicalypse.io/syntax/
	// https://github.com/kamranayub/igdb-dotnet/blob/master/README.md
	// https://api-docs.igdb.com/#getting-started

	private static IGDBClient igdb = new IGDBClient( "dc89wkvj5vk53gddargvkmbml0k19o", "qsoyhxogfxuyl92px7gy56qpbjdaiz" );

	public static async Task<Game[]> SearchGames(string search, GameGenre[]? genres, GamePlatform[]? platform, int limit = 10 ) {

		var filter = $"fields id, name, summary, cover, platforms, genres, dlcs, first_release_date; limit {limit};";

		if ( search == null || search.Length > 0) filter += $" search \"{search}\";";

		if (genres != null) filter += " where " + GenreData.GetGenreQuery( genres );
		
		if ( platform == null && genres != null ) filter += ";";
		else if ( platform != null && genres != null ) filter += " & " + PlatformData.GetPlatformQuery( platform ) + ";";
		else if ( platform != null && genres == null ) filter += "where " + PlatformData.GetPlatformQuery( platform ) + ";";

		var games = await igdb.QueryAsync<Game>( IGDBClient.Endpoints.Games, query: filter );
		return games;

	}

	public static async Task<Game[]> SearchGames( string search ) {

		var games = await igdb.QueryAsync<Game>( IGDBClient.Endpoints.Games, query: $"fields *; search \"{search}\"; where category = 0; " );
		return games;

	}

	public static async Task<Game[]> SearchGames( GameGenre[] genres ) {

		string genreQuery = GenreData.GetGenreQuery( genres );

		var games = await igdb.QueryAsync<Game>( IGDBClient.Endpoints.Games, query: $"fields *; where {genreQuery}; " );


        return games;

	}

	public static async Task<Game> GetGame( int id ) {

		var games = await igdb.QueryAsync<Game>( IGDBClient.Endpoints.Games, query: $"fields *; where id = {id}; " );
		return games[0];

	}

	public static async Task<PlatformWebsite> GetWebsites( int id ) {

		var website = await igdb.QueryAsync<PlatformWebsite>( IGDBClient.Endpoints.PlatformWebsites, query: $"fields *; where id = {id}; " );
		return website[0];

	}

	public static async Task<int> GetSteamID( Game game ) {

		if ( game.ExternalGames == null || game.ExternalGames.Ids == null || game.ExternalGames.Ids.Length == 0 ) return -1;

		foreach ( var id in game.ExternalGames.Ids ) {

			var test = await igdb.QueryAsync<ExternalGame>( IGDBClient.Endpoints.ExternalGames, query: $"fields *; where id = {id}; " );

			if ( test == null || test.Length == 0 ) continue;

			var data = test[0];

			if ( data.Category != ExternalCategory.Steam ) continue;

			return Int32.Parse( data.Uid );

		}

		return -1;

	}

	public static async Task<Cover> GetCover( long id ) {

		var cover = await igdb.QueryAsync<Cover>( IGDBClient.Endpoints.Covers, query: $"fields *; where id = {id}; " );
		return cover[0];

	}

    public static async Task<ReleaseDate> GetReleaseDate( long? id ) {
        
		var date = await igdb.QueryAsync<ReleaseDate> (IGDBClient.Endpoints.ReleaseDates, query: $"fields *; where id = {id}; " );
		return date[0];

    }

}
