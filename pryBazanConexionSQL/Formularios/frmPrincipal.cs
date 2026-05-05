using System;
using System.Windows.Forms;
using pryBazanConexionSQL.Utilidades;

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
            Width = 520;
            Height = 360;

            Label lblDescripcion = new Label
            {
                Text = "Seleccione el modulo que desea administrar.",
                Left = 45,
                Top = 24,
                Width = 410,
                Height = 26
            };

            btnVentas = CrearBoton("Ventas", 64);
            btnProductos = CrearBoton("Productos", 110);
            btnVendedores = CrearBoton("Vendedores", 156);
            btnNuevaVenta = CrearBoton("Nueva venta", 202);

            btnVentas.Click += btnVentas_Click;
            btnProductos.Click += btnProductos_Click;
            btnVendedores.Click += btnVendedores_Click;
            btnNuevaVenta.Click += btnNuevaVenta_Click;

            Controls.Add(lblDescripcion);
            Controls.Add(btnVentas);
            Controls.Add(btnProductos);
            Controls.Add(btnVendedores);
            Controls.Add(btnNuevaVenta);
            EstilosFormulario.AplicarFormulario(this, "Sistema Verduleros");
        }

        private Button CrearBoton(string texto, int top)
        {
            return new Button
            {
                Text = texto,
                Left = 145,
                Top = top,
                Width = 220,
                Height = 34
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
