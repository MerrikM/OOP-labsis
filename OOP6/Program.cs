using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP6
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form());
        }
    }
    public class Model
    {
        public Color object_color;
        public PictureBox picturbx; // Для передачи размера формы для рисования
        protected int RADIX;
        protected Point location;
        protected bool selection;



        public Model(Point location, Color color, PictureBox picturbx) // Предок, от которого наследуются свойства
        {
            RADIX = 40;
            this.picturbx = picturbx;
            this.location = location;
            selection = true;
            object_color = color;
        }


        public void changeselection_toFalse() { selection = false; } 


        public bool getselection() { return selection; } // Проверка, выбран ли объект


        public virtual void OnPaint(PaintEventArgs e) { }


        public virtual bool isPicked(MouseEventArgs e, bool controlUp) { return false; } 


        public virtual void increase_Size() // Увеличение
        {
            if (check_Location(location.X, location.Y, RADIX))
                RADIX += 3;
        }


        public virtual void decrease_Size() // Уменьшение
        {
            if (RADIX > 3)
                RADIX -= 3;
        }


        public virtual void move_Object(int _X, int _Y) // Движение объектов
        {
            if (check_Location(location.X + _X, location.Y + _Y, RADIX))
            {
                location.X += _X;
                location.Y += _Y;
            }
        }


        public bool check_Location(int point_X, int point_Y, int RADIX) //проверка на выход за границу
        {
            if ((point_X - RADIX >= 0) & (point_X + RADIX <= picturbx.Width) &
                (point_Y - RADIX >= 0) & (point_Y + RADIX <= picturbx.Height))
                return true;
            return false;
        }
    }


    public class CCircle : Model
    {

        public CCircle(Point location, Color color, PictureBox picturbx) : base(location, color, picturbx) { } // Базовый конструктор


        public override void OnPaint(PaintEventArgs e) // Перересовка всего экрана
        {
            Pen pen = new Pen(object_color, RADIX * 2);
            if (selection)
                pen.Color = Color.Red;
            else
                pen.Color = Color.Black;

            e.Graphics.DrawEllipse(pen, location.X - RADIX, location.Y - RADIX, RADIX * 2, RADIX * 2);
        }


        public override bool isPicked(MouseEventArgs e, bool controlUp) // Проверка, выбран(выделен) ли объект
        {
            if (((location.X - e.X)*(location.X - e.X) + (location.Y - e.Y)* (location.Y - e.Y) <= RADIX*RADIX) && controlUp)
            {
                selection = !selection;
                return true;
            }
            return false;
        }
    }


    public class CSquare : Model
    {
        public CSquare(Point location, Color color, PictureBox picturbx) : base(location, color, picturbx) { }


        public override void OnPaint(PaintEventArgs e) // Отрисовка Квадрата
        {
            Pen pen = new Pen(object_color, RADIX * 2);
            if (selection)
                pen.Color = Color.Red;
            else
                pen.Color = Color.Black;

            e.Graphics.DrawRectangle(pen, location.X - RADIX, location.Y - RADIX, RADIX * 2, RADIX * 2);
        }


        public override bool isPicked(MouseEventArgs e, bool controlUp) // Проверка, выбран(выделен) ли объект
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
    }


    public class Triangle : Model
    {
        private Point A;
        private Point B;
        private Point C;


        public Triangle(Point location, Color color, PictureBox picturbx) : base(location, color, picturbx)
        {
            //RADIX *= 2;
            refresh_attributes();
        }

        public void refresh_attributes() // В точках хранятся не координаты вершин, а переменные, к которым нужно прибавить координату, чтобы попасть в точку вершины
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

        public override void OnPaint(PaintEventArgs e) // Отрисовка треугольника
        {
            refresh_attributes();
            Pen pen = new Pen(object_color);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            // e.Graphics.FillRectangle(br, new Rectangle(pt, new Size(this.ClientRectangle.Width / 2, this.ClientRectangle.Height / 2)));
            if (selection)
            {
                pen.Color = Color.Red;
                pen.Width = 7;
            }
            else
            {
                pen.Color = Color.Black;
                pen.Width = 4;
            }
            e.Graphics.DrawLine(pen, location.X+A.X, location.Y+A.Y, location.X+B.X, location.Y+B.Y); // 3 Линии треугольника
            e.Graphics.DrawLine(pen, location.X+A.X, location.Y+A.Y, location.X+C.X, location.Y+C.Y);
            e.Graphics.DrawLine(pen, location.X+B.X, location.Y+B.Y, location.X+C.X, location.Y+C.Y);

        }


        public override bool isPicked(MouseEventArgs e, bool controlUp) // Проверка, выбран(выделен) ли объект
        {
            refresh_attributes();
            if ((Triangle_Square(location.X+A.X, location.Y+A.Y, location.X+B.X, location.Y+B.Y, e.X, e.Y) + Triangle_Square(location.X+A.X, location.Y+A.Y, e.X, e.Y, location.X+C.X, location.Y+C.Y) +         //Смертельный метод сравнения площадей
                Triangle_Square(e.X, e.Y, location.X+B.X, location.Y+B.Y, location.X+C.X, location.Y+C.Y) - Triangle_Square(location.X+A.X, location.Y+A.Y, location.X+B.X, location.Y+B.Y, location.X+C.X, location.Y+C.Y) <= 0.01) &
                controlUp)
            {
                selection = !selection; 
                return true;
            }
            return false;
        }

    }


    public class Storage
    {
        private List<Model> objects;
        public Storage()
        {
            objects = new List<Model>();
        }


        public int getSize() { return objects.Count(); }

        public Model getObject(int index) { return objects[index]; }


        public void AddObject(Model temp_object, MouseEventArgs e, bool controlUp)
        {
            for (int i = 0; i < objects.Count(); i++)
                if (objects[i].isPicked(e, controlUp)) 
                {
                    return;
                }
            objects.Add(temp_object);
            if (!controlUp) for (int i = 0; i < objects.Count() - 1; i++)
                objects[i].changeselection_toFalse();
        }


        public void delete_selectedObjects()
        {
            for (int i = objects.Count() - 1; i >= 0; i--)
                if (objects[i].getselection() == true)
                    objects.RemoveAt(i);
        }
        public void delete_all()
        {
            for (int i = objects.Count() - 1; i >= 0; i--)
                    objects.RemoveAt(i);
        }
    }
}