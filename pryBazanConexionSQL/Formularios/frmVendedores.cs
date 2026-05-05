using System;
using System.Data;
using System.Windows.Forms;
using pryBazanConexionSQL.Servicios;

namespace pryBazanConexionSQL.Formularios
{
    public class frmVendedores : Form
    {
        private readonly VendedorServicio vendedorServicio;
        private const int CantidadMaximaVendedores = 200;
        private int idVendedorSeleccionado;
        private TextBox txtNombre;
        private TextBox txtBuscar;
        private TextBox txtNif;
        private TextBox txtTelefono;
        private TextBox txtPoblacion;
        private TextBox txtDireccion;
        private DataGridView dgvVendedores;
        private Label lblLimite;

        public frmVendedores()
        {
            vendedorServicio = new VendedorServicio();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            Text = "Vendedores";
            StartPosition = FormStartPosition.CenterScreen;
            Width = 920;
            Height = 560;

            Label lblNombre = new Label { Text = "Nombre:", Left = 20, Top = 25, Width = 70 };
            txtNombre = new TextBox { Left = 90, Top = 22, Width = 180 };
            Label lblNif = new Label { Text = "NIF:", Left = 290, Top = 25, Width = 40 };
            txtNif = new TextBox { Left = 330, Top = 22, Width = 120 };
            Label lblTelefono = new Label { Text = "Telefono:", Left = 470, Top = 25, Width = 70 };
            txtTelefono = new TextBox { Left = 540, Top = 22, Width = 130 };
            Label lblPoblacion = new Label { Text = "Poblacion:", Left = 20, Top = 60, Width = 70 };
            txtPoblacion = new TextBox { Left = 90, Top = 57, Width = 180 };
            Label lblDireccion = new Label { Text = "Direccion:", Left = 290, Top = 60, Width = 70 };
            txtDireccion = new TextBox { Left = 360, Top = 57, Width = 310 };

            Button btnAgregar = new Button { Text = "Agregar", Left = 90, Top = 95, Width = 110 };
            Button btnModificar = new Button { Text = "Modificar", Left = 210, Top = 95, Width = 110 };
            Button btnEliminar = new Button { Text = "Eliminar", Left = 330, Top = 95, Width = 110 };
            Button btnLimpiar = new Button { Text = "Limpiar", Left = 450, Top = 95, Width = 110 };
            Label lblBuscar = new Label { Text = "Buscar:", Left = 20, Top = 140, Width = 60 };
            txtBuscar = new TextBox { Left = 90, Top = 137, Width = 220 };
            Button btnBuscar = new Button { Text = "Buscar", Left = 320, Top = 135, Width = 90 };
            lblLimite = new Label { Text = "Se muestran como maximo " + CantidadMaximaVendedores + " registros.", Left = 430, Top = 140, Width = 320 };

            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            btnBuscar.Click += btnBuscar_Click;

            dgvVendedores = new DataGridView
            {
                Left = 20,
                Top = 175,
                Width = 860,
                Height = 325,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvVendedores.CellClick += dgvVendedores_CellClick;

            Controls.AddRange(new Control[] { lblNombre, txtNombre, lblNif, txtNif, lblTelefono, txtTelefono,
                lblPoblacion, txtPoblacion, lblDireccion, txtDireccion, btnAgregar, btnModificar, btnEliminar,
                btnLimpiar, lblBuscar, txtBuscar, btnBuscar, lblLimite, dgvVendedores });

            Load += frmVendedores_Load;
        }

        private void frmVendedores_Load(object sender, EventArgs e)
        {
            CargarVendedores();
        }

        private void CargarVendedores()
        {
            dgvVendedores.DataSource = vendedorServicio.ObtenerTodos(CantidadMaximaVendedores, txtBuscar.Text);
            dgvVendedores.ClearSelection();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Complete el nombre.", "Vendedores", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void LimpiarFormulario()
        {
            idVendedorSeleccionado = 0;
            txtNombre.Clear();
            txtNif.Clear();
            txtTelefono.Clear();
            txtPoblacion.Clear();
            txtDireccion.Clear();
            dgvVendedores.ClearSelection();
            txtNombre.Focus();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            vendedorServicio.Agregar(txtNombre.Text.Trim(), txtNif.Text.Trim(), txtTelefono.Text.Trim(), txtPoblacion.Text.Trim(), txtDireccion.Text.Trim());
            CargarVendedores();
            LimpiarFormulario();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idVendedorSeleccionado == 0 || !ValidarCampos()) return;
            vendedorServicio.Modificar(idVendedorSeleccionado, txtNombre.Text.Trim(), txtNif.Text.Trim(), txtTelefono.Text.Trim(), txtPoblacion.Text.Trim(), txtDireccion.Text.Trim());
            CargarVendedores();
            LimpiarFormulario();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idVendedorSeleccionado == 0) return;
            try
            {
                vendedorServicio.Eliminar(idVendedorSeleccionado);
                CargarVendedores();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar el vendedor: " + ex.Message, "Vendedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            LimpiarFormulario();
            CargarVendedores();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarVendedores();
        }

        private void dgvVendedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow fila = dgvVendedores.Rows[e.RowIndex];
            idVendedorSeleccionado = Convert.ToInt32(fila.Cells["IdVendedor"].Value);
            txtNombre.Text = Convert.ToString(fila.Cells["NombreVendedor"].Value);
            txtNif.Text = Convert.ToString(fila.Cells["NIF"].Value);
            txtTelefono.Text = Convert.ToString(fila.Cells["Telefon"].Value);
            txtPoblacion.Text = Convert.ToString(fila.Cells["Poblacion"].Value);
            txtDireccion.Text = Convert.ToString(fila.Cells["Direccion"].Value);
        }
    }
}
