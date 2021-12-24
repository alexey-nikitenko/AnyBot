using BotStarter;
using BotStarter.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IRunApp _runApp;

        public MainWindow(IRunApp runApp)
        {
            _runApp = runApp;
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadInitialAnglesValues();
        }

        private void LoadInitialAnglesValues()
        {
            var lastAngles = _runApp.GetLastCoordinates();

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
            CoordinatesModel coordinatesModel = new CoordinatesModel();

            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            dictionary.Add("1", (int)FirstMotorSlider.Value);
            dictionary.Add("2", (int)SecondMotorSlider.Value);
            dictionary.Add("3", (int)ClickMotorSlider.Value);
            dictionary.Add("4", (int)RotateMotorSlider.Value);

            coordinatesModel.Name = ButtonName.Text;
            coordinatesModel.Coordinates = dictionary;

            _runApp.SaveCoordinates(coordinatesModel);
        }

        private void Process_Click(object sender, RoutedEventArgs e)
        {
            _runApp.MoveAndSave(1, (int)FirstMotorSlider.Value);
            _runApp.MoveAndSave(2, (int)SecondMotorSlider.Value);
            _runApp.MoveAndSave(3, (int)ClickMotorSlider.Value);
            _runApp.MoveAndSave(4, (int)RotateMotorSlider.Value);

        }

        private void ButtonName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
