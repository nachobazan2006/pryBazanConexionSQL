using System;
using System.Windows.Forms;

namespace pryBazanConexionSQL.Formularios
{
    public class frmPrincipal : Form
    {
        private Button btnVentas;
        private Button btnProductos;
        private Button btnVendedores;
        private Button btnNuevaVenta;

        public frmPrincipal()
        {
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            Text = "Sistema Verduleros";
            StartPosition = FormStartPosition.CenterScreen;
            Width = 420;
            Height = 260;

            btnVentas = CrearBoton("Ventas", 30);
            btnProductos = CrearBoton("Productos", 75);
            btnVendedores = CrearBoton("Vendedores", 120);
            btnNuevaVenta = CrearBoton("Nueva venta", 165);

            btnVentas.Click += btnVentas_Click;
            btnProductos.Click += btnProductos_Click;
            btnVendedores.Click += btnVendedores_Click;
            btnNuevaVenta.Click += btnNuevaVenta_Click;

            Controls.Add(btnVentas);
            Controls.Add(btnProductos);
            Controls.Add(btnVendedores);
            Controls.Add(btnNuevaVenta);
        }

        private Button CrearBoton(string texto, int top)
        {
            return new Button
            {
                Text = texto,
                Left = 110,
                Top = top,
                Width = 180,
                Height = 32
            };
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            using (frmVentas formulario = new frmVentas())
            {
                formulario.ShowDialog();
            }
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            using (frmProductos formulario = new frmProductos())
            {
                formulario.ShowDialog();
            }
        }

        private void btnVendedores_Click(object sender, EventArgs e)
        {
            using (frmVendedores formulario = new frmVendedores())
            {
                formulario.ShowDialog();
            }
        }

        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            using (frmNuevaVenta formulario = new frmNuevaVenta())
            {
                formulario.ShowDialog();
            }
        }
    }
}
