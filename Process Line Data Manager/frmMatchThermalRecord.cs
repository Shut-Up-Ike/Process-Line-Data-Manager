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
    public partial class frmMatchThermalRecord : Form
    {
        DataView dvP4D;
        DataView dvHRU;
        public DataSet dsThermal;
        DataTable dtUnmatchedHRU;
        DataTable dtUnmatchedP4D;

        public frmMatchThermalRecord(DataSet dsThermalData)
        {
            InitializeComponent();
            dsThermal = dsThermalData;
            //dtMap = dsThermal.Tables[Thermal.THERMALTABLENAMEMAP];
        }

        private void frmMatchThermalRecord_Load(object sender, EventArgs e)
        {
            FindUnmatchedRecords();
            dvP4D = new DataView(dtUnmatchedP4D);
            dvHRU = new DataView(dtUnmatchedHRU);

            dvP4D.Sort = "description";
            dvHRU.Sort = "description";

            lstHRULines.DataSource = dvHRU;
            lstHRULines.DisplayMember = "description";
            lstHRULines.ValueMember = "line_no";

            lstP4DLines.DataSource = dvP4D;
            lstP4DLines.DisplayMember = "description";
            lstP4DLines.ValueMember = "id";

            //set minimum size
            this.MinimumSize = new Size(900, 800);

            DataBindFormControls();
        }

        private void FindUnmatchedRecords()
        {
            dtUnmatchedHRU = (from a in dsThermal.Tables[Thermal.THERMALTABLENAMEHRU].AsEnumerable()
                                   join b in dsThermal.Tables[Thermal.THERMALTABLENAMEP4D].AsEnumerable()
                                   on a["line_no"].ToString() equals b["line_no"].ToString()
                                   into g
                                   where g.Count() == 0
                                   select a).CopyToDataTable();
            dtUnmatchedP4D = (from a in dsThermal.Tables[Thermal.THERMALTABLENAMEP4D].AsEnumerable()
                              join b in dsThermal.Tables[Thermal.THERMALTABLENAMEHRU].AsEnumerable()
                              on a["line_no"].ToString() equals b["line_no"].ToString()
                              into g
                              where g.Count() == 0
                              select a).CopyToDataTable();
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

                        if (c.Tag.ToString() != "" && dvHRU.Table.Columns.Contains(c.Tag.ToString()))
                        {
                            c.DataBindings.Add("Text", dvHRU, c.Tag.ToString());
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
            foreach (Control c in this.gbP4DLines.Controls)
            {
                if ((c.GetType() == typeof(Label)) && (!(c.Tag == null)))
                {
                    try
                    {
                        c.DataBindings.Clear();

                        if (c.Tag.ToString() != "" && dvP4D.Table.Columns.Contains(c.Tag.ToString()))
                        {
                            c.DataBindings.Add("Text", dvP4D, c.Tag.ToString());
                        }
                        else if (c.Tag.ToString() != "")
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
            if (lstP4DLines.Items.Count > 0)
            {
                DialogResult tempResult;
                tempResult = MessageBox.Show($"There are still {lstP4DLines.Items.Count} lines that need matched! Leaving them unmatched will mark them as DELETED. Continue?", "Unmatched lines!", MessageBoxButtons.YesNo);
                if (tempResult == DialogResult.Yes)
                {
                    //They want to mark the lines as DELETED:
                    this.DialogResult = DialogResult.Ignore;
                    this.Close();
                }
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCreateLink_Click(object sender, EventArgs e)
        {
            try
            {
                //Add a new row to the map table with the selected values:
                //DataRow newrow = dtMap.Rows.Add();
                //newrow["HRU_line_no"] = lstHRULines.SelectedValue;
                //newrow["P4D_line_no"] = lstP4DLines.SelectedValue;

                //Set line_no in P4D table to be the same as HRU value:
                DataRow row = dsThermal.Tables[Thermal.THERMALTABLENAMEP4D].AsEnumerable()
                    .SingleOrDefault(r => r.Field<int>("ID") == (int)lstP4DLines.SelectedValue);
                row["line_no"] = (Byte)lstHRULines.SelectedValue;

                //Delete the selected HRU record from the "unmatched" group:
                DataRow[] hrurow = dtUnmatchedHRU.Select("line_no=" + lstHRULines.SelectedValue.ToString());
                if (hrurow.Count() == 1)
                {
                    hrurow[0].Delete();
                    dtUnmatchedHRU.AcceptChanges();
                }

                //Delete the selected P4D record from the "unmatched" group:
                DataRow[] p4drow = dtUnmatchedP4D.Select("id=" + lstP4DLines.SelectedValue.ToString());
                if(p4drow.Count() == 1)
                {
                    p4drow[0].Delete();
                    dtUnmatchedP4D.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Problem creating link: {ex.Message}");
            }


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Changes made in this form are NOT being saved!");
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
