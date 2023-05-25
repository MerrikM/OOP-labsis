using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP8
{
    public abstract class ModelFactory //Абстрактная фабрика
    {
        public virtual Model CreateObject(string code) { return null; }
    }
}
