﻿using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Verificador_Precios
{
    public partial class Form1 : Form
    {
        private int segundos = 0;

        private String codigo = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(this.Width / 2 - pictureBox1.Width / 2, 0);
            label1.Location = new Point(this.Width / 2 - label1.Width / 2, pictureBox1.Height + 10);
            pictureBox2.Location = new Point(this.Width/2 - pictureBox2.Width/2,this.Height/2);
            label3.Location = new Point(this.Width /8, pictureBox1.Height + 460);
            label4.Location = new Point(this.Width / 8, pictureBox1.Height + 500);
            timer2.Enabled = true;
            timer3.Enabled = true;

        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (e.KeyChar == 13)
            {
                //MessageBox.Show("vamos a buscar el producto "+codigo);
                try
                {
                    MySqlConnection servidor;
                    servidor = new MySqlConnection("server = 127.0.0.1; user = root; database = verificador_de_precios; SSL Mode = None; ");
                    servidor.Open();
                    string query = "SELECT producto_nombre,producto_cantidad, producto_precio, producto_imagen FROM productos WHERE producto_codigo =" + codigo + ";";
                    //MessageBox.Show(query);
                    MySqlCommand consulta;
                    consulta = new MySqlCommand(query, servidor);
                    MySqlDataReader resultado = consulta.ExecuteReader();
                    if (resultado.HasRows)
                    {
                        resultado.Read();
                        //MessageBox.Show(resultado.GetString(1));
                        label2.Visible = true;
                        pictureBox2.Visible=false;
                        pictureBox3.Visible = true;
                        label2.Text = resultado.GetString(0)+Environment.NewLine+"Precio: $"+resultado.GetString(2) +
                            Environment.NewLine + "Stock: " + resultado.GetString(1);
                        pictureBox3.ImageLocation = resultado.GetString(3);
                        pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                        segundos = 0;
                        timer1.Enabled = true;
                     
                    }
                    else
                    {
                        MessageBox.Show("Llame al supervisor, el producto no fue encontrado");
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.ToString(), "Titulo", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
                codigo = "";
                label3.Visible = false;
                label4.Visible = false;
            }
            else
            {
                codigo += e.KeyChar;
            }
        }

		private void timer1_Tick(object sender, EventArgs e)
		{
            segundos++;

            if (segundos == 4)
            {
                timer1.Enabled = false;
                pictureBox2.Visible = true;
                pictureBox3.Visible = false;
                label2.Text = "";
                label3.Visible = true;
                label4.Visible = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToShortTimeString();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToLongDateString();
        }
    }
}