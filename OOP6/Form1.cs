using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP6
{
    public partial class Form : System.Windows.Forms.Form
    {
        Storage myStorage = new Storage();
        bool controlUp = false;
        Color btn_color = Color.Black;
        bool circle = true;
        bool square = false;
        bool triangle = false;

        public Form()
        {
            InitializeComponent();
        }


        private void panel1_Paint(object sender, PaintEventArgs e) // Отрисовка всех объектов из контейнера
        {
            for (int i = 0; i < myStorage.getSize(); i++)
            {
                myStorage.getObject(i).OnPaint(e);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e) // Создаем фигуру, где находится курсор
        {
            this.ActiveControl = null;
            if (circle)
                myStorage.AddObject(new CCircle(e.Location, btn_color, picturbx), e, controlUp);

            if (square)
                myStorage.AddObject(new CSquare(e.Location, btn_color, picturbx), e, controlUp);

            if (triangle)
                myStorage.AddObject(new Triangle(e.Location, btn_color, picturbx), e, controlUp);
            picturbx.Invalidate();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.W) // Увеличение
                for (int i = 0; i < myStorage.getSize(); i++)
                {
                    if (myStorage.getObject(i).getselection() == true)
                        myStorage.getObject(i).increase_Size();
                }

            if (e.KeyData == Keys.S)// Уменьшение
                for (int i = 0; i < myStorage.getSize(); i++)
                {
                    if (myStorage.getObject(i).getselection() == true)
                        myStorage.getObject(i).decrease_Size();
                }

            if (e.Control) // Выбор объектов
                controlUp = true;

            if (e.KeyData == Keys.Delete) // Удаление объектов
                myStorage.delete_selectedObjects();


            // Обработка 4 кнопок движения на стрелки

            if (e.KeyData == Keys.Right) // Движение вправо
            {
                for (int i = 0; i < myStorage.getSize(); i++)
                {
                    if (myStorage.getObject(i).getselection())
                        myStorage.getObject(i).move_Object(6, 0);
                }
            }
            if (e.KeyData == Keys.Left) // Движение влево
            {
                for (int i = 0; i < myStorage.getSize(); i++)
                {
                    if (myStorage.getObject(i).getselection())
                        myStorage.getObject(i).move_Object(-6, 0);
                }
            }
            if (e.KeyData == Keys.Down) // Тут +1, ибо ось Y направлена вниз
            {
                for (int i = 0; i < myStorage.getSize(); i++)
                {
                    if (myStorage.getObject(i).getselection())
                        myStorage.getObject(i).move_Object(0, 6);
                }
            }
            if (e.KeyData == Keys.Up) // Тут -1, ибо ось Y направлена вниз
            {
                for (int i = 0; i < myStorage.getSize(); i++)
                {
                    if (myStorage.getObject(i).getselection())
                        myStorage.getObject(i).move_Object(0, -6);
                }
            }
            picturbx.Invalidate();
        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            controlUp = false;
        }

        private void btn_red_Click(object sender, EventArgs e)
        {
            btn_color = ((Button)sender).BackColor;
            for (int i = 0; i < myStorage.getSize(); i++)
            {
                if (myStorage.getObject(i).getselection())
                    myStorage.getObject(i).object_color = btn_color;
            }
            picturbx.Invalidate();
            this.ActiveControl = null;
        }

        private void btn_crcle_Click(object sender, EventArgs e)
        {
            circle = true;
            square = false;
            triangle = false;
        }

        private void btn_sqr_Click(object sender, EventArgs e)
        {
            circle = false;
            square = true;
            triangle = false;
        }

        private void btn_tri_Click(object sender, EventArgs e)
        {
            circle = false;
            square = false;
            triangle = true;
        }

        private void btn_manycolor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btn_color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;

                for (int i = 0; i < myStorage.getSize(); i++)    //изменяем цвет у всех выбранных объектов
                {
                    if (myStorage.getObject(i).getselection())
                        myStorage.getObject(i).object_color = btn_color;
                }
                picturbx.Invalidate();
            }
            this.ActiveControl = null; // Убираем фокус с кнопки, не убирая фокус с фигуры, для ее движения
        }

        private void btn_clr_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < myStorage.getSize(); i++)
            {
                myStorage.delete_all();
            }
            picturbx.Invalidate();
            this.ActiveControl = null;
        }

        private void picturbx_Resize(object sender, EventArgs e)
        {
            for (int i = myStorage.getSize() - 1; i >= 0; i--)
            {
                myStorage.getObject(i).picturbx = picturbx; // Для вычисления высоты и ширины picturebox'а, чтобы фигуры не выходили за края
            }
        }
    }
}
