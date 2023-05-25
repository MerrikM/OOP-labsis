using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP8
{
    public class MyObjectsFactory : ModelFactory
    {
        public override Model CreateObject(string code)
        {
            Model temp = null;
            switch (code)
            {
                case "Circle":
                    temp = new CCircle();
                    break;
                case "Square":
                    temp = new CSquare();
                    break;
                case "Triangle":
                    temp = new Triangle();
                    break;
                case "Group":
                    temp = new Group();
                    break;
            }
            return temp;
        }
    }
}
