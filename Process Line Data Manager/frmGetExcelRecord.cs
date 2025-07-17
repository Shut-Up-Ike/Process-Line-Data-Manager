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
    public partial class frmGetExcelRecord: Form
    {
        DataView dvP4DHRU;
        public string SelectedLineId;
        public string SelectedLineDescription;

        public frmGetExcelRecord(DataTable currentDataTable, string SelectedExcelLineID = "")
        {
            InitializeComponent();
            dvP4DHRU = new DataView(currentDataTable);
            SelectedLineId = SelectedExcelLineID;
        }

        private void frmGetExcelRecord_Load(object sender, EventArgs e)
        {
            //Also selects the list item by using SelectedValue instead of SelectedIndex.

            //We need to have the P4D HRU data available to us:
            //dvP4DHRU = new DataView(DataHolder.P4DData);
            dvP4DHRU.Sort = "description";

            lstExcelLines.DataSource = dvP4DHRU;
            lstExcelLines.DisplayMember = "description";
            lstExcelLines.ValueMember = "id";

            DataBindFormControls();

            if(SelectedLineId != "")
            {
                lstExcelLines.SelectedValue = SelectedLineId;
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
                        if (c.Tag.ToString() != "")
                        {
                            c.DataBindings.Add("Text", dvP4DHRU, c.Tag.ToString());
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
            if(lstExcelLines.SelectedIndex > -1)            
            {
                SelectedLineId = lstExcelLines.SelectedValue.ToString();
                SelectedLineDescription = lstExcelLines.Text.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("No line selected!");
        }
    }
}
