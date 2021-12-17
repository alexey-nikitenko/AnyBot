using BotStarter;
using System;
using System.Windows;

namespace WpfUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadInitialAnglesValues();
        }

        private void LoadInitialAnglesValues()
        {
            Configuration configuration = new Configuration();

            var lastAngles = configuration.GetLastAngles();

            FirstMotorTextBox.Text = lastAngles["1"].ToString();
            FirstMotorSlider.Value = lastAngles["1"];
            
            SecondMotorTextBox.Text = lastAngles["2"].ToString();
            SecondMotorSlider.Value = lastAngles["2"];

            RotateMotorTextBox.Text = lastAngles["3"].ToString();
            RotateMotorSlider.Value = lastAngles["3"];

            ClickMotorTextBox.Text = lastAngles["4"].ToString();
            ClickMotorSlider.Value = lastAngles["4"];

        }

        private void LeftTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void LeftSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void UpTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void FirstMotorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void SecondMotorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void DownTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void RightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void RightTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
