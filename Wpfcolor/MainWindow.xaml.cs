using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpfcolor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string sigPath;

        public ColorRGB mcolor { get; set; }

        public Color clr { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            mcolor = new ColorRGB();
            mcolor.red = 0;
            mcolor.green = 0;
            mcolor.blue = 0;

            inkk.DefaultDrawingAttributes.Width = 1;
            inkk.DefaultDrawingAttributes.Height = 1;
            penSlider.Value = 1;

            this.lbl1.Background = new SolidColorBrush(Color.FromRgb(mcolor.red, mcolor.green, mcolor.blue));
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            inkk.DefaultDrawingAttributes = new System.Windows.Ink.DrawingAttributes();
            inkk.DefaultDrawingAttributes.Width = penSlider.Value;
            inkk.DefaultDrawingAttributes.Height = penSlider.Value;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnDev_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Принадлежит Попову Павлу",
                       "Внимание",
                       MessageBoxButton.OK,
                       MessageBoxImage.Warning);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sigPath = System.IO.Path.GetTempFileName();
                FileStream fs = new FileStream(sigPath, FileMode.Create);
                inkk.Strokes.Save(fs);
                fs.Close();
                MessageBox.Show("Сохранено: " + sigPath.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            this.inkk.Strokes.Clear();
        }

        private void sld_Color_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            string crlName = slider.Name; //Определяем имя контрола, который покрутили
            double value = slider.Value; //Получаем значение контрола
            // В зависимости от выбранного контрола, меняем ту или иную компоненту и переводим её в тип byte
            if (crlName.Equals("sld_RedColor"))
            {
                mcolor.red = Convert.ToByte(value);
            }
            if (crlName.Equals("sld_GreenColor"))
            {
                mcolor.green = Convert.ToByte(value);
            }
            if (crlName.Equals("sld_BlueColor"))
            {
                mcolor.blue = Convert.ToByte(value);
            }

            // Задаём значение переменной класса clr
            clr = Color.FromRgb(mcolor.red, mcolor.green, mcolor.blue);
            // Устанавлеваем фон для контрола Label
            this.lbl1.Background = new SolidColorBrush(Color.FromRgb(mcolor.red, mcolor.green, mcolor.blue));
            // Задаём цвет кисти дял контрола InkCanvas
            this.inkk.DefaultDrawingAttributes.Color = clr;
        }

        private void Redic_Click(object sender, RoutedEventArgs e)
        {
            this.inkk.EditingMode = InkCanvasEditingMode.Select;
        }

        private void Drawi_Click(object sender, RoutedEventArgs e)
        {
            this.inkk.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Cmdd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cmdd.SelectedIndex)
            {
                case 0:
                    clr = Color.FromRgb(255, 0, 0);
                    this.inkk.DefaultDrawingAttributes.Color = clr;
                    break;
                case 1:
                    clr = Color.FromRgb(0, 255, 0);
                    this.inkk.DefaultDrawingAttributes.Color = clr;
                    break;
                case 2:
                    clr = Color.FromRgb(0, 0, 255);
                    this.inkk.DefaultDrawingAttributes.Color = clr;
                    break;
            }
        }
    }
}
