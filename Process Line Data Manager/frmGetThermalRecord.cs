using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Process_Line_Data_Manager
{
    public partial class frmGetThermalRecord : Form
    {
        DataView dvP4DHRU;
        public string SelectedLineId;
        public string SelectedLineDescription;

        public frmGetThermalRecord(DataTable currentDataTable, string SelectedThermalLineID = "")
        {
            InitializeComponent();
            dvP4DHRU = new DataView(currentDataTable);
            SelectedLineId = SelectedThermalLineID;
        }

        private void frmGetHRURecord_Load(object sender, EventArgs e)
        {
            //We need to have the P4D HRU data available to us:
            dvP4DHRU.Sort = "description";

            lstHRULines.DataSource = dvP4DHRU;
            lstHRULines.DisplayMember = "description";
            //line_no has been replaced with ID. 
            lstHRULines.ValueMember = "ID";

            //set minimum size
            this.MinimumSize = new Size(695, 521);

            DataBindFormControls();

            if(SelectedLineId != "")
            {
                lstHRULines.SelectedValue = SelectedLineId;
            }
        }

        private void DataBindFormControls()
        {
            foreach(Control c in this.gbHRULines.Controls)
            {
                if((c.GetType() == typeof(Label)) && (!(c.Tag == null)))
                {
                    try
                    {
                        c.DataBindings.Clear();

                        if (c.Tag.ToString() != "" && dvP4DHRU.Table.Columns.Contains(c.Tag.ToString()))
                        {
                            c.DataBindings.Add("Text", dvP4DHRU, c.Tag.ToString());
                        }
                        else if(c.Tag.ToString() != "")
                        {
                            c.Text = "Disabled";
                            c.Enabled = false;
                        }
                    }
                    catch
                    {
                        //do nothing. Couldn't databind the field for some reason.
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(lstHRULines.SelectedIndex > -1)            
            {
                SelectedLineId = lstHRULines.SelectedValue.ToString();
                SelectedLineDescription = lstHRULines.Text.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("No line selected!");
        }
    }
}
