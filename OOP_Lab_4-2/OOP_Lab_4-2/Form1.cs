using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OOP_Lab_4_2.Properties;

// Разделяем логику и визуальные компоненты!!!
// Данные, которые хранятся, логика, которая обрабатывает эти данные и их визуальное отображение должны быть разделены (храним все в разных местах)

// Значения и правила, которые позволяют обрабатывать это значение одним образом и не позволяют обрабатывать другим,
// оно должно быть вынесено в отдельный объект


namespace OOP_Lab_4_2
{
    public partial class Form1 : Form
    {
        Model model; // Ссылка на Model
        EventHandler handler;
        public Form1()
        {
            InitializeComponent();
            model = new Model(); // Форма создает модель
            model.observer += new EventHandler(this.UpdateFromModel); // Форма подписывается на обновление модели
                                                                      // При обновлении формы вызывается метод UodateFromModel
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            model.saveSettings();
        }

        private void numericUpDownA_ValueChanged(object sender, EventArgs e)
        {
            // Передаем визуальные значения в модель
            model.set_A_Value(Decimal.ToInt32(numericUpDownA.Value)); // Decimal представляет десятичное число с плавающей запятой.
        }
        private void numericUpDownB_ValueChanged(object sender, EventArgs e)
        {
            model.set_B_Value(Decimal.ToInt32(numericUpDownB.Value));
        }

        private void numericUpDownC_ValueChanged(object sender, EventArgs e)
        {
            model.set_C_Value(Decimal.ToInt32(numericUpDownC.Value));
        }


        // Все визуальные компоненты, которые должны отображаться значения, все эти компоненты возьмут значения из модели и его отобразят
        private void UpdateFromModel(object sender, EventArgs e)
        {
            // Когда модель обновляется, компоненты, отображаемые на форме, они все разом должны получить 
            // обновленное значение

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
            catch(Exception exception) { } // Exception - базовый для всех типов исключений
        }

        private void textBoxB_Leave(object sender, EventArgs e)
        {
            try
            {
                int valueB = Convert.ToInt32(textBoxB.Text);
                model.set_B_Value(valueB);
            }
            catch (Exception exception) { }
        }

        private void textBoxC_Leave(object sender, EventArgs e)
        {
            try
            {
                int valueC = Convert.ToInt32(textBoxC.Text);
                model.set_C_Value(valueC);
            }
            catch (Exception exception) { }
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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Avalue = trackBarA.Value;
            Properties.Settings.Default.Cvalue = trackBarC.Value;
            Properties.Settings.Default.Save();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Avalue = trackBarA.Value;
            Properties.Settings.Default.Bvalue = trackBarC.Value;
            Properties.Settings.Default.Save();
        }
    }


    // Model описывает предметную область, она никак не связана с тем, как данные, хранимые в Model буду отображаться на экране
    public class Model
    {
        private int valueA, valueB, valueC; // Место, где хранятся наши значения
        public System.EventHandler observer;


        // Описание всех бизнес-правил, которые позволяют менять значения одним образом, и не позволяют дургим
        public Model()
        {
            valueA = Properties.Settings.Default.Avalue;
            valueB = Properties.Settings.Default.Bvalue;
            valueC = Properties.Settings.Default.Cvalue;

        }
        
        
        public void set_A_Value(int value)
        {
            observer.Invoke(this, EventArgs.Empty);
            if (value < 0) return;
            if (value > 100) return;
            if (value > valueB) set_B_Value(value);
            if (value > valueC) set_C_Value(value);
            valueA = value;
            observer.Invoke(this, null);
        }

        public void set_B_Value(int value)
        {
            observer.Invoke(this, EventArgs.Empty);
            if (value < 0) return;
            if (value > 100) return;
            if (value < valueA) return;
            if (value > valueC) return;
            valueB = value;
            observer.Invoke(this, null); // При изменении значений обновлений у всех, кто подписан на модель
            // Модель будет уведомлять всех тех, кто на нее подписался
        }

        public void set_C_Value(int value)
        {
            observer.Invoke(this, EventArgs.Empty);
            if (value < 0) return;
            if (value > 100) return;
            if (value < valueA) set_A_Value(value);
            if (value < valueB) set_B_Value(value);
            valueC = value;
            observer.Invoke(this, null);
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
            valueA = Settings.Default.Avalue;
            valueB = Settings.Default.Cvalue;
            valueC = Settings.Default.Bvalue;
        }
    }
}
