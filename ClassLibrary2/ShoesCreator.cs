using System;
using lab3.Clothes;
using lab3.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Construct
{
    class ShoesCreator : Creator
    {
        public override Wear Create(String name)
        {
            return new ClassLibrary.Shoes(name);
        }
    }
}