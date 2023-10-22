using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Parcial2Pilas
{
    public partial class Form2 : Form
    {
        Stack<char> pila = new Stack<char>();
        Stack<char> pila2 = new Stack<char>();
        Stack<char> resultado = new Stack<char>();
        public Form2()
        {
            InitializeComponent();
            //ListBoxPrede();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            double temp2=0, temp=0;
            if (string.IsNullOrWhiteSpace(textBox1.Text)||string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Debes llenar todos los campos");
            }
            else
            {
                listBox1.Items.Clear();
                pila = new Stack<char>(textBox1.Text.ToCharArray());
                pila2 = new Stack<char>(textBox2.Text.ToCharArray());
                MostrarPila(pila);
                listBox1.Items.Add("------------------------------");
                MostrarPila(pila2);
                listBox1.Items.Add("------------------------------");

                while (pila.Count > 0 || pila2.Count > 0)
                {
                    double num1 = 0, num2 = 0;
                    temp2 = 0;

                    if (pila.Count > 0)
                    {
                        if (double.TryParse(pila.Pop().ToString(), out num1))
                        {
                            temp2 += num1;
                        }
                    }

                    if (pila2.Count > 0)
                    {
                        if (double.TryParse(pila2.Pop().ToString(), out num2))
                        {
                            temp2 += num2;
                        }
                    }

                    temp2 += temp;
                    if (temp2 > 9)
                    {
                        temp = 1;
                        resultado.Push(temp2.ToString()[1]);
                    }
                    else
                    {
                        temp = 0;
                        resultado.Push((char)(temp2+'0'));
                    }
                }
                    
                MostrarPila(resultado);
                listBox1.Items.Add("------------------------------");
                MostrarResFormato(resultado);
                resultado.Clear();
                textBox1.Clear();
                textBox2.Clear();

            }
           
        }
        public void MostrarPila(Stack<char> pila)
        {
            foreach (char digito in pila)
            {
                listBox1.Items.Add(digito);
            }
        }
        public void MostrarResFormato(Stack<char> pila)
        {
            string pilaComoString = string.Join("", pila);
            listBox1.Items.Add("Total: "+pilaComoString);
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)//este solo acepta numros enteros
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox1_KeyPress(sender, e);
        }
    }
}