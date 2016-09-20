using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;


namespace LargeJsonValidator {

    public partial class Validator : Form {
        public Validator() {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e) {

            lblMessage.Text = @"Processing File ...";
            lblMessage.ForeColor = Color.Black;

            lblMessage.Refresh();

            var path = tbPath.Text;

            if (File.Exists(path) == false) {
                lblMessage.Text = @"Path does not exist";
                lblMessage.ForeColor = Color.Red;
            } else {

                using (var streamReader = new StreamReader(path)) {
                    using (var reader = new JsonTextReader(streamReader)) {
                        try {
                            var schema = JSchema.Load(reader);

                            lblMessage.Text = @"File seems valid.";
                            lblMessage.ForeColor = Color.Green;
                        } catch (JsonReaderException jrEx) {
                            MessageBox.Show(this, jrEx.Message);
                            lblMessage.Text = @"File is not valid.";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                }
            }

        }

        private void btnBrowse_Click(object sender, EventArgs e) {

            lblMessage.Text = "";

            openFileDialog1.Filter = @"JSON Files|*.json";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                var path = openFileDialog1.FileName;
                tbPath.Text = path;
            }
        }
    }

}