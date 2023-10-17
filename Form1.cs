using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Parcial2Listas
{
    public partial class Form1 : Form
    {
        string nombreBase = "Producto", nuevoNombre = "";
        int contadorNombres = 0;
        Random miRandom = new Random();
        List<int> numeros = new List<int>();
        List<int> pares = new List<int>();
        List<int> impares = new List<int>();
        List<Alumno> misAlumnos = new List<Alumno>();
        List<Alumno> aprobados = new List<Alumno>();
        List<Alumno> reprobados = new List<Alumno>();
        List<Producto> misProductos = new List<Producto>();
        List<List<string>> misPalabras = new List<List<string>>();
        List<char> listaDeCaracteres = new List<char>();
        Producto primerAlta;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            contadorNombres++;
            nuevoNombre = nombreBase + contadorNombres.ToString();
            if (primerAlta == null)
            {
                primerAlta = new Producto(nuevoNombre, miRandom.Next(1, 250), miRandom.Next(1, 20), primerAlta);
            }
            else
            {
                Producto temporal;
                for (temporal = primerAlta; temporal.next != null; temporal = temporal.next) ;
                temporal.next = new Producto(nuevoNombre, miRandom.Next(1, 250), miRandom.Next(1, 20), null);
            }
            ActualizarDataGridView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Producto temporal = primerAlta, temporal2 = null;
            int valor = (int)numericUpDown1.Value;
            if (contador() > 0 && valor <= contador() && valor != 0)
            {
                for (int i = 0; i < valor; i++)
                {
                    temporal2 = temporal;
                    temporal = temporal.next;
                }
                if (temporal2.capacidad != 0)
                {
                    temporal2.retirado++;
                    temporal2.capacidad--;
                }
                else
                {
                    MessageBox.Show("Sin existencias");
                }

            }
            else
            {
                if (contador() == 0)
                {
                    MessageBox.Show("Lista de productos vacia");
                }
                else if (contador() < valor || valor == 0)
                {
                    MessageBox.Show("Numero fuera del rango del indice");
                }
            }
            ActualizarDataGridView();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            numeros.Clear();
            pares.Clear();
            impares.Clear();
            for (int i = 0; i < 10; i++)
            {
                numeros.Add(miRandom.Next(1, 100));
            }
            foreach (var item in numeros)
            {
                if (item % 2 != 0)
                {
                    impares.Add(item);
                }
                else
                {
                    pares.Add(item);
                }
            }
            MostrarListas(numeros, listBox1);
            MostrarListas(pares, listBox2);
            MostrarListas(impares, listBox3);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string nombre = textBox4.Text;
            double calif1, calif2, calif3;

            if (string.IsNullOrWhiteSpace(nombre) ||
                !double.TryParse(textBox1.Text, out calif1) ||
                !double.TryParse(textBox2.Text, out calif2) ||
                !double.TryParse(textBox3.Text, out calif3))
            {
                MessageBox.Show("LLena todos los datos");
            }
            else
            {
                misAlumnos.Add(new Alumno(nombre, calif1, calif2, calif3, Math.Round((calif1 + calif2 + calif3) / 3, 2)));
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Clasificar();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            string name = textBox5.Text;
            int valor;

            if (string.IsNullOrWhiteSpace(name) || !int.TryParse(textBox6.Text, out valor))
            {
                MessageBox.Show("LLena todos los datos");
            }
            else
            {
                misProductos.Add(new Producto(name, valor));
            }
            textBox5.Clear();
            textBox6.Clear();
            MostrarSumaTotal();
            misProductos = misProductos.OrderBy(producto => producto.name).ToList();
            ActualizarDataGridView3();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            int indice;
            if (misProductos.Count() > 0)
            {
                if (!int.TryParse(textBox7.Text, out indice))
                {
                    MessageBox.Show("Ingresa un ID");
                }
                else
                {
                    if (indice <= misProductos.Count() && indice != 0)
                    {
                        indice--;
                        misProductos.RemoveAt(indice);
                    }
                    else
                    {
                        MessageBox.Show("Numero fuera del rango del indice");
                    }
                }
            }
            else
            {
                MessageBox.Show("Lista vacia");
            }
            textBox7.Clear();
            MostrarSumaTotal();
            ActualizarDataGridView3();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            string name = textBox9.Text;
            char primeraLetra;
            char primeraLetraTemp;
            bool encontrado = true;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Ingresa una palabra");
            }
            else
            {
                if (misPalabras.Count() == 0)
                {
                    misPalabras.Add(new List<string> { name });
                }
                else
                {
                    primeraLetra = char.ToUpper(name[0]);
                    foreach (var item in misPalabras)
                    {
                        primeraLetraTemp = char.ToUpper(item[0][0]);
                        if (primeraLetra == primeraLetraTemp)
                        {
                            item.Add(name);
                            encontrado = false;
                            break;
                        }
                    }
                    if (encontrado)
                    {
                        misPalabras.Add(new List<string> { name });
                    }
                }
            }
            textBox9.Clear();
            MostrarListas();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            string palabra = textBox10.Text;
            if (string.IsNullOrWhiteSpace(palabra))
            {
                MessageBox.Show("Ingresa una palabra");
            }
            else
            {
                textBox11.Text = Invertir(palabra).ToString();
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            string palabra = textBox12.Text;
            if (string.IsNullOrWhiteSpace(palabra))
            {
                MessageBox.Show("Ingresa una palabra");
            }
            else
            {
                if (palabra.ToUpper()==Invertir(palabra).ToUpper())
                {
                    MessageBox.Show("ES UN PALINDROMO");
                }
                else
                {
                    MessageBox.Show("NO ES UN PALINDROMO");
                }
            }
            textBox12.Clear();
        }
        static string Invertir(string palabra)
        {
            char temp;
            List<char> listaDeCaracteres = palabra.ToList();
            int ultima = listaDeCaracteres.Count();
            int limite = ultima / 2;
            for (int i = 0; i < limite; i++)
            {
                ultima--;
                temp = listaDeCaracteres[i];
                listaDeCaracteres[i] = listaDeCaracteres[ultima];
                listaDeCaracteres[ultima] = temp;
            }
            return new string(listaDeCaracteres.ToArray());
        }
        public int Total()
        {
            int sumaTotal = 0;
            foreach (var item in misProductos)
            {
                sumaTotal = sumaTotal + item.valor;
            }
            return sumaTotal;
        }
        private void MostrarSumaTotal()
        {
            double sumaTotal = Total();
            textBox8.Text = sumaTotal.ToString();
        }

        public int contador()
        {
            Producto temporal = primerAlta;
            int contador = 0;

            while (temporal != null)
            {
                contador++;
                temporal = temporal.next;
            }

            return contador;
        }
        private void Clasificar()
        {
            if (misAlumnos.Count() > 0)
            {
                foreach (var item in misAlumnos)
                {
                    if (item.promedio >= 7)
                    {
                        aprobados.Add(item);
                    }
                    else
                    {
                        reprobados.Add(item);
                    }
                }
                misAlumnos.Clear();
            }
            else
            {
                MessageBox.Show("No hay alumnos para clasificar");
            }
            ActualizarDataGridView2(aprobados, dataGridView3);
            ActualizarDataGridView2(reprobados, dataGridView4);
        }
        private void ActualizarDataGridView3()
        {
            int index = 1;
            dataGridView5.Rows.Clear();
            foreach (var item in misProductos)
            {
                dataGridView5.Rows.Add(index, item.name, item.valor);
                index++;
            }
        }
        private void ActualizarDataGridView2(List<Alumno> lista, DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            foreach (var item in lista)
            {
                dataGridView.Rows.Add(item.name, item.calif1, item.calif2, item.calif3, item.promedio);
            }
        }
        private void ActualizarDataGridView()
        {
            int ID = 1;
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            Producto temporal = primerAlta;
            while (temporal != null)
            {
                dataGridView1.Rows.Add(ID, temporal.name, temporal.valor, temporal.capacidad);
                temporal = temporal.next;
                ID++;
            }
            temporal = primerAlta;
            while (temporal != null)
            {
                if (temporal.retirado > 0)
                {
                    dataGridView2.Rows.Add(temporal.name, temporal.valor, temporal.retirado);
                }
                temporal = temporal.next;
            }
        }
        private void MostrarListas(List<int> lista, ListBox listBox)
        {
            listBox.Items.Clear();

            foreach (int item in lista)
            {
                listBox.Items.Add(item);
            }
        }
        private void MostrarListas()
        {
            listBox4.Items.Clear();

            for (int i = 0; i < misPalabras.Count; i++)
            {
                char letraInicial = misPalabras[i][0][0];
                listBox4.Items.Add($"Lista ({letraInicial}): {string.Join(", ", misPalabras[i])}");
            }
        }
        private void programa1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxPrograma1.Visible = true;
            groupBoxPrograma2.Visible = false;
            groupBoxPrograma3.Visible = false;
            groupBoxPrograma4.Visible = false;
            groupBoxPrograma5.Visible = false;
            groupBoxPrograma6.Visible = false;
            groupBoxPrograma7.Visible = false;
        }

        private void programa2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxPrograma1.Visible = false;
            groupBoxPrograma2.Visible = true;
            groupBoxPrograma3.Visible = false;
            groupBoxPrograma4.Visible = false;
            groupBoxPrograma5.Visible = false;
            groupBoxPrograma6.Visible = false;
            groupBoxPrograma7.Visible = false;
        }

        private void programa3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxPrograma1.Visible = false;
            groupBoxPrograma2.Visible = false;
            groupBoxPrograma3.Visible = true;
            groupBoxPrograma4.Visible = false;
            groupBoxPrograma5.Visible = false;
            groupBoxPrograma6.Visible = false;
            groupBoxPrograma7.Visible = false;
        }
        private void programa4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxPrograma1.Visible = false;
            groupBoxPrograma2.Visible = false;
            groupBoxPrograma3.Visible = true;
            groupBoxPrograma4.Visible = true;
            groupBoxPrograma5.Visible = false;
            groupBoxPrograma6.Visible = false;
            groupBoxPrograma7.Visible = false;
        }
        private void programa5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxPrograma1.Visible = false;
            groupBoxPrograma2.Visible = false;
            groupBoxPrograma3.Visible = false;
            groupBoxPrograma4.Visible = false;
            groupBoxPrograma5.Visible = true;
            groupBoxPrograma6.Visible = false;
            groupBoxPrograma7.Visible = false;
        }
        private void programa6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxPrograma1.Visible = false;
            groupBoxPrograma2.Visible = false;
            groupBoxPrograma3.Visible = false;
            groupBoxPrograma4.Visible = false;
            groupBoxPrograma5.Visible = false;
            groupBoxPrograma6.Visible = true;
            groupBoxPrograma7.Visible = false;

        }
        private void programa7PalindromoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxPrograma1.Visible = false;
            groupBoxPrograma2.Visible = false;
            groupBoxPrograma3.Visible = false;
            groupBoxPrograma4.Visible = false;
            groupBoxPrograma5.Visible = false;
            groupBoxPrograma6.Visible = false;
            groupBoxPrograma7.Visible = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.' || e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf('.') > -1 || (sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox1_KeyPress(sender, e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox1_KeyPress(sender, e);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox6_KeyPress(sender, e);
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox9_KeyPress(sender, e);
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox9_KeyPress(sender, e);
        }
    }
}