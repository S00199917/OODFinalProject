using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameList;

namespace GamesData
{
    class Program
    {
        static void Main(string[] args)
        {
            GameData db = new GameData();

            using (db)
            {
                //SATISFACTORY
                Games g1 = new Games()
                {
                    GameID = 1,
                    GameName = "Satisfactory",
                    GameDescription = "Satisfactory is a simulation game created by the Swedish video game developer Coffee Stain Studios. It is a 3D first-person open world exploration and factory building game.",
                    GamePrice = 29.99m,
                    GameRating = 10
                };

                //ROCKET LEAGUE
                Games g2 = new Games()
                {
                    GameID = 2,
                    GameName = "Rocket League",
                    GameDescription = "Rocket League is a vehicular soccer video game developed and published by Psyonix.",
                    GamePrice = 0m,
                    GameRating = 9
                };

                //AMONG US
                Games g3 = new Games()
                {
                    GameID = 3,
                    GameName = "Among Us",
                    GameDescription = "Among Us is an online multiplayer social deduction game developed and published by American game studio Innersloth.",
                    GamePrice = 3.99m,
                    GameRating = 9
                };

                //DARK SOULS REMASTERED
                Games g4 = new Games()
                {
                    GameID = 4,
                    GameName = "Dark Souls: REMASTERED",
                    GameDescription = "Dark Souls Remastered is a remastered version of the first game in FromSoftware's Dark Souls series.",
                    GamePrice = 39.99m,
                    GameRating = 9
                };
                Console.WriteLine("Games created");

                //Add the games to the database
                db.Games.Add(g1);
                db.Games.Add(g2);
                db.Games.Add(g3);
                db.Games.Add(g4);
                Console.WriteLine("Games added");

                //Save any changes
                db.SaveChanges();
                Console.WriteLine("Database saved");
            }
        }
    }
}
