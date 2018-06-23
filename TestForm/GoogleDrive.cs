using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestForm.GoogleAPI;

namespace TestForm
{
    public partial class GoogleDrive : Form
    {
        //private string email = "";
        public GoogleDrive()
        {
            InitializeComponent();           
        }

        public static string email = "";

        public static string getEmail()
        {
            return email;
        }

        private TestForm.GoogleAPI.GoogleAPI googleAPI = new TestForm.GoogleAPI.GoogleAPI();

        private void btnOk_Click(object sender, EventArgs e)
        {
            //lưu email xuống file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@".\gd.txt"))
            {
                file.WriteLine(txtEmail.Text);
            }
            this.Close();            
        }        
    }
}
