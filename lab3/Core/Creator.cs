using lab3.Clothes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Core
{
    public abstract class Creator
    {
        abstract public Wear Create(String name);
    }
}
