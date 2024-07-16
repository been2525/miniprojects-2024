using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace teamproject4
{
    public partial class Graphpage : Window
    {
        public ViewModel ViewModel { get; private set; }

        public Graphpage()
        {
            InitializeComponent();
            ViewModel = new ViewModel();
            DataContext = ViewModel;

            ViewModel.get_data_graph();
        }

        private void TxtLogout_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var res = MessageBox.Show("로그아웃 하시겠습니까?", "로그아웃창", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                this.Hide();
                homepage Homepage = new homepage();
                Homepage.ShowDialog();
            }
        }

        private void BtnGraph_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Graphpage graphpage = new Graphpage();
            graphpage.Topmost = true;
            graphpage.ShowDialog();
        }

        private void BtnManagement_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainwindow = new MainWindow();
            mainwindow.Topmost = true;
            mainwindow.ShowDialog();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("종료하시겠습니까?", "종료창", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes) Environment.Exit(0);
        }

        private void TxtOn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // On 클릭 처리 로직 추가
        }

        private void TxtOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Off 클릭 처리 로직 추가
        }
    }

    public class ViewModel
    {
        public int red { get; private set; }
        public int green { get; private set; }
        public int blue { get; private set; }

        public ISeries[] PieSeries { get; private set; }
        public ISeries[] ColumnSeries { get; private set; }
        public Axis[] XAxes { get; private set; }

        public ViewModel()
        {
            PieSeries = new ISeries[]
            {
                new PieSeries<double> { Values = new double[] { 0 } },
                new PieSeries<double> { Values = new double[] { 0 } },
                new PieSeries<double> { Values = new double[] { 0 } }
            };

            ColumnSeries = new ISeries[]
            {
                new ColumnSeries<double> { Name = "Category 1", Values = new double[] { 0 } },
                new ColumnSeries<double> { Name = "Category 2", Values = new double[] { 0 } },
                new ColumnSeries<double> { Name = "Category 3", Values = new double[] { 0 } }
            };

            XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = new string[] { "Category 1", "Category 2", "Category 3" },
                    LabelsRotation = 0,
                    SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                    TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35))
                }
            };
        }

        public void get_data_graph()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                {
                    conn.Open();

                    using (var Pcount = new SqlCommand(Models.Result.RESULT_SELECT_PLASTIC, conn))
                    {
                        var P_result = Pcount.ExecuteScalar();
                        red = P_result != null ? Convert.ToInt32(P_result) : 0;
                    }

                    using (var Rcount = new SqlCommand(Models.Result.RESULT_SELECT_PAPER, conn))
                    {
                        var R_result = Rcount.ExecuteScalar();
                        green = R_result != null ? Convert.ToInt32(R_result) : 0;
                    }

                    using (var Ccount = new SqlCommand(Models.Result.RESULT_SELECT_CAN, conn))
                    {
                        var C_result = Ccount.ExecuteScalar();
                        blue = C_result != null ? Convert.ToInt32(C_result) : 0;
                    }

                    // PieSeries 업데이트
                    PieSeries = new ISeries[]
                    {
                        new PieSeries<double>
                        {
                            Values = new double[] { red },
                            Fill = new SolidColorPaint(SKColors.Red)
                        },
                        new PieSeries<double>
                        {
                            Values = new double[] { green },
                            Fill = new SolidColorPaint(SKColors.Green)
                        },
                        new PieSeries<double>
                        {
                            Values = new double[] { blue },
                            Fill = new SolidColorPaint(SKColors.Blue)
                        }
                    };

                    // ColumnSeries 업데이트
                    ColumnSeries = new ISeries[]
                    {
                        new ColumnSeries<double>
                        {
                            Name = "Plastic",
                            Values = new double[] { red }
                        },
                        new ColumnSeries<double>
                        {
                            Name = "Paper",
                            Values = new double[] { green }
                        },
                        new ColumnSeries<double>
                        {
                            Name = "Can",
                            Values = new double[] { blue }
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터 조회 중 오류가 발생했습니다: " + ex.Message);
            }
        }
    }
}
