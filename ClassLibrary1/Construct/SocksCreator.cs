﻿using System;
using System.Collections.Generic;
using System.Linq;
using lab3.Clothes;
using lab3.Core;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Construct
{
    class SocksCreator : Creator
    {
        public override lab3.Clothes.Wear Create(String name)
        {
            return new ClassLibrary.Socks(name);
        }
    }
}
