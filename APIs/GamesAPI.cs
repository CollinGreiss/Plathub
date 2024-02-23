using System.Dynamic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using IGDB;
using IGDB.Models;
using Plathub.Models;
using static Plathub.Data.GenreData;

namespace Plathub.APIs;

public class GamesAPI {

	// https://apicalypse.io/syntax/
	// https://github.com/kamranayub/igdb-dotnet/blob/master/README.md
	// https://api-docs.igdb.com/#getting-started

	public static async Task<Game[]> SearchGames( string search ) {

		var igdb = new IGDBClient( "dc89wkvj5vk53gddargvkmbml0k19o", "qsoyhxogfxuyl92px7gy56qpbjdaiz" );

		var games = await igdb.QueryAsync<Game>( IGDBClient.Endpoints.Games, query: $"fields *; search \"{search}\"; where category = 0; " );
		return games;

	}

	public static async Task<Game[]> SearchGames( GameGenre[] genres ) {

		var igdb = new IGDBClient( "dc89wkvj5vk53gddargvkmbml0k19o", "qsoyhxogfxuyl92px7gy56qpbjdaiz" );

		string genreQuery = "(" + (int) genres[0];

		for ( int i = 1; i < genres.Length; i++ ) {

			genreQuery += (int) genres[i];

        }

        genreQuery += ")";

		var games = await igdb.QueryAsync<Game>( IGDBClient.Endpoints.Games, query: $"fields *; where genres = {genreQuery}; " );


        return games;

	}


    public static async Task<Game> GetGame( int id ) {

		var igdb = new IGDBClient( "dc89wkvj5vk53gddargvkmbml0k19o", "qsoyhxogfxuyl92px7gy56qpbjdaiz" );

		var games = await igdb.QueryAsync<Game>( IGDBClient.Endpoints.Games, query: $"fields *; where id = {id}; " );
		return games[0];

	}

	public static async Task<PlatformWebsite> GetWebsites( int id ) {

		var igdb = new IGDBClient( "dc89wkvj5vk53gddargvkmbml0k19o", "qsoyhxogfxuyl92px7gy56qpbjdaiz" );

		var website = await igdb.QueryAsync<PlatformWebsite>( IGDBClient.Endpoints.PlatformWebsites, query: $"fields *; where id = {id}; " );
		return website[0];

	}

	public static async Task<int> GetSteamID( Game game ) {

		var igdb = new IGDBClient( "dc89wkvj5vk53gddargvkmbml0k19o", "qsoyhxogfxuyl92px7gy56qpbjdaiz" );

		foreach ( var id in game.ExternalGames.Ids ) {

			var test = await igdb.QueryAsync<ExternalGame>( IGDBClient.Endpoints.ExternalGames, query: $"fields *; where id = {id}; " );

			if ( test == null || test.Length == 0 ) continue;

			var data = test[0];

			if ( data.Category != ExternalCategory.Steam ) continue;

			return Int32.Parse( data.Uid );

		}

		return -1;

	}

}
