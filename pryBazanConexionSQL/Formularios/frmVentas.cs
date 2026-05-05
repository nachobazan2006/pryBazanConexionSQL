using System;
using System.Data;
using System.Windows.Forms;
using pryBazanConexionSQL.Servicios;

namespace pryBazanConexionSQL.Formularios
{
    public class frmVentas : Form
    {
        private readonly VentaServicio ventaServicio;
        private readonly VendedorServicio vendedorServicio;
        private readonly ProductoServicio productoServicio;
        private readonly GrupoServicio grupoServicio;
        private const int CantidadMaximaVentas = 500;
        private ComboBox cmbVendedores;
        private ComboBox cmbProductos;
        private ComboBox cmbGrupos;
        private DateTimePicker dtpDesde;
        private DateTimePicker dtpHasta;
        private Button btnBuscar;
        private Button btnLimpiar;
        private DataGridView dgvVentas;
        private Label lblTotalKilos;
        private Label lblTotalVendido;
        private Label lblCantidad;
        private Label lblLimite;

        public frmVentas()
        {
            ventaServicio = new VentaServicio();
            vendedorServicio = new VendedorServicio();
            productoServicio = new ProductoServicio();
            grupoServicio = new GrupoServicio();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            Text = "Ventas";
            StartPosition = FormStartPosition.CenterScreen;
            Width = 1050;
            Height = 620;

            Label lblVendedor = CrearLabel("Vendedor:", 12, 18);
            cmbVendedores = CrearCombo(85, 14, 180);
            Label lblProducto = CrearLabel("Producto:", 285, 18);
            cmbProductos = CrearCombo(355, 14, 180);
            Label lblGrupo = CrearLabel("Grupo:", 555, 18);
            cmbGrupos = CrearCombo(610, 14, 140);

            dtpDesde = CrearFecha(85, 52);
            dtpHasta = CrearFecha(285, 52);
            Label lblDesde = CrearLabel("Desde:", 12, 56);
            Label lblHasta = CrearLabel("Hasta:", 235, 56);

            btnBuscar = new Button { Text = "Buscar", Left = 470, Top = 50, Width = 100, Height = 28 };
            btnLimpiar = new Button { Text = "Limpiar", Left = 580, Top = 50, Width = 100, Height = 28 };
            btnBuscar.Click += btnBuscar_Click;
            btnLimpiar.Click += btnLimpiar_Click;

            dgvVentas = new DataGridView
            {
                Left = 12,
                Top = 95,
                Width = 1005,
                Height = 430,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            lblCantidad = CrearLabel("Cantidad: 0", 12, 540);
            lblTotalKilos = CrearLabel("Total kilos: 0", 180, 540);
            lblTotalVendido = CrearLabel("Total vendido: 0", 380, 540);
            lblTotalVendido.Width = 250;
            lblLimite = CrearLabel("Se muestran como maximo " + CantidadMaximaVentas + " registros. Use filtros para acotar la busqueda.", 640, 540);
            lblLimite.Width = 380;

            Controls.AddRange(new Control[] { lblVendedor, cmbVendedores, lblProducto, cmbProductos, lblGrupo, cmbGrupos,
                lblDesde, dtpDesde, lblHasta, dtpHasta, btnBuscar, btnLimpiar, dgvVentas, lblCantidad, lblTotalKilos, lblTotalVendido, lblLimite });

            Load += frmVentas_Load;
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarVentas();
        }

        private void CargarCombos()
        {
            CargarComboConTodos(cmbVendedores, vendedorServicio.ObtenerTodos(), "NombreVendedor", "IdVendedor");
            CargarComboConTodos(cmbProductos, productoServicio.ObtenerTodos(), "NomProducto", "IdProducto");
            CargarComboConTodos(cmbGrupos, grupoServicio.ObtenerTodos(), "NombreGrupo", "IdGrupo");
        }

        private void CargarVentas()
        {
            int? idVendedor = ObtenerValorCombo(cmbVendedores);
            int? idProducto = ObtenerValorCombo(cmbProductos);
            int? idGrupo = ObtenerValorCombo(cmbGrupos);
            DateTime? desde = dtpDesde.Checked ? dtpDesde.Value.Date : (DateTime?)null;
            DateTime? hasta = dtpHasta.Checked ? dtpHasta.Value.Date : (DateTime?)null;

            DataTable tabla = ventaServicio.ObtenerVentas(idVendedor, idProducto, idGrupo, desde, hasta, CantidadMaximaVentas);
            dgvVentas.DataSource = tabla;
            CalcularTotales(tabla);
        }

        private void CalcularTotales(DataTable tabla)
        {
            decimal totalKilos = 0;
            decimal totalVendido = 0;

            foreach (DataRow fila in tabla.Rows)
            {
                totalKilos += Convert.ToDecimal(fila["Kilos"]);
                totalVendido += Convert.ToDecimal(fila["Total"]);
            }

            lblCantidad.Text = "Cantidad: " + tabla.Rows.Count;
            lblTotalKilos.Text = "Total kilos: " + totalKilos.ToString("N2");
            lblTotalVendido.Text = "Total vendido: " + totalVendido.ToString("C2");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarVentas();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbVendedores.SelectedValue = 0;
            cmbProductos.SelectedValue = 0;
            cmbGrupos.SelectedValue = 0;
            dtpDesde.Checked = false;
            dtpHasta.Checked = false;
            CargarVentas();
        }

        private Label CrearLabel(string texto, int left, int top)
        {
            return new Label { Text = texto, Left = left, Top = top, Width = 100, AutoSize = false };
        }

        private ComboBox CrearCombo(int left, int top, int width)
        {
            return new ComboBox { Left = left, Top = top, Width = width, DropDownStyle = ComboBoxStyle.DropDownList };
        }

        private DateTimePicker CrearFecha(int left, int top)
        {
            return new DateTimePicker { Left = left, Top = top, Width = 140, ShowCheckBox = true, Checked = false, Format = DateTimePickerFormat.Short };
        }

        private void CargarComboConTodos(ComboBox combo, DataTable tabla, string display, string value)
        {
            DataRow fila = tabla.NewRow();
            fila[value] = 0;
            fila[display] = "Todos";
            tabla.Rows.InsertAt(fila, 0);

            combo.DataSource = tabla;
            combo.DisplayMember = display;
            combo.ValueMember = value;
        }

        private int? ObtenerValorCombo(ComboBox combo)
        {
            if (combo.SelectedValue == null || Convert.ToInt32(combo.SelectedValue) == 0)
            {
                return null;
            }

            return Convert.ToInt32(combo.SelectedValue);
        }
    }
}
