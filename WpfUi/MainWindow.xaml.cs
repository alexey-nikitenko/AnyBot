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

        private void RotateMotorTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            double value = 100;
            RotateMotorSlider.Value = Double.TryParse(RotateMotorTextBox.Text, out value) ? value : value;
        }

        private void RotateMotorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (RotateMotorTextBox != null)
                RotateMotorTextBox.Text = ((int)RotateMotorSlider.Value).ToString();
        }

        private void FirstMotorTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            double value = 100;
            FirstMotorSlider.Value = Double.TryParse(FirstMotorTextBox.Text, out value) ? value : value;
        }

        private void FirstMotorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (FirstMotorTextBox != null)
                FirstMotorTextBox.Text = ((int)FirstMotorSlider.Value).ToString();
        }

        private void SecondMotorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SecondMotorTextBox != null)
                SecondMotorTextBox.Text = ((int)SecondMotorSlider.Value).ToString();
        }

        private void SecondMotorTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            double value = 100;
            SecondMotorSlider.Value = Double.TryParse(SecondMotorTextBox.Text, out value) ? value : value;
        }

        private void ClickMotorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ClickMotorTextBox != null)
                ClickMotorTextBox.Text = ((int)ClickMotorSlider.Value).ToString();
        }

        private void ClickMotorTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            double value = 100;
            ClickMotorSlider.Value = Double.TryParse(ClickMotorTextBox.Text, out value) ? value : value;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Process_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
