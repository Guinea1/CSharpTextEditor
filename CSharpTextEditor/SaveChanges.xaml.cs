using CSharpTextEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace CSharpTextEditor
{
    /// <summary>
    /// Interaction logic for SaveChanges.xaml
    /// </summary>
    public partial class SaveChanges : Window
    {
        public SaveChanges()
        {
            InitializeComponent();
        }

        public bool continueaction = true;
        private bool appbutton = false;

        private void save_click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).save_click(sender, e);
            appbutton = true;
            Close();
        }

        private void dontsave_click(object sender, RoutedEventArgs e)
        {
            appbutton = true;
            Close();
        }

        private void cancel_click(object sender, RoutedEventArgs e)
        {
            appbutton = true;
            continueaction = false;
            Close();
        }

        private void close_click(object sender, CancelEventArgs e)
        {
            if (!appbutton)
            {
                continueaction = false;
            }
        }
    }
}
