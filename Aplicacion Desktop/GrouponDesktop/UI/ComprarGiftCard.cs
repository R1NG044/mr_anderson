﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GrouponDesktop.UI;

namespace GrouponDesktop
{
    public partial class ComprarGiftCard : Form
    {
        LoginWindow login;
        String saldo;
        Int16 montoMinimo;
        Int16 montoMaximo;

        public ComprarGiftCard(LoginWindow login)
        {
            InitializeComponent();
            this.montoMinimo = 10;
            this.montoMaximo = 50;
            this.labelMonto.Text = "El monto\nno puede ser\nmenor a "+this.montoMinimo.ToString()+"\nni mayor a "+this.montoMaximo.ToString()+".";
            this.TxtBoxUsuarioDestino.Enabled = false;
            this.login = login;
            saldo = login.UsuarioActivo.DatosCliente.Saldo.ToString();
            if (String.IsNullOrEmpty(saldo))
                LblSaldo.Text = "0";
            else
                LblSaldo.Text = saldo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User.HomeUsuarios usuarios = new GrouponDesktop.User.HomeUsuarios();
            int montoGiftCard = 0;
            if (Int32.TryParse(TxtBoxMontoGift.Text, out montoGiftCard) == false)
            {
                MessageBox.Show("Monto no valido");
                return;
            }
            if (this.montoMinimo > montoGiftCard | this.montoMaximo < montoGiftCard)
            {
                MessageBox.Show("El monto debe estar entre los valores indicados");
                return;
            }
            Boolean usuarioNoExistente;
            usuarioNoExistente = usuarios.usuarioNoExistente(TxtBoxUsuarioDestino.Text);
            if (usuarioNoExistente || montoGiftCard > Int32.Parse(saldo))
            {
                if(usuarioNoExistente) MessageBox.Show("El usuario no existe");
                else MessageBox.Show("Su saldo no es suficiente");
                return;
            }
            User.DatosCliente clienteDestino = usuarios.getDatosCliente(TxtBoxUsuarioDestino.Text);

            String result = Dominio.DataAdapter.GiftCard.comprarGiftCard(login.UsuarioActivo.DatosCliente.Dni,
                clienteDestino.Dni, Int32.Parse(TxtBoxMontoGift.Text), AdministradorConfiguracion.obtenerFecha());
            usuarios.setInformacionAlUsuario(login.UsuarioActivo);
            MessageBox.Show(result);
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListadoUsuarios w = new ListadoUsuarios("Cliente", "GiftCard", this);
            w.ShowDialog();
            
        }


    }
}
