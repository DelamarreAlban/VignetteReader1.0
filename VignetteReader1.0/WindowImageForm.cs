using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VignetteReader1._0
{
    public partial class WindowImageForm : Form
    {
        public WindowImageForm()
        {
            InitializeComponent();
        }

        public WindowImageForm(String name, Image image)
        {
            InitializeComponent();

            this.Text = name;
            this.Height = image.Height;
            this.Width = image.Width;
            ImageBox.Image = image;

            this.Show();
        }
    }
}
