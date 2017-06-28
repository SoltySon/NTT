using System;
using System.Collections.Generic;
using lab3.Clothes;
using lab3.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Construct
{
    class SneakersCreator : Creator
    {
        public override lab3.Clothes.Wear Create(String name)
        {
            return new ClassLibrary.Sneakers(name);
        }
    }
}
