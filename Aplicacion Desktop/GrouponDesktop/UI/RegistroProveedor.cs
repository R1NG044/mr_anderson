﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GrouponDesktop.User;

namespace GrouponDesktop
{
    public partial class RegistroProveedor : Form
    {
        RegistroWindow padre;
        HomeUsuarios homeUsr;

        public RegistroProveedor(RegistroWindow padre)
        {
            InitializeComponent();
            this.padre = padre;
            this.homeUsr = new HomeUsuarios();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c.Text.Equals("")) { MessageBox.Show("Tiene que completar todos los campos"); return; }

            }
            try
            {
                this.padre.Usuario.DatosProveedor.Cuit = this.textBox4.Text;
                this.padre.Usuario.DatosProveedor.Mail = this.textBox2.Text;
                this.padre.Usuario.DatosProveedor.NombreContacto = this.textBox12.Text;
                this.padre.Usuario.DatosProveedor.RazonSocial = this.textBox1.Text;
                this.padre.Usuario.DatosProveedor.Rubro = this.textBox13.Text;
                this.padre.Usuario.DatosProveedor.Telefono = this.textBox3.Text;

                this.padre.Usuario.Direccion.Calle = this.textBox6.Text +" "+ this.textBox7.Text;
                this.padre.Usuario.Direccion.Numero = Convert.ToInt32(this.textBox7.Text);
                this.padre.Usuario.DatosProveedor.Ciudad = this.textBox5.Text;
                this.padre.Usuario.Direccion.CodigoPostal = Convert.ToInt32(this.textBox11.Text);
                this.padre.Usuario.Direccion.Piso = Convert.ToInt32(this.textBox8.Text);
                this.padre.Usuario.Direccion.Depto = this.textBox9.Text;
                this.padre.Usuario.Direccion.Localidad = this.textBox10.Text;

                if (!this.textBox8.Text.Equals("-")) this.padre.Usuario.Direccion.Piso = Convert.ToInt32(this.textBox8.Text);
                homeUsr.persistir(this.padre.Usuario);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                return;
            }

            MessageBox.Show("El usuario se ha generado correctamente");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}