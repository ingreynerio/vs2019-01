using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using App.Entities;

namespace App.UI.Desktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text=
            ConfigurationManager.AppSettings["IGV"].ToString();
        }

        private void BtnCalcular_Click(object sender, EventArgs e)
        {
            Factura documento = new Factura();
            documento.Total = Convert.ToDecimal(txtTotal.Text);

            documento.onDespuesCalcular += new Entities.Events.Listeners.DespuesCalcular(MostrarDatos);
            // Se llama al evento y el evento llama a la función
            documento.Calcular();

           
        }

        //Metodo mostrar
        private void MostrarDatos(object obj)
        {
            var documento = (Factura)obj;
            lblTotal.Text = documento.Total.ToString();
            //lblIgv.Text = documento.IGV.ToString();
            lblIgv.Text = ((Factura)documento).IGV.ToString();
            //lblSubTotal.Text = documento.SubTotal.ToString();
            lblSubTotal.Text = ((Factura)documento).SubTotal.ToString();
        }
    }
}
