using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace OOP8
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
    public abstract class Model : Observerable
    {
        public Color object_color;
        protected int RADIX;
        protected Point location;
        protected bool selection;
        protected Point picturbxparam;
        protected bool mosh;
        protected bool slave;
        protected bool slavesel;
        protected List<Model> Colleagues;


        public Model()
        {
            List<Model> Colleagues = new List<Model>();
        }
        public Model(Point location, Color color, PictureBox picturbx)
        {
            RADIX = 40;
            this.picturbxparam.X = picturbx.Width;
            this.picturbxparam.Y = picturbx.Height;
            this.location = location;
            selection = true;
            slave = false;
            slavesel = false;
            object_color = color;
            Colleagues = new List<Model>();
        }

        public Point getloc()
        {
            Point op = new Point(location.X, location.Y);
            return op;
        }
        public bool getslavesel() { return slavesel; }
        public void changeselection_to(bool _p) { selection = _p; } 
        public void changeslavesel_to(bool _p) { slavesel = _p; }
        public void Addcolleagues(Model _m)
        {
                this.Colleagues.Add(_m);
                this.Colleagues[Colleagues.Count - 1].slave = true;
        }
        public void RemoveColleagues()
        {
            if(Colleagues!=null)for (int i = 0; i < Colleagues.Count; i++)
                if (Colleagues[i].getselection()) {
                    Colleagues.RemoveAt(i);
                this.slave = false;
        }
        }
        public void freecolleagues()
        { if (Colleagues!=null) for (int i = 0; i < Colleagues.Count; i++)
                    Colleagues[i].slave = false;
        }
        public void changemosh_to(bool _p) { mosh = _p; }

        public virtual bool getselection() { return selection; } 

        public virtual void OnPaint(PaintEventArgs e) { }

        public void SetBord(int _x, int _y)
        {
            this.picturbxparam = new Point(_x, _y);
        }
        public virtual bool isPicked(MouseEventArgs e, bool controlUp, bool Tup) { return false; }

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
            if (mosh)
                notifyObjects(_X, _Y);

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
        public virtual void Draw_Lines(PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black);
            pen.Width = 4;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            pen.CustomEndCap = new AdjustableArrowCap(5, 5);

            if(Colleagues!=null)
            for (int i = 0; i < this.Colleagues.Count(); i++)
            {
                e.Graphics.DrawLine(pen, this.getloc(), this.Colleagues[i].getloc());
            }
        }
        public virtual void SaveObject(StreamWriter save)
        {
            save.Write(location.X.ToString() + " ");
            save.Write(location.Y.ToString() + " ");
            save.Write(RADIX.ToString() + " ");
            save.Write(object_color.ToArgb() + " ");
            save.Write(selection.ToString() + " ");
            save.Write(Colleagues.Count().ToString() + " ");
            save.Write(picturbxparam.X.ToString() + " ");                      
            save.WriteLine(picturbxparam.Y.ToString() + " ");
            for(int i=0; i < Colleagues.Count(); i++)
            {
                Colleagues[i].SaveObject(save);
            }
        }


        public virtual void LoadObject(StreamReader load, ModelFactory factory)
        {
            string[] s = load.ReadLine().Split(' ');
            Colleagues = new List<Model>();
            location.X = int.Parse(s[0]);
            location.Y = int.Parse(s[1]);
            RADIX = int.Parse(s[2]);
            object_color = Color.FromArgb(int.Parse(s[3]));
            selection = bool.Parse(s[4]);
            int counter = int.Parse(s[5]);
            picturbxparam.X = int.Parse(s[6]);
            picturbxparam.Y = int.Parse(s[7]);
            for (int i = 0; i < counter; i++)
            {
                Model tempMode2;
                tempMode2 = factory.CreateObject(load.ReadLine());
                tempMode2.LoadObject(load, factory);
                Colleagues.Add(tempMode2);
            }
        }
        public virtual bool isMoshniy(Model o)
        {
            if (Math.Sqrt(Math.Pow(o.location.X - location.X, 2) +
                Math.Pow(o.location.Y - location.Y, 2)) <= o.RADIX + RADIX)
                return true;
            return false;
        }
    }




    public class Storage : Observerable
    {
        private List<Model> objects;
        public Storage()
        {
            objects = new List<Model>();
        }


        public int getSize() { return objects.Count(); }

        public Model getObject(int index) { return objects[index]; }


        public void AddObject(Model temp_object, MouseEventArgs e, bool controlUp, bool Tup)
        {
            for (int i = 0; i < objects.Count(); i++)
                if (objects[i].isPicked(e, controlUp, Tup))
                {
                    if (Tup)
                    {
                        MoshniyObserver MO = new MoshniyObserver();
                        MO.AddMoshniyObserver(objects[i]);
                        objects[GetFirstSelected()].Addcolleagues(objects[i]);
                        objects[GetFirstSelected()].changemosh_to(true);
                        objects[GetFirstSelected()].addObserver(MO);
                    }
                    notifyTree();
                    return;
                }
            objects.Add(temp_object);
            if (!controlUp) for (int a = 0; a < objects.Count() - 1; a++)
                { objects[a].changeselection_to(false); }
            notifyTree();
            return;
        }
        public void AddObject(Model temp_object)
        {
            objects.Add(temp_object);
        }
        public int GetFirstSelected()
        {
            for (int i = 0; i < getSize(); i++) if (objects[i].getselection()) return i;
            return -1;
        }
        public void delete_selectedObjects()
        {

            for (int i = objects.Count() - 1; i >= 0; i--)
                if (objects[i].getselection() == true)
                {
                    objects[i].freecolleagues();
                    objects.RemoveAt(i);
                }
            notifyTree();
        }
        public void delete_all()
        {
            for (int i = objects.Count() - 1; i >= 0; i--)
                    objects.RemoveAt(i);
            notifyTree();
        }
        public void GroupObjects()
        {
            Group g = new Group();
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                
                if (objects[i].getselection())
                {
                    if (objects[i].getslavesel())
                    {
                        g.changeslavesel_to(true); objects[i].changeslavesel_to(false);
                    }
                    g.add_in_Group(objects[i]);
                    objects.RemoveAt(i);
                }
            }
            if (g.getGroup().Count < 2)
                objects.AddRange(g.getGroup());
            else
                objects.Add(g);
            notifyTree();
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
            notifyTree();
        }
        public void SaveStorage(string _name)
        {
            StreamWriter save = new StreamWriter(_name, false);
            save.WriteLine(objects.Count.ToString());
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
                count = int.Parse(load.ReadLine());
            }
            catch
            {
                load.Close();
                return;
            }

            for (int i = 0; i < count; i++)
            {
                tempModel = factory.CreateObject(load.ReadLine());
                if (tempModel != null)
                {
                    tempModel.LoadObject(load, factory);
                    objects.Add(tempModel);
                }
            }
            load.Close();
            notifyTree();
        }
    }

}