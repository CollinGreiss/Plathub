using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using IGDB;
using IGDB.Models;

namespace Plathub.APIs;

public class Games {

	// https://apicalypse.io/syntax/
	// https://github.com/kamranayub/igdb-dotnet/blob/master/README.md
	// https://api-docs.igdb.com/#getting-started

	public static async Task GetTask() {

		var igdb = new IGDBClient( "dc89wkvj5vk53gddargvkmbml0k19o", "qsoyhxogfxuyl92px7gy56qpbjdaiz" );

		var games = await igdb.QueryAsync<Game>( IGDBClient.Endpoints.Games, query: "fields *; where id = 76239; " );
		foreach ( var game in games ) {

			Console.WriteLine( game.Name );

		}

	}

}
