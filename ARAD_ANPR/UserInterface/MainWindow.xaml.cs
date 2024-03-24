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

namespace ARAD_ANPR.UserInterface
{
      /// <summary>
      /// Interaction logic for MainWindow.xaml
      /// </summary>
      public partial class MainWindow : Window
      {
            public MainWindow()
            {
                  ViewModels.MainWindowViewModel vm = new ViewModels.MainWindowViewModel();
                  this.DataContext = vm;
                  InitializeComponent();
            }
      }
}
