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
using System.Windows.Shapes;

namespace bomb_grafic
{
    /// <summary>
    /// Interaction logic for Settings_windowxaml.xaml
    /// </summary>
    public partial class Settings_windowxaml : Window{
        public uint Bombs { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Settings_windowxaml() {
            InitializeComponent();
            TextboxBombs.Text = "10";
            TextboxWidth.Text = "10";
            TextboxHeight.Text = "10";
        }
		private void SaveSettingButton_Click(object sender, RoutedEventArgs e) {
            Bombs = Convert.ToUInt32(TextboxBombs.Text);
            Width = Convert.ToInt32(TextboxWidth.Text);
            Height = Convert.ToInt32(TextboxHeight.Text);

            DialogResult = true;
		}
	}
}
