using Counter4E1.Models;

namespace Counter4E1.Views
{
    public partial class MainPage : ContentPage
    {
        int counterValue = 0;

        string filePath = Path.Combine(
            FileSystem.AppDataDirectory,
            "liczniki.txt");

        public MainPage()
        {
            InitializeComponent();
            Load();
        }

        private void AddToCounter(object sender, EventArgs e)
        {
            counterValue++;
            counterLabel.Text = counterValue.ToString();
            Save();
        }

        private void SubtractFromCounter(object sender, EventArgs e)
        {
            counterValue--;
            counterLabel.Text = counterValue.ToString();
            Save();
        }

        private void AddNewCounter(object sender, EventArgs e)
        {
            string name = newCounterName.Text?.Trim() ?? "";

            if (!int.TryParse(newCounterValue.Text, out int value))
            {
                DisplayAlert("Błąd", "Podaj liczbę!", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                DisplayAlert("Błąd", "Podaj nazwę!", "OK");
                return;
            }

            Counter counter = new Counter(name, value);

            AddCounter(counter);

            newCounterName.Text = "";
            newCounterValue.Text = "";

            Save();
        }

        private void AddCounter(Counter counter)
        {
            Label nameLabel = new Label
            {
                Text = counter.Name,
                FontSize = 24,
                HorizontalOptions = LayoutOptions.Center
            };

            Label valueLabel = new Label
            {
                Text = counter.Value.ToString(),
                FontSize = 38,
                HorizontalOptions = LayoutOptions.Center
            };

            Button plusButton = new Button
            {
                Text = "+",
                WidthRequest = 70,
                Margin = 20,
                FontSize = 30
            };

            Button minusButton = new Button
            {
                Text = "-",
                WidthRequest = 70,
                Margin = 20,
                FontSize = 30
            };

            plusButton.Clicked += (s, e) =>
            {
                counter.Value++;
                valueLabel.Text = counter.Value.ToString();
                Save();
            };

            minusButton.Clicked += (s, e) =>
            {
                counter.Value--;
                valueLabel.Text = counter.Value.ToString();
                Save();
            };

            HorizontalStackLayout buttons = new HorizontalStackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 20
            };

            buttons.Children.Add(minusButton);
            buttons.Children.Add(plusButton);

            VerticalStackLayout counterLayout = new VerticalStackLayout
            {
                Spacing = 10
            };

            counterLayout.Children.Add(nameLabel);
            counterLayout.Children.Add(valueLabel);
            counterLayout.Children.Add(buttons);

            newCounters.Children.Add(counterLayout);
        }

        private void Save()
        {
            string text = "Początkowy licznik:" + counterValue;

            foreach (var element in newCounters.Children)
            {
                if (element is VerticalStackLayout counterLayout)
                {
                    string name = ((Label)counterLayout.Children[0]).Text;
                    string value = ((Label)counterLayout.Children[1]).Text;

                    text += "\n" + name + ":" + value;
                }
            }

            File.WriteAllText(filePath, text);
        }

        private void Load()
        {
            if (!File.Exists(filePath))
                return;

            string[] counters = File.ReadAllLines(filePath);

            foreach (string counterLine in counters)
            {
                string[] data = counterLine.Split(':');

                if (data.Length != 2)
                    continue;

                if (!int.TryParse(data[1], out int value))
                    continue;

                if (data[0] == "Początkowy licznik")
                {
                    counterValue = value;
                    counterLabel.Text = value.ToString();
                }
                else
                {
                    Counter counter = new Counter(data[0], value);
                    AddCounter(counter);
                }
            }
        }
    }
}