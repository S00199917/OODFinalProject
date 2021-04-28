using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameData db = new GameData();
        List<Games> games = new List<Games>(), trolly = new List<Games>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from games in db.Games
                        select games;

            games = query.ToList();
            games.Sort();

            lbxGameList.ItemsSource = games;
        }

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            Games game = lbxGameList.SelectedItem as Games;

            if (game != null)
            {
                if (!trolly.Contains(game))
                    trolly.Add(game);
            }

            trolly.Sort();

            lbxTrolly.ItemsSource = null;
            lbxTrolly.ItemsSource = trolly;
            lbxLibraryList.ItemsSource = null;
            lbxLibraryList.ItemsSource = trolly;
        }

        private void tbxSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxSearch.Text = "";
        }

        private void lbxLibraryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Games selectedGame = lbxLibraryList.SelectedItem as Games;

            if (selectedGame != null)
            {
                imgGameImage.Source = new BitmapImage(new Uri(selectedGame.ImageURL));
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<Games> searchedGame = new List<Games>();

            if (tbxSearch.Text == null || tbxSearch.Text == "" || tbxSearch.Text == "Search")
                MessageBox.Show("You must enter a game name in the search box");
            else
            {
                var query= from games in db.Games
                               where games.GameName == tbxSearch.Text
                               select games;

                searchedGame = query.ToList();
            }

            lbxLibraryList.ItemsSource = searchedGame;
        }
    }
}
