﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab3.Clothes;

namespace lab3.Core
{
    class TshirtCreator : Creator
    {
        public override Wear Create(String name)
        {
            return new Tshirt(name);
        }
    }
}
