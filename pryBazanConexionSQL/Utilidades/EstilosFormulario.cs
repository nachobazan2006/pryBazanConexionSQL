using System.Drawing;
using System.Windows.Forms;

namespace pryBazanConexionSQL.Utilidades
{
    public static class EstilosFormulario
    {
        public static readonly Color Fondo = Color.FromArgb(247, 241, 232);
        public static readonly Color Panel = Color.FromArgb(252, 248, 242);
        public static readonly Color Principal = Color.FromArgb(91, 62, 43);
        public static readonly Color PrincipalOscuro = Color.FromArgb(62, 42, 31);
        public static readonly Color Acento = Color.FromArgb(215, 191, 158);
        public static readonly Color Texto = Color.FromArgb(33, 37, 41);
        public static readonly Color TextoSuave = Color.FromArgb(93, 102, 112);
        public static readonly Color Borde = Color.FromArgb(235, 220, 198);
        public static readonly Color FondoVentas = Color.FromArgb(247, 241, 232);
        public static readonly Color MarronVentas = Color.FromArgb(91, 62, 43);
        public static readonly Color MarronOscuroVentas = Color.FromArgb(62, 42, 31);
        public static readonly Color CremaVentas = Color.FromArgb(235, 220, 198);
        public static readonly Color BeigeVentas = Color.FromArgb(215, 191, 158);

        public static readonly Font FuenteBase = new Font("Century Gothic", 9F, FontStyle.Italic);
        public static readonly Font FuenteTitulo = new Font("Century Gothic", 17F, FontStyle.Italic | FontStyle.Bold);
        public static readonly Font FuenteSubtitulo = new Font("Century Gothic", 9F, FontStyle.Italic);
        public static readonly Font FuenteBoton = new Font("Century Gothic", 9F, FontStyle.Italic | FontStyle.Bold);
        public static readonly Font FuenteGrilla = new Font("Century Gothic", 8.5F, FontStyle.Italic);

        public static void AplicarFormulario(Form formulario, string titulo)
        {
            formulario.Font = FuenteBase;
            formulario.BackColor = Fondo;
            formulario.ForeColor = Texto;
            formulario.FormBorderStyle = FormBorderStyle.FixedSingle;
            formulario.MaximizeBox = false;

            DesplazarControles(formulario, 74);
            formulario.Controls.Add(CrearEncabezado(formulario, titulo));
            AplicarEstiloControles(formulario);
        }

        public static void AplicarFormularioVentas(Form formulario, string titulo)
        {
            formulario.Font = FuenteBase;
            formulario.BackColor = FondoVentas;
            formulario.ForeColor = Texto;
            formulario.FormBorderStyle = FormBorderStyle.FixedSingle;
            formulario.MaximizeBox = false;

            DesplazarControles(formulario, 74);
            formulario.Controls.Add(CrearEncabezadoVentas(formulario, titulo));
            AplicarEstiloControlesVentas(formulario);
        }

        public static void AplicarBotonPrincipal(Button boton)
        {
            AplicarBoton(boton, Principal);
        }

        public static void AplicarBotonSecundario(Button boton)
        {
            AplicarBoton(boton, Color.FromArgb(133, 92, 62));
        }

        public static void AplicarBotonNeutro(Button boton)
        {
            AplicarBoton(boton, Color.FromArgb(126, 108, 91));
        }

        public static void AplicarBotonVentas(Button boton)
        {
            AplicarBoton(boton, MarronVentas);
        }

        public static void AplicarBotonVentasSecundario(Button boton)
        {
            AplicarBoton(boton, Color.FromArgb(133, 92, 62));
        }

        public static void AplicarGrilla(DataGridView grilla)
        {
            grilla.BackgroundColor = Panel;
            grilla.BorderStyle = BorderStyle.None;
            grilla.GridColor = Borde;
            grilla.EnableHeadersVisualStyles = false;
            grilla.ColumnHeadersDefaultCellStyle.BackColor = PrincipalOscuro;
            grilla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grilla.ColumnHeadersDefaultCellStyle.Font = FuenteBoton;
            grilla.ColumnHeadersDefaultCellStyle.SelectionBackColor = PrincipalOscuro;
            grilla.DefaultCellStyle.BackColor = Color.White;
            grilla.DefaultCellStyle.ForeColor = Texto;
            grilla.DefaultCellStyle.Font = FuenteGrilla;
            grilla.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 213, 185);
            grilla.DefaultCellStyle.SelectionForeColor = Texto;
            grilla.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(242, 231, 214);
            grilla.RowHeadersVisible = false;
            grilla.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            grilla.RowTemplate.Height = 28;
        }

        public static void AplicarGrillaVentas(DataGridView grilla)
        {
            grilla.BackgroundColor = Color.FromArgb(252, 248, 242);
            grilla.BorderStyle = BorderStyle.None;
            grilla.GridColor = CremaVentas;
            grilla.EnableHeadersVisualStyles = false;
            grilla.ColumnHeadersDefaultCellStyle.BackColor = MarronOscuroVentas;
            grilla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grilla.ColumnHeadersDefaultCellStyle.Font = FuenteBoton;
            grilla.ColumnHeadersDefaultCellStyle.SelectionBackColor = MarronOscuroVentas;
            grilla.DefaultCellStyle.BackColor = Color.FromArgb(252, 248, 242);
            grilla.DefaultCellStyle.ForeColor = Texto;
            grilla.DefaultCellStyle.Font = FuenteGrilla;
            grilla.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 213, 185);
            grilla.DefaultCellStyle.SelectionForeColor = Texto;
            grilla.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(242, 231, 214);
            grilla.RowHeadersVisible = false;
            grilla.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            grilla.RowTemplate.Height = 28;
        }

        private static Panel CrearEncabezado(Form formulario, string titulo)
        {
            Panel encabezado = new Panel
            {
                Name = "pnlEncabezado",
                Dock = DockStyle.Top,
                Height = 64,
                BackColor = PrincipalOscuro
            };

            Label lblTitulo = new Label
            {
                Text = titulo,
                Left = 22,
                Top = 14,
                Width = formulario.Width - 60,
                Height = 30,
                ForeColor = Color.White,
                Font = FuenteTitulo
            };

            Label lblSubtitulo = new Label
            {
                Text = "Orden y control, dan resultados",
                Left = 24,
                Top = 43,
                Width = formulario.Width - 60,
                Height = 18,
                ForeColor = Color.FromArgb(235, 220, 198),
                Font = FuenteSubtitulo
            };

            Panel linea = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 4,
                BackColor = Acento
            };

            encabezado.Controls.Add(lblTitulo);
            encabezado.Controls.Add(lblSubtitulo);
            encabezado.Controls.Add(linea);
            return encabezado;
        }

        private static Panel CrearEncabezadoVentas(Form formulario, string titulo)
        {
            Panel encabezado = CrearEncabezado(formulario, titulo);
            encabezado.BackColor = MarronOscuroVentas;

            foreach (Control control in encabezado.Controls)
            {
                if (control.Dock == DockStyle.Bottom)
                {
                    control.BackColor = BeigeVentas;
                }
                else if (control is Label etiqueta && etiqueta.Top > 30)
                {
                    etiqueta.ForeColor = CremaVentas;
                }
            }

            return encabezado;
        }

        private static void AplicarEstiloControles(Control contenedor)
        {
            foreach (Control control in contenedor.Controls)
            {
                if (control.Name == "pnlEncabezado")
                {
                    continue;
                }

                control.Font = FuenteBase;
                control.ForeColor = Texto;

                if (control is Label)
                {
                    control.BackColor = Color.Transparent;
                }
                else if (control is TextBox || control is ComboBox || control is NumericUpDown || control is DateTimePicker)
                {
                    control.BackColor = Panel;
                }
                else if (control is Button boton)
                {
                    AplicarBotonPrincipal(boton);
                }
                else if (control is DataGridView grilla)
                {
                    AplicarGrilla(grilla);
                }

                if (control.HasChildren)
                {
                    AplicarEstiloControles(control);
                }
            }
        }

        private static void AplicarEstiloControlesVentas(Control contenedor)
        {
            foreach (Control control in contenedor.Controls)
            {
                if (control.Name == "pnlEncabezado")
                {
                    continue;
                }

                control.Font = FuenteBase;
                control.ForeColor = Texto;

                if (control is Label)
                {
                    control.BackColor = Color.Transparent;
                }
                else if (control is TextBox || control is ComboBox || control is NumericUpDown || control is DateTimePicker)
                {
                    control.BackColor = Color.FromArgb(252, 248, 242);
                }
                else if (control is Button boton)
                {
                    AplicarBotonVentas(boton);
                }
                else if (control is DataGridView grilla)
                {
                    AplicarGrillaVentas(grilla);
                }

                if (control.HasChildren)
                {
                    AplicarEstiloControlesVentas(control);
                }
            }
        }

        private static void AplicarBoton(Button boton, Color color)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.BackColor = color;
            boton.ForeColor = Color.White;
            boton.Font = FuenteBoton;
            boton.Cursor = Cursors.Hand;
        }

        private static void DesplazarControles(Control contenedor, int desplazamiento)
        {
            foreach (Control control in contenedor.Controls)
            {
                control.Top += desplazamiento;
            }
        }
    }
}
