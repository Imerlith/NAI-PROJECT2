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

namespace NAI_PROJECT2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IEnumerable<Button> Buttons;
        private Network network;
        public MainWindow()
        {
            InitializeComponent();
            //Znajduje wszystkie przyciski z MainWindow.xaml oprocz Zatwierdz
            Buttons = FindVisualChildren<Button>(this);
            //Tworzenie danych do nauki
           
            var training0 = new Training
            {
                Input = new List<double>(new double[] { 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0 }),
                Expected = new List<double>(new double[] { 1, 0, 0, 0,0, 0, 0, 0, 0, 0 })
            };
            var training1 = new Training
            {
                Input = new List<double>(new double[] { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, }),
                Expected = new List<double>(new double[] { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 })
            };
            var training2 = new Training
            {
                Input = new List<double>(new double[] { 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1 }),
                Expected = new List<double>(new double[] { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 })
            };
            var training3 = new Training
            {
                Input = new List<double>(new double[] { 1, 1, 1, 0, 0, 0, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0 }),
                Expected = new List<double>(new double[] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 })
            };
            var training4 = new Training
            {
                Input = new List<double>(new double[] { 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1, 0, }),
                Expected = new List<double>(new double[] { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 })
            };
            var training5 = new Training
            {
                Input = new List<double>(new double[] { 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 0 }),
                Expected = new List<double>(new double[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 })
            };
            var training6 = new Training
            {
                Input = new List<double>(new double[] { 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0 }),
                Expected = new List<double>(new double[] { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 })
            };
            var training7 = new Training
            {
                Input = new List<double>(new double[] { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, }),
                Expected = new List<double>(new double[] { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 })
            };
            var training8 = new Training
            {
                Input = new List<double>(new double[] { 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1 }),
                Expected = new List<double>(new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 })
            };
            var training9 = new Training
            {
                Input = new List<double>(new double[] { 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1 }),
                Expected = new List<double>(new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 })
            };

            var trainingSet = new List<Training>
            {
                training0,
                training1,
                training2,
                training3,
                training4,
                training5,
                training6,
                training7,
                training8,
                training9
            };
            //Tworzymy nową sieć z 3 Neuronami listą  z danymi treningowymi oraz zmienną uczącą Alpha o wartości 0.5 
            network = new Network(3, trainingSet, 0.5);
            //uczymy sieć na podstawie danych treningowych
            network.StartLearning();
        }
        //Zmienianie koloru przycisków które zaznaczamy gdy rysujemy liczbę
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.Background == Brushes.Gray)
            {
                button.Background = Brushes.Red;
            }
            else
            {
                button.Background = Brushes.Gray;
            }

        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            //Blokujemy przyciski na czas obliczania
            Block();
            var input = new List<double>();
            //Tworzymy wektor wejść jeśli przycisk byl czerwony to daje nam 1 0 wpp
            foreach (Button b in Buttons)
            {
                if (b.Background == Brushes.Red)
                {
                    input.Add(1);
                }
                else
                {
                    input.Add(0);
                }
            }
            input.ForEach(Console.WriteLine);
            //Inicjujemy zapytanie do sieci i ustalamy odpowiedz do Label pod przyciskiem Zatwierdź
            ResultLbl.Content = network.Test(input);
            //Aktywujemy przyciski i ustawiamy je na szaro 
            Reset();
        }
        private void Block()
        {

            foreach (Button button in Buttons)
            {
                button.IsEnabled = false;

            }
        }
        private void Reset()
        {
            foreach (Button button in Buttons)
            {
                button.IsEnabled = true;
                button.Background = Brushes.Gray;
            }
        }
        //Metoda pomocnicza znajdująca elementy z xaml danego typu 
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        var ch = child as Button;
                        if (ch.Name != "SubmitBtn")
                            yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
