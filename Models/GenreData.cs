namespace Plathub.Models;
public class GenreData {

    public enum GameGenre {

        PointAndClick = 2,
        Fighting = 4,
        Shooter = 5,
        Music = 7,
        Platform = 8,
        Puzzle = 9,
        Racing = 10,
        RealTimeStrategy = 11,
        RolePlaying = 12,
        Simulator = 13,
        Sport = 14,
        Strategy = 15,
        TurnBasedStrategy = 16,
        Tactical = 24,
        QuizTrivia = 26,
        Pinball = 30,
        Adventure = 31,
        Indie = 32,
        Arcade = 33,
        VisualNovel = 34,
        CardBoardGame = 35,
        MOBA = 36

    }

    public static GameGenre[]? GetGenres( int? id ) {

		if ( id == null ) return null;
        return GetGenres( new int[] { (int) id } );

    }

    public static GameGenre[] GetGenres( int[] ids ) {

        GameGenre[] genres = new GameGenre[ids.Length];

        for ( int i = 0; i < ids.Length; i++ ) {

            genres[i] = (GameGenre) ids[i];

        }

        return genres;

    }



	public static string GetGenreQuery( GameGenre[] genres ) {

		var query = "genres = (" + (int) genres[0];

		for ( int i = 1; i < genres.Length; i++ ) {

			query += (int) genres[i];

		}

		query += ")";

		return query;

	}

}
