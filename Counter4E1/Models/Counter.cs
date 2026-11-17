
using System.ComponentModel;

namespace Counter4E1.Models;
public class Counter : INotifyPropertyChanged
{
    private int value;

    public string Name { get; set; }

    public int Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs("Value"));
        }
    }

    public Counter(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
