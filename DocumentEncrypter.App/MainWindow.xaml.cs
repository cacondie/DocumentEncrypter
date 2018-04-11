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

namespace DocumentEncrypter.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Encrypter _encrypter;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(@"c:\Temp");
            //   _encrypter = new Encrypter(directory);
            _encrypter = new Encrypter(new System.IO.FileInfo("c:\\Temp\\privateKey.xml"), directory);
            //_encrypter.GenerateKeys();
            //_encrypter.Encrypt(new System.IO.FileInfo(@"c:\Temp\Sample.txt"));
            _encrypter.Decrypt(new System.IO.FileInfo(@"c:\Temp\Sample.txt.enc"));
        }
    }
}
