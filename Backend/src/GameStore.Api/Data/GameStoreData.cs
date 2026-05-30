using GameStore.Api.Models;

namespace GameStore.Api.Data
{
    public class GameStoreData
    {
        private readonly List<Genre> genres = new List<Genre>
        {
            new Genre { Id = new Guid("01522711-15d7-4218-9f03-a82d5ddab63e"), Name = "RPG" },
            new Genre { Id = new Guid("01522711-15d7-4218-9f03-a82d5ddab63f"), Name = "Action RPG" },
            new Genre { Id = new Guid("01522711-15d7-4218-9f03-a82d5ddab640"), Name = "Action-adventure" }
        };

        private readonly List<Game> games;

        public GameStoreData()
        {
            games = new List<Game>
            {
                new Game
                {
                    Id = Guid.NewGuid(),
                    Name = "The Witcher 3: Wild Hunt",
                    Genre = genres[0],
                    Price = 59.99m,
                    ReleaseDate = new DateOnly(2015, 5, 19),
                    Description = "The Witcher 3: Wild Hunt is an action role-playing game developed and published by CD Projekt. It is the third installment in The Witcher series and is based on the book series of the same name by Polish author Andrzej Sapkowski. The game follows the story of Geralt of Rivia, a monster hunter known as a Witcher, as he searches for his adopted daughter Ciri, who is on the run from the Wild Hunt, a group of spectral riders. The game features an open world environment, a branching narrative with multiple endings, and a combat system that combines swordplay and magic."
                },
                new Game
                {
                    Id = Guid.NewGuid(),
                    Name = "Elden Ring",
                    Genre = genres[1],
                    Price = 59.99m,
                    ReleaseDate = new DateOnly(2022, 2, 25),
                    Description = "Elden Ring is an action role-playing game developed by FromSoftware and published by Bandai Namco Entertainment. It is directed by Hidetaka Miyazaki and produced by George R.R. Martin."
                },
                new Game
                {
                    Id = Guid.NewGuid(),
                    Name = "Cyberpunk 2077",
                    Genre = genres[2],
                    Price = 59.99m,
                    ReleaseDate = new DateOnly(2020, 12, 10),
                    Description = "Cyberpunk 2077 is an action role-playing video game developed by CD Projekt RED and published by Bandai Namco Entertainment. It is set in the cyberpunk universe of the tabletop role-playing game Cyberpunk RED."
                }
            };
        }

        public IEnumerable<Game> GetGames() => games;

        public Game? GetGame(Guid id) => games.Find(g => g.Id == id);

        public void AddGame(Game game)
        {
            game.Id = Guid.NewGuid();
            games.Add(game);
        }

        public void UpdateGame(Game game)
        {
            var existingGame = games.Find(g => g.Id == game.Id);
            if (existingGame is not null)
            {
                existingGame.Name = game.Name;
                existingGame.Genre = game.Genre;
                existingGame.Price = game.Price;
                existingGame.ReleaseDate = game.ReleaseDate;
                existingGame.Description = game.Description;
            }
        }

        public void RemoveGame(Guid id)
        {
            games.RemoveAll(g => g.Id == id);
        }

        public IEnumerable<Genre> GetGenres() => genres;
        public Genre? GetGenre(Guid id) => genres.Find(g => g.Id == id);
    }
}