using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameList
{
    public class Games : IComparable
    {
        [Key]
        public int GameID { get; set; }
        public string GameName { get; set; }
        public string GameDescription { get; set; }
        public decimal GamePrice { get; set; }
        public decimal GameRating { get; set; }
        public List<string> GameGenres { get; set; }
        public string ImageURL { get; set; }

        public int CompareTo(object obj)
        {
            Games that = obj as Games;

            return this.GameName.CompareTo(that.GameName);
        }

        public override string ToString()
        {
            return string.Format($"{GameName} - {GamePrice:c}");
        }
    }

    public class GameData : DbContext
    {
        public GameData() : base("GameDataBase") { }

        public DbSet<Games> Games { get; set; }
    }
}
