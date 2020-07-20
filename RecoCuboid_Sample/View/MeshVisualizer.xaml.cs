using HelixToolkit.Wpf;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace RecoCuboid_Sample
{
    /// <summary>
    /// Interaction logic for MeshVisualizer.xaml
    /// </summary>
    public partial class MeshVisualizer : Window
    {
        public MeshVisualizer()
        {
            InitializeComponent();

            var reader = new StLReader();
            reader.DefaultMaterial = Materials.Green;
            var model = reader.Read(@"D:\Messe.stl");
            model.Freeze();

            viewport.Children.Add(new ModelVisual3D() { Content = model });
        }
    }
}