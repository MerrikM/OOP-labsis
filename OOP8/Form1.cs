using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP8
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Storage myStorage;
        bool controlUp = false;
        bool Tup = false;
        Color btn_color = Color.Black;
        bool circle = true;
        bool square = false;
        bool triangle = false;

        TreeNode tn = new TreeNode();
        public Form()
        {
            myStorage = new Storage();
            myStorage.addObserver(new TreeObserver());
            InitializeComponent();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < myStorage.getSize(); i++)
            {
                myStorage.getObject(i).OnPaint(e);
            }
            
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null;
            if (circle)
                myStorage.AddObject(new CCircle(e.Location, btn_color, picturbx), e, controlUp, Tup);

            if (square)
                myStorage.AddObject(new CSquare(e.Location, btn_color, picturbx), e, controlUp, Tup);

            if (triangle)
                myStorage.AddObject(new Triangle(e.Location, btn_color, picturbx), e, controlUp, Tup);
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(myStorage.gett());
            picturbx.Invalidate();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Add) //Увеличение
                for (int i = 0; i < myStorage.getSize(); i++)
                {
                    if (myStorage.getObject(i).getselection() == true)
                        myStorage.getObject(i).increase_Size();
                }

            if (e.KeyData == Keys.Subtract)//Уменьшение
                for (int i = 0; i < myStorage.getSize(); i++)
                {
                    if (myStorage.getObject(i).getselection() == true)
                        myStorage.getObject(i).decrease_Size();
                }

            if (e.Control) //Выбор объектов
                controlUp = true;
            if (e.KeyCode == Keys.T)
                Tup = true;

            if (e.KeyData == Keys.Delete)//Удаление объектов
            {
                myStorage.delete_selectedObjects();
                for(int i=0; i<myStorage.getSize(); i++)
                {
                    myStorage.getObject(i).RemoveColleagues();
                }
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(myStorage.gett());
            }
            if (e.KeyData == Keys.Q)
            { 
            }
            //обработка 4 кнопок движения

            if (e.KeyData == Keys.D)//Движение вправо
            {
                for (int i = 0; i < myStorage.getSize(); i++)
                {
                    if (myStorage.getObject(i).getselection())
                        myStorage.getObject(i).move_Object(6, 0);
                }
            }
            if (e.KeyData == Keys.A)//Движение влево
            {
                for (int i = 0; i < myStorage.getSize(); i++)
                {
                    if (myStorage.getObject(i).getselection())
                        myStorage.getObject(i).move_Object(-6, 0);
                }
            }
            if (e.KeyData == Keys.S)//тут +1, ибо ось Y направлена вниз
            {
                for (int i = 0; i < myStorage.getSize(); i++)
                {
                    if (myStorage.getObject(i).getselection())
                        myStorage.getObject(i).move_Object(0, 6);
                }
            }
            if (e.KeyData == Keys.W)//тут -1, ибо ось Y направлена вниз
            {
                for (int i = 0; i < myStorage.getSize(); i++)
                {
                    if (myStorage.getObject(i).getselection())
                        myStorage.getObject(i).move_Object(0, -6);
                }
            }
            if (e.Shift)
            {
                myStorage.GroupObjects();
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(myStorage.gett());
            }
            if (e.KeyData == Keys.G)
            {
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(myStorage.gett());
                myStorage.UngroupObjects();
            }
            picturbx.Invalidate();
        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            controlUp = false;
            Tup = false;
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
            this.ActiveControl = null;
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
                myStorage.getObject(i).SetBord(picturbx.Width,picturbx.Height);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = picturbx;
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = @"C:\Users\neket\Desktop\projects\visual\OOP7";
            sfd.Filter = "TXT files|*.txt";
            if (sfd.ShowDialog() == DialogResult.Cancel) return;
            string filename = sfd.FileName;
            myStorage.SaveStorage(filename);
            this.ActiveControl = picturbx;
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TXT files|*.txt";
            ofd.InitialDirectory = @"C:\Users\neket\Desktop\projects\visual\OOP7";
            if (ofd.ShowDialog() == DialogResult.Cancel) return;
            string path = ofd.FileName;
            myStorage.delete_all();
            MyObjectsFactory factory = new MyObjectsFactory();
            myStorage.LoadStorage(factory, path);
            picturbx.Invalidate();
            treeView1.Invalidate();
            this.ActiveControl = picturbx;
        }
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            for (int i = 0; i < myStorage.getSize(); i++)
            {
                if (treeView1.TopNode.LastNode.Index >= i) // Выбор объектов в меню Storage
                    myStorage.getObject(i).changeselection_to(treeView1.TopNode.Nodes[i].Checked);
            }
            picturbx.Invalidate();
        }
        private void treeView1_MouseLeave_1(object sender, EventArgs e)
        {
            this.ActiveControl = null; 
        }
        private void treeView1_MouseEnter(object sender, EventArgs e)
        {
            this.ActiveControl = picturbx; // Приоритет фокуса на pictureBox
        }
    }
}
