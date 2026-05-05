using System;
using System.Data;
using System.Windows.Forms;
using pryBazanConexionSQL.Servicios;

namespace pryBazanConexionSQL.Formularios
{
    public class frmNuevaVenta : Form
    {
        private readonly VentaServicio ventaServicio;
        private readonly VendedorServicio vendedorServicio;
        private readonly ProductoServicio productoServicio;
        private ComboBox cmbVendedores;
        private ComboBox cmbProductos;
        private DateTimePicker dtpFecha;
        private NumericUpDown nudKilos;
        private Label lblPrecio;
        private Label lblTotal;
        private Button btnGuardar;

        public frmNuevaVenta()
        {
            ventaServicio = new VentaServicio();
            vendedorServicio = new VendedorServicio();
            productoServicio = new ProductoServicio();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            Text = "Nueva venta";
            StartPosition = FormStartPosition.CenterScreen;
            Width = 440;
            Height = 290;

            Label lblVendedor = new Label { Text = "Vendedor:", Left = 25, Top = 25, Width = 90 };
            cmbVendedores = new ComboBox { Left = 120, Top = 22, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
            Label lblProducto = new Label { Text = "Producto:", Left = 25, Top = 65, Width = 90 };
            cmbProductos = new ComboBox { Left = 120, Top = 62, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
            Label lblFecha = new Label { Text = "Fecha:", Left = 25, Top = 105, Width = 90 };
            dtpFecha = new DateTimePicker { Left = 120, Top = 102, Width = 140, Format = DateTimePickerFormat.Short };
            Label lblKilos = new Label { Text = "Kilos:", Left = 25, Top = 145, Width = 90 };
            nudKilos = new NumericUpDown { Left = 120, Top = 142, Width = 120, DecimalPlaces = 2, Maximum = 100000, Minimum = 1 };
            lblPrecio = new Label { Text = "Precio: 0", Left = 25, Top = 180, Width = 180 };
            lblTotal = new Label { Text = "Total: 0", Left = 220, Top = 180, Width = 180 };
            btnGuardar = new Button { Text = "Guardar", Left = 145, Top = 210, Width = 120, Height = 30 };

            cmbProductos.SelectedIndexChanged += cmbProductos_SelectedIndexChanged;
            nudKilos.ValueChanged += nudKilos_ValueChanged;
            btnGuardar.Click += btnGuardar_Click;

            Controls.AddRange(new Control[] { lblVendedor, cmbVendedores, lblProducto, cmbProductos, lblFecha, dtpFecha,
                lblKilos, nudKilos, lblPrecio, lblTotal, btnGuardar });

            Load += frmNuevaVenta_Load;
        }

        private void frmNuevaVenta_Load(object sender, EventArgs e)
        {
            cmbVendedores.DataSource = vendedorServicio.ObtenerTodos();
            cmbVendedores.DisplayMember = "NombreVendedor";
            cmbVendedores.ValueMember = "IdVendedor";

            cmbProductos.DataSource = productoServicio.ObtenerTodos();
            cmbProductos.DisplayMember = "NomProducto";
            cmbProductos.ValueMember = "IdProducto";
            CalcularTotal();
        }

        private void cmbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void nudKilos_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void CalcularTotal()
        {
            decimal precio = ObtenerPrecioProducto();
            decimal total = precio * nudKilos.Value;
            lblPrecio.Text = "Precio: " + precio.ToString("C2");
            lblTotal.Text = "Total: " + total.ToString("C2");
        }

        private decimal ObtenerPrecioProducto()
        {
            DataRowView fila = cmbProductos.SelectedItem as DataRowView;
            if (fila == null)
            {
                return 0;
            }

            return Convert.ToDecimal(fila["Precio"]);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int idVendedor = Convert.ToInt32(cmbVendedores.SelectedValue);
                int idProducto = Convert.ToInt32(cmbProductos.SelectedValue);
                ventaServicio.Agregar(idVendedor, idProducto, dtpFecha.Value.Date, Convert.ToDouble(nudKilos.Value));
                MessageBox.Show("Venta guardada correctamente.", "Nueva venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar la venta: " + ex.Message, "Nueva venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
