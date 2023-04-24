using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//OOP_Lab_4_1


/*
 События KeyDown и KeyUp отправляются сфокусированному контролу. Если на форме есть другие контролы, и они сфокусированы, то они будут заглатывать эти события, и форме ничего не останется.

Если вы хотите, чтобы форме всегда приходили эти события, то установите для формы KeyPreview=true.
 
 */
namespace OOP_Lab_4_1
{

    public partial class Form1 : Form
    {
        private int radius = 0;
        private List<CCircle> Circles = new List<CCircle>();
        private int ctrl = 0;
        public Form1()
        {
            InitializeComponent();
            radius = 70;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (CCircle Circle in Circles)
            {
                Circle.drawCircle(e.Graphics); 
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (ctrl == 0) 
            {
                foreach (CCircle Circle1 in Circles)
                {
                    Circle1.setColor("Black"); // Снимаем выделение со всех объектов 
                }
                CCircle Circle = new CCircle(e.X, e.Y, radius);
                Circles.Add(Circle);
            }
            if (ctrl == 1) 
            {
                foreach (CCircle Circle1 in Circles)
                {
                    if (Circle1.mouseinCircle(e) == true) 
                    {
                        checkBox2.Checked = true;
                        break;
                    }
                }
                Refresh();
            }
            Refresh();
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys == Keys.Control) 
            {
                checkBox1.Checked = !checkBox1.Checked; 
            }
            switch (ctrl) 
            {
                case 0:
                    ctrl++;
                    foreach (CCircle Circle1 in Circles)
                    {
                        Circle1.setCtrl(true);
                    }
                    break;
                case 1:
                    ctrl = 0;
                    foreach (CCircle Circle1 in Circles)
                    {
                        Circle1.setCtrl(false);
                    }
                    break;
            }

        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Circles.Count(); i++)
            {
                if (Circles[i].getColor() == "Red")
                {
                    Circles.RemoveAt(i);
                    i--;
                    checkBox2.Checked = false;
                }
            }
            Refresh();
        }
        public class CCircle
        {
            private int coordX, coordY, radius;
            private string color = "Red";
            private bool ctrl = false;
            public CCircle(int coordX, int coordY, int radius)
            {
                this.coordX = coordX;
                this.coordY = coordY;
                this.radius = radius;

            }
            public void drawCircle(Graphics Canvas) 
            {
                if (color == "Red")
                {
                    Canvas.DrawEllipse(new Pen(Color.Red, radius * 2),      // Структура Pen, определяющая цвет, ширину и стиль эллипса.
                                                       coordX - radius,     // Координата X верхнего левого угла ограничивающего прямоугольника, который определяет эллипс.
                                                       coordY - radius,     // Координата Y верхнего левого угла ограничивающего прямоугольника, который определяет эллипс.
                                                       radius * 2,          // Ширина ограничивающего прямоугольника, который определяет эллипс.
                                                       radius * 2);         // Высота ограничивающего прямоугольника, который определяет эллипс.
                }
                else
                {
                    Canvas.DrawEllipse(new Pen(Color.Black, radius * 2),
                                                       coordX - radius,
                                                       coordY - radius,
                                                       radius * 2,
                                                       radius * 2);
                }
            }
            public void setColor(string Color) 
            {
                color = Color;
            }
            public string getColor() 
            {
                return color;
            }
            public bool mouseinCircle(MouseEventArgs e) 
            {
                if (ctrl)
                {
                    if (Math.Pow(e.X - coordX, 2) + Math.Pow(e.Y - coordY, 2) <= Math.Pow(radius, 2) && color != "Red")
                    {
                        color = "Red";
                        return true;
                    }
                }
                return false;
            }
            public void setCtrl(bool a)
            {
                ctrl = a;
            }

        }
    }
}