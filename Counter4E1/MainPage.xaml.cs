using Microsoft.Maui.Graphics.Text;

namespace Counter4E1
{
    public partial class MainPage : ContentPage
    {
        int wartoscLicznika = 0;

        string plik = Path.Combine(
            FileSystem.AppDataDirectory,
            "liczniki.txt");

        public MainPage()
        {
            InitializeComponent();
            Wczytaj();
        }

     
        private void dodajDoLicznika(object sender, EventArgs e)
        {
            wartoscLicznika++;
            licznikLabel.Text = wartoscLicznika.ToString();
            Zapisz();
        }

        private void odejmijOdLicznika(object sender, EventArgs e)
        {
            wartoscLicznika--;
            licznikLabel.Text = wartoscLicznika.ToString();
            Zapisz();
        }

 
        private  void DodajNowyLicznik(object sender, EventArgs e)
        {
            string nazwa = nazwaNowegoLicznika.Text?.Trim() ?? "";
            if (!int.TryParse(wartoscNowegoLicznika.Text, out int wartosc))
            {
                DisplayAlert("Błąd", "Podaj liczbę!", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(nazwa))
            {
                DisplayAlert("Błąd", "Podaj nazwę!", "OK");
                return;
            }

            DodajLicznik(nazwa, wartosc);

            nazwaNowegoLicznika.Text = "";
            wartoscNowegoLicznika.Text = "";
            Zapisz();
        }

        private void DodajLicznik(string nazwa, int wartosc)
        {
            Label nazwaLabel = new Label
            {
                Text = nazwa,
                FontSize = 24,
                HorizontalOptions = LayoutOptions.Center
            };

            Label wynik = new Label
            {
                Text = wartosc.ToString(),
                FontSize = 38,
                HorizontalOptions = LayoutOptions.Center
            };

            Button plus = new Button
            {
                Text = "+",
                WidthRequest = 70,
                Margin = 20,
                FontSize = 30
            };

            Button minus = new Button
            {
                Text = "-",
                WidthRequest = 70,
                Margin = 20,
                FontSize = 30
            };

            plus.Clicked += (s, e) =>
            {
                wartosc++;
                wynik.Text = wartosc.ToString();
                Zapisz();
            };

            minus.Clicked += (s, e) =>
            {
                wartosc--;
                wynik.Text = wartosc.ToString();
                Zapisz();
            };

            HorizontalStackLayout przyciski = new HorizontalStackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 20
            };

            przyciski.Children.Add(plus);
            przyciski.Children.Add(minus);

            VerticalStackLayout licznik = new VerticalStackLayout
            {
                Spacing = 10
            };

            licznik.Children.Add(nazwaLabel);
            licznik.Children.Add(wynik);
            licznik.Children.Add(przyciski);

            noweLiczniki.Children.Add(licznik);
        }


        private void Zapisz()
        {
            string tekst = "Początkowy licznik:" + wartoscLicznika;

            foreach (var element in noweLiczniki.Children)
            {
                if (element is VerticalStackLayout licznik)
                {
                    string nazwa = ((Label)licznik.Children[0]).Text;
                    string wartosc = ((Label)licznik.Children[1]).Text;

                    tekst += "\n" + nazwa + ":" + wartosc;
                }
            }

            File.WriteAllText(plik, tekst);
        }

 
        private void Wczytaj()
        {
            if (!File.Exists(plik)) return;

            string[] liczniki = File.ReadAllLines(plik);

            foreach (string licznik in liczniki)
            {
                string[] dane = licznik.Split(':');

                if (dane.Length != 2)
                    continue;

                if (!int.TryParse(dane[1], out int wartosc))
                    continue;

                if (dane[0] == "Początkowy licznik")
                {
                    wartoscLicznika = wartosc;
                    licznikLabel.Text = wartosc.ToString();
                }
                else
                {
                    DodajLicznik(dane[0], wartosc);
                }
            }
        }
    }
       
}
