using System;
using lab3.Clothes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [Serializable]
    public class Socks : Wear
    {
        public string type { get; set; }

        public Socks(String name) : base(name) { }

        public Socks() { }

        public override string ToString()
        {
            return "Socks";
        }
    }
}
