using System;
using lab3.Clothes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [Serializable]
    public class Sneakers : Shoes
    {
        public string collection { get; set; }

        public Sneakers(String name) : base(name) { }

        public Sneakers() { }

        public override string ToString()
        {
            return "Sneakers";
        }
    }
}
