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
    public partial class frmNewPipeline : Form
    {
        public string PipeLineName;
        public string PipeLineDescription;
        public string SecondaryLineDescription;
        public int ThermalLineId;
        public int ExcelLineId;
        private readonly DataTable dtHRU;
        private readonly DataTable dtExcel;
        public frmNewPipeline(DataTable currentHRUdatatable, DataTable currentExceldatatable)
        {
            InitializeComponent();
            dtHRU = currentHRUdatatable;
            dtExcel = currentExceldatatable;
            lblExcel_ID.Text = "";
            lblHRU_line_no.Text = "";
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if(txtNewPipeLine.Text == "" || txtMainLineDescription.Text == "")
            {
                MessageBox.Show("Enter valid name and description");
            }
            else
            {
                PipeLineName = txtNewPipeLine.Text;
                PipeLineDescription = txtMainLineDescription.Text;
                SecondaryLineDescription = txtSecondaryLineDescription.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("New Pipeline Cancelled");
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnGetThermal_Click(object sender, EventArgs e)
        {
            if (dtHRU.Rows.Count > 0)
            {
                using (var form = new frmGetThermalRecord(dtHRU))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        int.TryParse(form.SelectedLineId, out ThermalLineId);
                        lblHRU_line_no.Text = ThermalLineId.ToString();
                        lblExcel_ID.Text = null;

                        txtThermalDescription.Text = form.SelectedLineDescription.ToString();
                        txtMainLineDescription.Text = txtThermalDescription.Text;
                    }
                }
            }
            else MessageBox.Show("No Thermal records exist. Did you forget to import them?");
        }

        private void btnGetExcel_Click(object sender, EventArgs e)
        {
            if (dtExcel.Rows.Count > 0)
            {
                using (var form = new frmGetExcelRecord(dtExcel))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        int.TryParse(form.SelectedLineId, out ExcelLineId);
                        lblExcel_ID.Text = ExcelLineId.ToString();
                        lblHRU_line_no.Text = null;

                        txtThermalDescription.Text = form.SelectedLineDescription.ToString();
                        txtMainLineDescription.Text = txtThermalDescription.Text;
                    }
                }
            }
            else MessageBox.Show("No Excel records exist. Did you forget to import them?");
        }
    }
}
