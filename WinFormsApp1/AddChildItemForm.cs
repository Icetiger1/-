using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WinFormsApp1
{
    public partial class AddChildItemForm : Form
    {

        public string ItemName
        {
            get
            {
                return textBoxName.Text;
            }
            set { }
            
        }
        public string ItemType
        {
            get
            {
                if (radioButtonFile.Checked)
                {
                    return "File";
                }
                else if (radioButtonFolder.Checked)
                {
                    return "Folder";
                };
                return null;
            }
            set { }
        }

        public AddChildItemForm()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ItemType))
            {
                MessageBox.Show("Выберите тип создаваемого объекта.");
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(ItemName))
                {
                    if (ItemType == "Folder")
                        ItemName = "New Folder";
                    else
                        ItemName = "New File";
                }

                var regexItem = new Regex("^[a-zA-Z0-9_.!() ]*$");
                if (!regexItem.IsMatch(textBoxName.Text))
                {
                    MessageBox.Show("Введены спец символы");
                }

            }
        }
    }
}
