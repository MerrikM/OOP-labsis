using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace OOP7
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
        protected int RADIX;
        protected Point location;
        protected bool selection;
        protected Point picturbxparam;


        public Model() { }
        public Model(Point location, Color color, PictureBox picturbx)
        {
            RADIX = 40;
            this.picturbxparam.X = picturbx.Width;
            this.picturbxparam.Y = picturbx.Height;
            this.location = location;
            selection = true;
            object_color = color;
        }


        public void changeselection_toFalse() { selection = false; } 


        public virtual bool getselection() { return selection; } 


        public virtual void OnPaint(PaintEventArgs e) { }

        public void SetBord(int _x, int _y)
        {
            this.picturbxparam = new Point(_x, _y);
        }
        public virtual bool isPicked(MouseEventArgs e, bool controlUp) { return false; }

        public virtual void setColor(Color color) //Установить цвет объекта
        {
            this.object_color = color;
        }
        public virtual void increase_Size() 
        {
            if (check_Location(6, 6))
                RADIX += 6;
        }
        public virtual bool isIncreasable(int _radix)
        {
            if (((location.X - _radix - RADIX >= 0) & (location.X + _radix + RADIX <= picturbxparam.X) &
                (location.Y - _radix - RADIX >= 0) & (location.Y + _radix + RADIX <= picturbxparam.Y)))
                return true;
            return false;
        }

        public virtual void decrease_Size()
        {
            if (RADIX > 6)
                RADIX -= 6;
        }


        public virtual void move_Object(int _X, int _Y) 
        {
            if (check_Location(_X,_Y))
            {
                location.X += _X;
                location.Y += _Y;
            }
        }
        public virtual (int, int, int, int) getGroupBoards()
        {
            var tuple = (location.X - RADIX, location.X + RADIX, location.Y - RADIX, location.Y + RADIX);
            return tuple;
        }

        public virtual bool check_Location(int point_X, int point_Y) //проверка на выход за границу
        {
            if ((this.location.X - this.RADIX + point_X >= 0) & (point_X + this.RADIX + this.location.X <= picturbxparam.X) &
                (this.location.Y - this.RADIX + point_Y >= 0) & (point_Y + this.RADIX + this.location.Y <= picturbxparam.Y))
                return true;
            return false;
        }
        public virtual void SaveObject(StreamWriter save)
        {
            save.Write(location.X.ToString() + " ");
            save.Write(location.Y.ToString() + " ");
            save.Write(RADIX.ToString() + " ");
            save.Write(object_color.ToArgb() + " ");
            save.Write(selection.ToString() + " ");
            save.Write(picturbxparam.X.ToString() + " ");
            save.WriteLine(picturbxparam.Y.ToString() + " ");
        }


        public virtual void LoadObject(StreamReader load, ModelFactory factory)
        {
            string[] s = load.ReadLine().Split(' ');
            location.X = int.Parse(s[0]);
            location.Y = int.Parse(s[1]);
            RADIX = int.Parse(s[2]);
            object_color = Color.FromArgb(int.Parse(s[3]));
            selection = bool.Parse(s[4]);
            picturbxparam.X = int.Parse(s[5]);
            picturbxparam.Y = int.Parse(s[6]);
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
        public void GroupObjects() // Группировка объектов
        {
            Group g = new Group();
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                if (objects[i].getselection())
                {
                    g.add_in_Group(objects[i]); // Добавляем в объект группа (новый список)
                    objects.RemoveAt(i); // Убираем добавленный элемент из старого списка объектов
                }
            
            }
            if (g.getGroup().Count < 2)
                objects.AddRange(g.getGroup()); // Если кол-во = 1, работаем, как с объектом, а не группой
            else
                objects.Add(g);
        }


        //Разгруппировывает объекты
        public void UngroupObjects()
        {
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                if (objects[i] is Group & objects[i].getselection())
                {
                    objects.AddRange(((Group)objects[i]).getGroup());
                    objects.RemoveAt(i);
                }
            }
        }
        public void SaveStorage(string _name)
        {
            StreamWriter save = new StreamWriter(_name, false);
            save.WriteLine(objects.Count.ToString()); // Записываем кол-во объектов
            foreach (var obj in objects)
            {
                obj.SaveObject(save);
                save.Flush();
            }
            save.Close();
        }


        public void LoadStorage(ModelFactory factory, string _name)
        {
            Model tempModel;
            StreamReader load = new StreamReader(_name);
            int count;

            try
            {
                count = int.Parse(load.ReadLine()); // Берем 1 строку и преобразовываем ее в int, для вычисления кол-ва элементов
            }
            catch
            {
                load.Close();
                return;
            }

            for (int i = 0; i < count; i++)
            {
                /*
                 "Выгружаем" информацию из файла и создаем объект
                */
                tempModel = factory.CreateObject(load.ReadLine()); 
                if (tempModel != null)
                {
                    tempModel.LoadObject(load, factory);
                    objects.Add(tempModel);
                }
            }
            load.Close();
        }
    }
}