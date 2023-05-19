using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace CSharpTextEditor
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

        TextDocument doc = new TextDocument();
        private void new_click(object sender, RoutedEventArgs e)
        {
            if (check_erase())
            {
                doc = new TextDocument();
                text.Text = null;
            }
        }

        private void open_click(object sender, RoutedEventArgs e)
        {
            if (check_erase())
            {
                bool result = (bool)open.ShowDialog();
                if (result)
                {
                    doc.path = open.FileName;
                    doc.open();
                    text.Text = doc.contents;
                }
            }
        }
        public void save_click(object sender, RoutedEventArgs e)
        {
            if (doc.path == null)
            {
                saveas_click(sender, e);
                return;
            }
            doc.contents = text.Text;
            doc.save();
        }

        private void saveas_click(object sender, RoutedEventArgs e)
        {
            bool result = (bool)saveas.ShowDialog();
            if (result)
            {
                doc.path = saveas.FileName;
                doc.contents = text.Text;
                doc.save();
            }
        }

        private void exit_click(object sender, RoutedEventArgs e)
        {
            if (check_erase())
            {
                Application.Current.Shutdown();
            }
        }

        private void about_click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }
        private bool check_erase()
        {
            if (text.Text != doc.contents)
            {
                SaveChanges changes = new SaveChanges();
                changes.ShowDialog();
                bool result = changes.continueaction;
                if (result)
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private void close_click(object sender, CancelEventArgs e)
        {
            if (!check_erase())
            {
                e.Cancel = true;
            }
        }

        public void file_click(object sender, RoutedEventArgs e)
        {
            if (text.Text == doc.contents && !(text.Text == "" && doc.contents == ""))
            {
                save.IsEnabled = false;
            }
            else
            {
                save.IsEnabled = true;
            }
        }

        public OpenFileDialog open = new OpenFileDialog
        {
            Filter = "Text files (*.txt)|*.txt"
        };

        public SaveFileDialog saveas = new SaveFileDialog
        {
            Filter = "Text files (*.txt)|*.txt"
        };

        class TextDocument
        {
            public string path { get; set; }
            public string contents { get; set; } = "";

            public void save()
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(contents);
                }
            }
            public void open()
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.EndOfStream == false)
                    {
                        contents += $"{sr.ReadLine()}\n";
                    }
                }
            }
        }
    }
}
