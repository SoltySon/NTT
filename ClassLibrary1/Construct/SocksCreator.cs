using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Construct
{
    class SocksCreator : Creation
    {
        public override lab3.Clothes.Wear Create(String name)
        {
            return new ClassLibrary.Socks(name);
        }
    }
}
