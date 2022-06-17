using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace Gallium_v1.Vue.Frame
{
    /// <summary>
    /// Logique d'interaction pour StatFrame.xaml
    /// </summary>
    public partial class StatFrame : Page
    {
        public SeriesCollection series {get;set;}
        public StatFrame()
        {
            InitializeComponent();
            series = new SeriesCollection{
                new LineSeries
                {
                    Values = new ChartValues<double> { 3, 5, 7, 4 }
                },
                new ColumnSeries
                {
                    Values = new ChartValues<decimal> { 5, 6, 2, 7 }
                }
            };
        }

        private void Pie_Click(object sender, RoutedEventArgs e)
        {
            DrawChart.Children.Clear();
            PieChart chart = new PieChart();
            chart.Series = series;
            chart.Height = DrawChart.Height;
            chart.Width = DrawChart.Width;
            DrawChart.Children.Add(chart);
        }
        private void Cartesian_Click(object sender, RoutedEventArgs e)
        {
            DrawChart.Children.Clear();
            CartesianChart chart = new CartesianChart();
            chart.Series = series;
            chart.Height = DrawChart.Height;
            chart.Width = DrawChart.Width;
            DrawChart.Children.Add(chart);
        }
    }
}
