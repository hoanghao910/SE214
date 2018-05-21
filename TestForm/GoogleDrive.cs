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

        private TestForm.GoogleAPI.GoogleAPI googleAPI = new TestForm.GoogleAPI.GoogleAPI();

        private void btnOk_Click(object sender, EventArgs e)
        {
            //googleAPI.setEmailLogin(txtEmail.Text);
            //googleAPI.loginGoogleDrive(googleAPI.getEmailLogin());
            this.Close();
        }        
    }
}
