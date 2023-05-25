using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Forms;
using System.Reflection;

namespace OOP8
{
    public class Group : Model
    {
        private List<Model> groupObjects;

        int left_Board;  //Границы рамки группы
        int right_Board;
        int up_Board;
        int down_Board;


        public Group()
        {
            groupObjects = new List<Model>();
            selection = true;
            left_Board = int.MaxValue;
            right_Board = int.MinValue;
            up_Board = int.MaxValue;
            down_Board = int.MinValue;
            Colleagues = new List<Model>();
        }



        //"Распаковка" нашей группы(получаем еще один List<Model>, только из отрисовываемых объектов //!!!!!!!!!!
       


        //Возвращаем группу 
        public List<Model> getGroup()
        {
            return groupObjects;
        }


        public void add_in_Group(Model temp_object)
        {
            groupObjects.Add(temp_object);
            Point a = temp_object.getloc();
        }


        //получить информацию, выделена ли наша группа
        public override bool getselection()
        {
            return selection;
        }


        //Установить цвет у всей группы
        public override void setColor(Color color)
        {
            foreach (var obj in groupObjects)
                obj.setColor(color);
        }

        public override (int, int, int, int) getGroupBoards()
        {
            (int, int, int, int) tuple = (int.MaxValue, int.MinValue, int.MaxValue, int.MinValue);
            for (int i = 0; i < this.groupObjects.Count; i++)
            {
                tuple = this.groupObjects[i].getGroupBoards();
                tuple.Item1 = Math.Min(left_Board, tuple.Item1);
                tuple.Item2 = Math.Max(right_Board, tuple.Item2);
                tuple.Item3 = Math.Min(up_Board, tuple.Item3);
                tuple.Item4 = Math.Max(down_Board, tuple.Item4);
            }
            return tuple;
        }
        public void setboarders ()
        {
            left_Board = int.MaxValue;
            right_Board = int.MinValue;
            up_Board = int.MaxValue;
            down_Board = int.MinValue;
            (int, int, int, int) tuple = (int.MaxValue, int.MinValue, int.MaxValue, int.MinValue);
            for (int i = 0; i < this.groupObjects.Count; i++)
            {
                tuple = this.groupObjects[i].getGroupBoards();
                left_Board = Math.Min(left_Board, tuple.Item1);
                right_Board = Math.Max(right_Board, tuple.Item2);
                up_Board = Math.Min(up_Board, tuple.Item3);
                down_Board = Math.Max(down_Board, tuple.Item4);
            }
            this.location.X = (right_Board + left_Board) / 2;
            this.location.Y = (down_Board + up_Board) / 2;
        }
        //Отрисовка объектов группы и рамки
        public override void OnPaint(PaintEventArgs e)
        {

            //Отрисывывает все элементы
            foreach (var obj in groupObjects)
            {
                obj.changeselection_to(false);  
                obj.OnPaint(e);
            }
            this.setboarders();

            //Рисует рамку
            Pen pen = new Pen(Color.Red);
            if (selection)
                pen.Width = 3;
            else
                pen.Width = 1;
            e.Graphics.DrawRectangle(pen, left_Board - 8, up_Board - 8, right_Board - left_Board + 16, down_Board - up_Board + 16);
            Draw_Lines(e);
        }

        //Попали ли мы в область рамки группы
        public override bool isPicked(MouseEventArgs e, bool controlUp, bool Tup)
        {
            if ((e.X >= left_Board - 8) & (e.X <= right_Board + 16) &
                (e.Y >= up_Board - 8) & (e.Y <= down_Board + 16) & (controlUp||Tup))
            {
                if(controlUp)selection = !selection; //Инвертируем выделенность
                return true;
            }
            return false;//не попал в рамку - ниче не делает
        }


        //Можем ли сдвинуть ВСЕ объекты группы
        public override bool check_Location(int point_X, int point_Y)
        {
            foreach (var obj in groupObjects)
                if (obj.check_Location(point_X,point_Y) == false)
                    return false;
            return true;
        }


        //Можем ли увеличить ВСЕ объекты группы
        public override bool isIncreasable(int _radix)
        {
            foreach (var obj in groupObjects)
                if (obj.isIncreasable(_radix) == false)
                    return false;
            return true;
        }


        //Двигает ВСЕ объекты группы
        public override void move_Object(int _X, int _Y)
        {
            if (mosh)
                notifyObjects(_X, _Y);
            if (check_Location(_X,_Y))
                foreach (var obj in groupObjects)
                    obj.move_Object(_X, _Y);
            this.location.X += _X;
            this.location.Y += _Y;
        }


        //Увеличить размер ВСЕХ объектов группы
        public override void increase_Size()
        {
            if (isIncreasable(this.RADIX))
                foreach (var obj in groupObjects)
                    obj.increase_Size();
        }


        //Уменьшить размер ВСЕХ объектов группы
        public override void decrease_Size()
        {
            foreach (var obj in groupObjects)
                obj.decrease_Size();
        }


        public override void SaveObject(StreamWriter save)
        {
            save.WriteLine("Group");
            save.WriteLine(this.selection.ToString());
            save.WriteLine(groupObjects.Count.ToString());
            foreach (var obj in groupObjects)
                obj.SaveObject(save);
        }


        public override void LoadObject(StreamReader load, ModelFactory factory)
        {
            this.selection = bool.Parse(load.ReadLine());
            int count = int.Parse(load.ReadLine());
            for (int i = 0; i < count; i++)
            {
                Model temp;
                temp = factory.CreateObject(load.ReadLine());
                temp.LoadObject(load, factory);
                groupObjects.Add(temp);
            }
            this.location= groupObjects[0].getloc();
        }
        public override bool isMoshniy(Model o)
        {
            foreach (var obj in groupObjects)
                if (obj.isMoshniy(o))
                    return true;
            return false;
        }
    }
}
