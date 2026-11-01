namespace Counter4E1.Models
{
    public class Counter
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public Counter(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}
