namespace Plathub.Data;
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

    public static GameGenre[] GetGenres( int id ) {

        return GetGenres( new int[] { id } );

    }

    public static GameGenre[] GetGenres( int[] ids ) {

        GameGenre[] genres = new GameGenre[ids.Length];

        for ( int i = 0; i < ids.Length; i++ ) {

            genres[i] = (GameGenre) ids[i];

        }

        return genres;

    }

}
