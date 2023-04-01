using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Lab_4_2
{
    public partial class Form1 : Form
    {
        Model model;
        EventHandler handler;
        public Form1()
        {
            InitializeComponent();
            model = new Model();
            model.observer += new EventHandler(this.UpdateFromModel);

            handler += textBoxA_Leave;
            handler += textBoxB_Leave;
            handler += textBoxC_Leave;

            textBoxA.Leave += textBoxA_Leave;
            textBoxB.Leave += textBoxB_Leave;
            textBoxC.Leave += textBoxC_Leave;
        }

        private void numericUpDownA_ValueChanged(object sender, EventArgs e)
        {
            model.set_A_Value(Decimal.ToInt32(numericUpDownA.Value));
        }
        private void numericUpDownB_ValueChanged(object sender, EventArgs e)
        {
            model.set_B_Value(Decimal.ToInt32(numericUpDownB.Value));
        }

        private void numericUpDownC_ValueChanged(object sender, EventArgs e)
        {
            model.set_C_Value(Decimal.ToInt32(numericUpDownC.Value));
        }

        private void UpdateFromModel(object sender, EventArgs e)
        {
            int valueA = model.getA_Value();
            int valueB = model.getB_Value();
            int valueC = model.getC_Value();

            textBoxA.Text = valueA.ToString();
            textBoxB.Text = valueB.ToString();
            textBoxC.Text = valueC.ToString();

            numericUpDownA.Value = Convert.ToDecimal(valueA);
            numericUpDownB.Value = Convert.ToDecimal(valueB);
            numericUpDownC.Value = Convert.ToDecimal(valueC);

            trackBarA.Value = valueA;
            trackBarB.Value = valueB;
            trackBarC.Value = valueC;
        }

        private void textBoxA_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                handler.Invoke(sender, null);
            }
        }

        private void textBoxA_Leave(object sender, EventArgs e)
        {
            try
            {
                int valueA = Convert.ToInt32(textBoxA.Text);
                model.set_A_Value(valueA);
            }
            catch(Exception ex) { }
        }

        private void textBoxB_Leave(object sender, EventArgs e)
        {
            try
            {
                int valueB = Convert.ToInt32(textBoxB.Text);
                model.set_B_Value(valueB);
            }
            catch (Exception ex) { }
        }

        private void textBoxC_Leave(object sender, EventArgs e)
        {
            try
            {
                int valueC = Convert.ToInt32(textBoxC.Text);
                model.set_C_Value(valueC);
            }
            catch (Exception ex) { }
        }

        private void trackBarA_Scroll(object sender, EventArgs e)
        {
            model.set_A_Value(trackBarA.Value);
        }

        private void trackBarB_Scroll(object sender, EventArgs e)
        {
            model.set_B_Value(trackBarB.Value);
        }

        private void trackBarC_Scroll(object sender, EventArgs e)
        {
            model.set_C_Value(trackBarC.Value);
        }
    }

    public class Model
    {
        private int valueA, valueB, valueC;
        public System.EventHandler observer;

        public void set_A_Value(int value)
        {
            observer.Invoke(this, EventArgs.Empty);
            if (value < 0) return;
            if (value > 100) return;
            if (value > valueB) set_B_Value(value);
            if (value > valueC) set_C_Value(value);
            valueA = value;
            observer.Invoke(this, EventArgs.Empty);
        }

        public void set_B_Value(int value)
        {
            observer.Invoke(this, EventArgs.Empty);
            if (value < 0) return;
            if (value > 100) return;
            if (value < valueA) return;
            if (value > valueC) return;
            valueB = value;
            observer.Invoke(this, EventArgs.Empty);
        }

        public void set_C_Value(int value)
        {
            observer.Invoke(this, EventArgs.Empty);
            if (value < 0) return;
            if (value > 100) return;
            if (value < valueA) set_A_Value(value);
            if (value < valueB) set_B_Value(value);
            valueC = value;
            observer.Invoke(this, EventArgs.Empty);
        }

        public int getA_Value()
        {
            return valueA;
        }
        public int getB_Value()
        {
            return valueB;
        }
        public int getC_Value()
        {
            return valueC;
        }
        public void saveSettings()
        {
            
        }

    }
}
