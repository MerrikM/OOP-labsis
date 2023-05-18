using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP7;

namespace OOP7
{
    public abstract class ModelFactory //Абстрактная фабрика
    {
        public virtual Model CreateObject(string code) { return null; }
    }
}
