<<<<<<< HEAD
﻿
using System.ComponentModel;

namespace Counter4E1.Models;

public class Counter : INotifyPropertyChanged
=======
namespace Counter4E1.Models
>>>>>>> 91290574b2c642f26a0efc56782ee6b3f600efc9
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
