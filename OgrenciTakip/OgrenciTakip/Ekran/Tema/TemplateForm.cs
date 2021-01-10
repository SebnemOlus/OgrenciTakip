using FsCheck;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OgrenciTakip.Ekran.Tema
{
    public partial class TemplateForm : Form
    {
       
        public TemplateForm()
        {
            InitializeComponent();
        }

        public bool IsUpdate { get; set; }

    }
}
