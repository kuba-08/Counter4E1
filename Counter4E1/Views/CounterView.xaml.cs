using Counter4E1.Models;
using Counter4E1.Views;

namespace Counter4E1.Views;

public partial class CounterView : ContentView
{
	public CounterView()
	{
		InitializeComponent();
	}

    private void SubstractValue(object sender, EventArgs e)
    {
        if (BindingContext is Counter counter)
        {
            counter.Value--;
        }
    }

    private void AddValue(object sender, EventArgs e)
    {
        if (BindingContext is Counter counter)
        {
            counter.Value++;
        }
    }
}