using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Presentacion_IU
{
    public partial class FormReportes : Form
    {
        public FormReportes()
        {
            InitializeComponent();
            oBLLCliente = new BLLCliente();
            oBLLVenta = new BLLVenta();
            oBLLEmpleado = new BLLEmpleado();
        }

        BLLVenta oBLLVenta;
        BLLCliente oBLLCliente;
        BLLEmpleado oBLLEmpleado;

        private void FormReportes_Load(object sender, EventArgs e)
        {
            cargarChartCompras();
            this.reportViewer1.RefreshReport();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            cargarChartCompras();
        }


        void cargarChartCompras()
        {
            // un grafico que muestra un ranking de compras de cada cliente
            Dictionary<string, int> listaChart = new Dictionary<string, int>();
            foreach (BECliente objCliente in oBLLCliente.ListarTodo())
            {
                // se obtienen las compras de cada cliente
                int TotalCompras = oBLLVenta.ListarTodo().Count(x => x.Cliente.Codigo == objCliente.Codigo);
                listaChart.Add(objCliente.ToString(), TotalCompras);
            }
            chart2.Titles.Clear();
            chart2.Series.Clear();
            chart2.ChartAreas.Clear();
            chart2.Titles.Add(new Title("Ranking de compras"));
            chart2.ChartAreas.Add(new ChartArea());

            Series serie1 = new Series("Compras");
            serie1.ChartType = SeriesChartType.Column;
            serie1.Points.DataBindXY(listaChart.Keys, listaChart.Values);
            chart2.Series.Add(serie1);

        }


        void cargarChartVentas()
        {
            // ranking de ventas de cada empleado
            Dictionary<string, int> listaChart = new Dictionary<string, int>();
            foreach (BEEmpleado oBEEmpleado in oBLLEmpleado.ListarTodo())
            {
                // Se toman la cantidad de ventas realizadas por el empleado
                int totalVentas = oBLLVenta.ListarTodo().Count(x => x.Empleado.Codigo == oBEEmpleado.Codigo);
                listaChart.Add(oBEEmpleado.ToString(), totalVentas);
            }
            chart1.Titles.Clear();
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Titles.Add(new Title("Ranking de ventas"));
            chart1.ChartAreas.Add(new ChartArea());

            Series serie1 = new Series("Ventas");
            serie1.ChartType = SeriesChartType.Column;
            serie1.Points.DataBindXY(listaChart.Keys, listaChart.Values);
            chart1.Series.Add(serie1);
        }
    }
}
