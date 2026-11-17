using System.Collections.ObjectModel;
using System.ComponentModel;
using Counter4E1.Models;

namespace Counter4E1.Views;

    public partial class MainPage : ContentPage
    {
        private int counterValue = 0;

        string filePath = Path.Combine(
            FileSystem.AppDataDirectory,
            "liczniki.txt");

    public ObservableCollection<Counter> Counters { get; } = new ObservableCollection<Counter>();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
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
        counter.PropertyChanged += Counter_PropertyChanged;
        Counters.Add(counter);
        }
    private void Counter_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Counter.Value))
        {
            Save();
        }
    }

        private void Save()
        {
            List<string> lines = new List<string>();
        lines.Add("Początkowy Licznik:" + counterValue);

        foreach (Counter counter in Counters)
        {
            lines.Add(counter.Name + ":" + counter.Value);
        }
        File.WriteAllLines(filePath, lines);
    }

        private void Load()
        {
            if (!File.Exists(filePath))
                return;

            string[] countersLines = File.ReadAllLines(filePath);

            foreach (string counterLine in countersLines)
            {
                string[] data = counterLine.Split(':', 2);

                if (data.Length != 2)
                    continue;

                if (!int.TryParse(data[1], out int value))
                    continue;

                if (data[0].Equals("Początkowy licznik", StringComparison.OrdinalIgnoreCase))
                {
                    counterValue = value;
                    counterLabel.Text = value.ToString();
                continue;
                }
                else
                {
                    Counter counter = new Counter(data[0], value);
                    AddCounter(counter);
                }
            }
        }
    }
