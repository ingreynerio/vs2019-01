using Chinok.Data;
using Chinook.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chinook.UI.Desktop02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dgvListado.AutoGenerateColumns = true;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            var da = new ArtistaDA();
            //var model = da.Gets();
            var model = da.GetsWithParam($"{textBox1.Text.Trim()}%");
            dgvListado.DataSource = model;
            dgvListado.Refresh();
        }
    }
}
