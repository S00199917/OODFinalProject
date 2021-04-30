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
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameData db = new GameData();
        List<Games> games = new List<Games>(), trolly = new List<Games>(), library = new List<Games>();
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
            library.AddRange(trolly);

            lbxTrolly.ItemsSource = null;
            lbxTrolly.ItemsSource = trolly;
            lbxLibraryList.ItemsSource = null;
            lbxLibraryList.ItemsSource = trolly;
        }

        private void btnFavorite_Click(object sender, RoutedEventArgs e)
        {
            if (lbxLibraryList.SelectedItem != null)
            {
                string data = JsonConvert.SerializeObject(lbxLibraryList.SelectedItem, Formatting.Indented);

                using (StreamWriter sw = new StreamWriter("favorite.json"))
                {
                    sw.Write(data);


                    sw.Close();
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            using (StreamReader file = File.OpenText("favorite.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject token = (JObject)JToken.ReadFrom(reader);

                imgFavorite.Source = new BitmapImage(new Uri(token.SelectToken("ImageURL").ToString()));
            }
        }

        private void btnAddNewGame_Click(object sender, RoutedEventArgs e)
        {
            string errors;
            
            errors = "You have the following errors:";
            
            bool check = true;

            decimal review, price;

            Uri uriResult;

            string Title = tbxEnterTitle.Text, Description = tbxEnterDescription.Text, Review = tbxEnterReview.Text, Price = tbxEnterPrice.Text, URL = tbxEnterURL.Text;

            if (Title == "" || Title == "Enter Title")
            {
                errors += "\nInvalid Title name";
                check = false;
            }
            
            if (Description == "" || Description == "Enter Description")
            {
                errors += "\nInvalid Description";
                check = false;
            }
            
            if (!decimal.TryParse(Review, out review))
            {
                errors += "\nInvalid Review: must be a decimal";
                check = false;
            }
            else
            {
                decimal.TryParse(Review, out review);

                if (review > 10 || review < 0)
                {
                    errors += "\nInvalid Review: must be a number between 0 or 10";
                    check = false;
                }
            }

            if(!Uri.TryCreate(URL, UriKind.Absolute, out uriResult))
            {
                errors += "\nInvalid URL";
                check = false;
            }
            else
            {
                Uri.TryCreate(URL, UriKind.Absolute, out uriResult);
            }

            if (!decimal.TryParse(Price, out price))
            {
                errors += "\nInvalid Price: must be a decimal";
                check = false;
            }
            else
            {
                decimal.TryParse(Price, out price);

                if (price < 0)
                {
                    errors += "\nInvalid Price: must be a number greater than or equal to 0";
                    check = false;
                }
            }

            if (check)
            {
                Games game = new Games()
                {
                    GameName = Title,
                    GameDescription = Description,
                    GamePrice = price,
                    GameRating = review,
                    ImageURL = URL
                };

                db.Games.Add(game);
                MessageBox.Show("Added " + game.ToString() + " to the database");

                db.SaveChanges();
                MessageBox.Show("Saved changes");

                var query = from games in db.Games
                            select games;

                games = query.ToList();
                games.Sort();

                lbxGameList.ItemsSource = games;
            }
            else
            {
                MessageBox.Show(errors, "ERRORS", MessageBoxButton.OK, MessageBoxImage.Error);
                tbxEnterTitle.Text = "Enter Title Name";
                tbxEnterDescription.Text = "Enter Description";
                tbxEnterPrice.Text = "Price";
                tbxEnterReview.Text = "0";
                tbxEnterURL.Text = "Enter URL";
            }
                
        }

        private void tbxSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxSearch.Text = "";
        }

        private void tbxEnterDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxEnterDescription.Text = "";
        }

        private void tbxEnterReview_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxEnterReview.Text = "";
        }

        private void tbxEnterPrice_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxEnterPrice.Text = "";
        }

        private void tbxEnterURL_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxEnterURL.Text = "";
        }

        private void tbxEnterTitle_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxEnterTitle.Text = "";
        }

        private void lbxLibraryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Games selectedGame = lbxLibraryList.SelectedItem as Games;

            if (selectedGame != null)
            {
                tblkDescription.Text = selectedGame.GameDescription;

                tblkPrice.Text = string.Format($"{selectedGame.GamePrice:c}");

                tblkRating.Text = string.Format($"{selectedGame.GameRating}/10");

                imgGameImage.Source = new BitmapImage(new Uri(selectedGame.ImageURL));
            }
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Games> searchedGame = new List<Games>();
            
            var query = (from games in library
                        where games.GameName.Contains(tbxSearch.Text)
                        select games).Distinct();
            
            searchedGame = query.ToList();

            searchedGame.Sort();

            lbxLibraryList.ItemsSource = searchedGame;
        }
    }
}
