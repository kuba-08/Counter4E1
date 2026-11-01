using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
