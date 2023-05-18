using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP7
{
    public class CCircle : Model
    {
        public CCircle() { }
        public CCircle(Point location, Color color, PictureBox picturbx) : base(location, color, picturbx) { }


        public override void OnPaint(PaintEventArgs e)
        {
            Pen pen = new Pen(object_color);
            if (selection)
                pen.Width = 7;
            else
                pen.Width = 4;

            e.Graphics.DrawEllipse(pen, location.X - RADIX, location.Y - RADIX, RADIX * 2, RADIX * 2);
        }


        public override bool isPicked(MouseEventArgs e, bool controlUp)
        {
            if (((location.X - e.X) * (location.X - e.X) + (location.Y - e.Y) * (location.Y - e.Y) <= RADIX * RADIX) && controlUp)
            {
                selection = !selection;
                return true;
            }
            return false;
        }
        public override void SaveObject(StreamWriter save)
        {
            save.WriteLine("Circle");
            base.SaveObject(save);
        }
    }


    public class CSquare : Model
    {
        public CSquare() { }
        public CSquare(Point location, Color color, PictureBox picturbx) : base(location, color, picturbx) { }


        public override void OnPaint(PaintEventArgs e)//Отрисовка Квадрата
        {
            Pen pen = new Pen(object_color);
            if (selection)
                pen.Width = 7;
            else
                pen.Width = 4;

            e.Graphics.DrawRectangle(pen, location.X - RADIX, location.Y - RADIX, RADIX * 2, RADIX * 2);
        }


        public override bool isPicked(MouseEventArgs e, bool controlUp)//попали ли мы в объект
        {
            if ((e.X <= location.X + RADIX) & (e.X >= location.X - RADIX) &
                (e.Y <= location.Y + RADIX) & (e.Y >= location.Y - RADIX) &
                controlUp)
            {
                selection = !selection;
                return true;
            }
            return false;
        }
        public override void SaveObject(StreamWriter save)
        {
            save.WriteLine("Square");
            base.SaveObject(save);
        }
    }


    public class Triangle : Model
    {
        private Point A;
        private Point B;
        private Point C;

        public Triangle() { }
        public Triangle(Point location, Color color, PictureBox picturbx) : base(location, color, picturbx)
        {
            //RADIX *= 2;
            refresh_attributes();
        }

        public void refresh_attributes()
        {
            A.X = -RADIX;
            A.Y = RADIX;

            B.X = RADIX;
            B.Y = RADIX;

            C.X = 0;
            C.Y = -RADIX;
        }

        private double Triangle_Square(double aX, double aY, double bX, double bY, double cX, double cY)
        {
            return Math.Abs(bX * cY - cX * bY - aX * cY + cX * aY + aX * bY - bX * aY); // Мудрёная формула площади по точкам
        }

        public override void OnPaint(PaintEventArgs e)
        {
            refresh_attributes();
            Pen pen = new Pen(object_color);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;

            if (selection)
                pen.Width = 7;
            else
                pen.Width = 4;

            e.Graphics.DrawLine(pen, location.X + A.X, location.Y + A.Y, location.X + B.X, location.Y + B.Y);
            e.Graphics.DrawLine(pen, location.X + A.X, location.Y + A.Y, location.X + C.X, location.Y + C.Y);
            e.Graphics.DrawLine(pen, location.X + B.X, location.Y + B.Y, location.X + C.X, location.Y + C.Y);

        }


        public override bool isPicked(MouseEventArgs e, bool controlUp)
        {
            refresh_attributes();
            if ((Triangle_Square(location.X + A.X, location.Y + A.Y, location.X + B.X, location.Y + B.Y, e.X, e.Y) + Triangle_Square(location.X + A.X, location.Y + A.Y, e.X, e.Y, location.X + C.X, location.Y + C.Y) +         //Смертельный метод сравнения площадей
                Triangle_Square(e.X, e.Y, location.X + B.X, location.Y + B.Y, location.X + C.X, location.Y + C.Y) - Triangle_Square(location.X + A.X, location.Y + A.Y, location.X + B.X, location.Y + B.Y, location.X + C.X, location.Y + C.Y) <= 0.01) &
                controlUp)
            {
                selection = !selection;
                return true;
            }
            return false;
        }
        public override void SaveObject(StreamWriter save)
        {
            save.WriteLine("Triangle");
            base.SaveObject(save);
        }
    }

}
