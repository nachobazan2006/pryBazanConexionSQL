using System;
using System.Data;
using System.Windows.Forms;
using pryBazanConexionSQL.Servicios;
using pryBazanConexionSQL.Utilidades;

namespace pryBazanConexionSQL.Formularios
{
    public class frmProductos : Form
    {
        private readonly ProductoServicio productoServicio;
        private readonly GrupoServicio grupoServicio;
        private const int CantidadMaximaProductos = 200;
        private int idProductoSeleccionado;
        private TextBox txtNombre;
        private TextBox txtBuscar;
        private ComboBox cmbGrupos;
        private NumericUpDown nudPrecio;
        private DataGridView dgvProductos;
        private Label lblLimite;

        public frmProductos()
        {
            productoServicio = new ProductoServicio();
            grupoServicio = new GrupoServicio();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            Text = "Productos";
            StartPosition = FormStartPosition.CenterScreen;
            Width = 820;
            Height = 600;

            Label lblNombre = new Label { Text = "Nombre:", Left = 20, Top = 25, Width = 70 };
            txtNombre = new TextBox { Left = 90, Top = 22, Width = 190 };
            Label lblGrupo = new Label { Text = "Grupo:", Left = 300, Top = 25, Width = 60 };
            cmbGrupos = new ComboBox { Left = 360, Top = 22, Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            Label lblPrecio = new Label { Text = "Precio:", Left = 530, Top = 25, Width = 60 };
            nudPrecio = new NumericUpDown { Left = 590, Top = 22, Width = 100, DecimalPlaces = 2, Maximum = 100000 };

            Button btnAgregar = new Button { Text = "Agregar", Left = 90, Top = 60, Width = 110 };
            Button btnModificar = new Button { Text = "Modificar", Left = 210, Top = 60, Width = 110 };
            Button btnEliminar = new Button { Text = "Eliminar", Left = 330, Top = 60, Width = 110 };
            Button btnLimpiar = new Button { Text = "Limpiar", Left = 450, Top = 60, Width = 110 };
            Label lblBuscar = new Label { Text = "Buscar:", Left = 20, Top = 105, Width = 60 };
            txtBuscar = new TextBox { Left = 90, Top = 102, Width = 220 };
            Button btnBuscar = new Button { Text = "Buscar", Left = 320, Top = 100, Width = 90 };
            lblLimite = new Label { Text = "Se muestran como maximo " + CantidadMaximaProductos + " registros.", Left = 430, Top = 105, Width = 300 };

            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            btnBuscar.Click += btnBuscar_Click;

            dgvProductos = new DataGridView
            {
                Left = 20,
                Top = 140,
                Width = 760,
                Height = 315,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvProductos.CellClick += dgvProductos_CellClick;

            Controls.AddRange(new Control[] { lblNombre, txtNombre, lblGrupo, cmbGrupos, lblPrecio, nudPrecio,
                btnAgregar, btnModificar, btnEliminar, btnLimpiar, lblBuscar, txtBuscar, btnBuscar, lblLimite, dgvProductos });
            EstilosFormulario.AplicarFormulario(this, "Productos");
            EstilosFormulario.AplicarBotonSecundario(btnBuscar);
            EstilosFormulario.AplicarBotonNeutro(btnLimpiar);

            Load += frmProductos_Load;
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            cmbGrupos.DataSource = grupoServicio.ObtenerTodos();
            cmbGrupos.DisplayMember = "NombreGrupo";
            cmbGrupos.ValueMember = "IdGrupo";
            CargarProductos();
        }

        private void CargarProductos()
        {
            dgvProductos.DataSource = productoServicio.ObtenerTodos(CantidadMaximaProductos, txtBuscar.Text);
            dgvProductos.ClearSelection();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Complete el nombre.", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void LimpiarFormulario()
        {
            idProductoSeleccionado = 0;
            txtNombre.Clear();
            nudPrecio.Value = 0;
            dgvProductos.ClearSelection();
            txtNombre.Focus();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            productoServicio.Agregar(txtNombre.Text.Trim(), Convert.ToInt32(cmbGrupos.SelectedValue), nudPrecio.Value);
            CargarProductos();
            LimpiarFormulario();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idProductoSeleccionado == 0 || !ValidarCampos()) return;
            productoServicio.Modificar(idProductoSeleccionado, txtNombre.Text.Trim(), Convert.ToInt32(cmbGrupos.SelectedValue), nudPrecio.Value);
            CargarProductos();
            LimpiarFormulario();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idProductoSeleccionado == 0) return;
            try
            {
                productoServicio.Eliminar(idProductoSeleccionado);
                CargarProductos();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar el producto: " + ex.Message, "Productos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            LimpiarFormulario();
            CargarProductos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];
            idProductoSeleccionado = Convert.ToInt32(fila.Cells["IdProducto"].Value);
            txtNombre.Text = fila.Cells["NomProducto"].Value.ToString();
            cmbGrupos.SelectedValue = Convert.ToInt32(fila.Cells["IdGrupo"].Value);
            nudPrecio.Value = Convert.ToDecimal(fila.Cells["Precio"].Value);
        }
    }
}
