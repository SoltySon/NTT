using System;
using lab3.Clothes;
using lab3.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [Serializable]
    public class Shoes : Wear
    {
        public string size { get; set; }

        public string model { get; set; }

        public Shoes(String name) : base(name) { }

        public Shoes() { }

        public override string ToString()
        {
            return "Shoes";
        }
    }
}
