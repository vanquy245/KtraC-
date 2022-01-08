using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Qlbanxe
{
    public partial class Form1 : Form
    {

        string Status = "";
        int Index = -1;
        int IndexTim = -1;

        DataTable dataTableWrite = new DataTable();
        DataTable dataTableRead = new DataTable();

        DataSet dataSetWrite = new DataSet();
        DataSet dataSetRead = new DataSet();
        public Form1()
        {
            InitializeComponent();
            Lock_Unlock(mySave.KT);
        }


        /*#region Method*/
        DataTable CreateDataTable()
        {
            DataTable dataTable = new DataTable();
            DataColumn colid = new DataColumn("Id");
            DataColumn coltenkh = new DataColumn("TenKH");
            DataColumn colsdt = new DataColumn("SDT");
            DataColumn colidmathang = new DataColumn("Idmathang");
            DataColumn colgiaban = new DataColumn("GiaBan");

            dataTable.Columns.Add(colid);
            dataTable.Columns.Add(coltenkh);
            dataTable.Columns.Add(colsdt);
            dataTable.Columns.Add(colidmathang);
            dataTable.Columns.Add(colgiaban);

            return dataTable;
        }
        void WriteXML()
        {
            dataTableWrite = CreateDataTable();
            foreach(var item in Qlbanxe.Instance.ListSodth)
            {
                dataTableWrite.Rows.Add(item.Sodth, item.Diachi, item.Ten, item.Ghichu);
            }
            dataSetWrite.Tables.Add(dataTableWrite);
            dataSetWrite.WriteXml("data.xml");
        }
        void ReadXML()
        {
            dataSetRead.ReadXml("data.xml");
            dataTableRead = dataSetRead.Tables[0];

            foreach(DataRow item in dataTableRead.Rows)
            {
                Banxe new = new Banxe(item);
                ds.Instance.ListSodth.Add(new);
            }
        }
        void CreateColumnForDataGirdView()
        {
            var colDiachi = new DataGridViewTextBoxColumn();
            var colSodth = new DataGridViewTextBoxColumn();
            var colTen = new DataGridViewTextBoxColumn();
            var colGhichu = new DataGridViewTextBoxColumn();

            colDiachi.HeaderText = "Địa chỉ";
            colSodth.HeaderText = "Số điện thoại";
            colTen.HeaderText = "Tên";
            colGhichu.HeaderText = "Ghi chú";
            
            colTen.DataPropertyName = "Ten";
            colSodth.DataPropertyName = "Sodth";
            colDiachi.DataPropertyName = "Diachi";
            colGhichu.DataPropertyName = "Ghichu";

            colDiachi.Width = 100;
            colSodth.Width = 100;
            colTen.Width = 100;
            colGhichu.Width = 100;

            dtgvbanxe.Columns.AddRange(new DataGridViewColumn[] {colTen, colSodth, colGhichu, colDiachi });
        }
        void Loaddsbanxe()
        {
            dtgvbanxe.DataSource = null;
            CreateColumnForDataGirdView();
            dtgvbanxe.DataSource = dsbanxe.Instance.ListSodth;
            dtgvbanxe.Refresh();
        }
        
        void EnableControls(bool isEnbleTextBox, bool isEnbleDataGridView)
        {
            textBox3.Enabled = textBox2.Enabled = textBox1.Enabled = textBox4.Enabled = isEnbleTextBox;
            dtgvbanxe.Enabled = isEnbleDataGridView;
        }

        void ClearTextBox()
        {
            foreach(var item in this.Controls)
            {
                TextBox item1 = item as TextBox;
                if(item1 != null)
                {
                    item1.Clear();
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            EnableControls(false,true);
            ReadXML();
            Loaddsbanxe();
            button3.Enabled = button2.Enabled = false;
        }
        void Lock_Unlock(Boolean kt)
        {
            outToolStripMenuItem.Enabled= đăngXuấtToolStripMenuItem.Enabled=button1.Enabled=button5.Enabled=button4.Enabled = !kt;
            đăngNhậpToolStripMenuItem.Enabled = kt;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void outToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /*Thêm*/
        private void button1_Click(object sender, EventArgs e)
        {
            EnableControls(true, false);
            button1.Enabled = button5.Enabled = button4.Enabled = false;
            button3.Enabled = button2.Enabled = true;
            Status = "button1";
        }
       /* Sửa*/
        private void button5_Click(object sender, EventArgs e)
        {
            if (Index < 0)
            {
                MessageBox.Show("Hãy chọn một danh bạ", "Cảnh báo");
                return;
            }
            EnableControls(true, false);
            button1.Enabled = button5.Enabled = button4.Enabled = false;
            button3.Enabled = button2.Enabled = true;

            textBox1.Text = dsbanxe.Instance.ListSodth[Index].Diachi;
            textBox2.Text = dsbanxe.Instance.ListSodth[Index].Ghichu;
            textBox3.Text = dsbanxe.Instance.ListSodth[Index].Ten;
            textBox4.Text = dsbanxe.Instance.ListSodth[Index].Sodth;

            Status = "button5";
        }
        /*Lưu*/
        private void button3_Click(object sender, EventArgs e)
        {
            if (!CheckInput())
            {
                return;
            }

            String diachi = textBox1.Text;
            String ghichu = textBox2.Text;
            String ten = textBox3.Text;
            String sodth = textBox4.Text;
            if(Status== "button1")
            {
                dsbanxe.Instance.ListSodth.Add(new banxe(sodth, diachi, ten, ghichu));
            }
            if (Status == "button5")
            {
                dsbanxe.Instance.ListSodth[Index].Sodth = textBox4.Text;
                dsbanxe.Instance.ListSodth[Index].Diachi= textBox1.Text;
                dsbanxe.Instance.ListSodth[Index].Ghichu= textBox2.Text;
                dsbanxe.Instance.ListSodth[Index].Ten= textBox3.Text;
            }
            EnableControls(false, true);
            Loaddsbanxe();
            ClearTextBox();
            button1.Enabled = button5.Enabled = button4.Enabled = true;
            button3.Enabled = button2.Enabled = false;
        }

        private void dtgvbanxe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(Status== "button6")
            {
                IndexTim = e.RowIndex;
                for (int i = 0; i < dsbanxe.Instance.ListSodth.Count; i++)
                {
                    if (dsbanxe.Instance.ListSodth[i].Ten == dtgvbanxe.Rows[IndexTim].Cells[1].Value.ToString())
                        Index = i;
                }
            }
            else
            {
                Index = e.RowIndex;
            }
        }
        /*Huỷ*/
        private void button2_Click(object sender, EventArgs e)
        {
            ClearTextBox();
            EnableControls(false, true);
            button1.Enabled = button5.Enabled = button4.Enabled = true;
            button3.Enabled = button2.Enabled = false;
        }
        /*Xoá*/
        private void button4_Click(object sender, EventArgs e)
        {
            if (Index < 0)
            {
                MessageBox.Show("Hãy chọn một danh bạ", "Cảnh báo");
                return;
            }

            dsbanxe.Instance.ListSodth.RemoveAt(Index);

            Loaddsbanxe();
        }
        bool CheckInput()
        {
            long result;
            if (textBox4.Text == "" || textBox3.Text == "" || textBox2.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Cảnh báo");
                return false;
            }
            if(!(long.TryParse(textBox4.Text, out result)))
            {
                MessageBox.Show("Số điện thoại không hợp lệ", "Cảnh báo");
                return false;
            }
            if(result <= 0)
            {
                MessageBox.Show("Kiểm tra lại thông tin", "Cảnh báo");
                return false;
            }    
            return true;
        }
       /*Tìm kiếm*/
        private void button6_Click(object sender, EventArgs e)
        {
            button1.Enabled = textBox3.Enabled = false;
            textBox5.Enabled = true;
            button2.Enabled = button3.Enabled = textBox3.Enabled = true;
            Status = "button6";
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string tim = textBox5.Text;
            if (tim != "")
            {
                List<banxe> listTim = new List<banxe>();
                foreach (var item in dsbanxe.Instance.ListSodth)
                {
                    if (item.Ten.ToLower().Contains(tim.ToLower()))
                    {
                        listTim.Add(item);
                    }
                }
                dtgvbanxe.DataSource = null;
                CreateColumnForDataGirdView();
                dtgvbanxe.DataSource = listTim;
            }
            else
            {
                dtgvbanxe.DataSource = null;
                Loaddsbanxe();
            }
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {  
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteXML();
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dangnhap d = new Dangnhap();
            d.Show();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Lock_Unlock(mySave.KT);
        }

        private void chiTiếPhầnMềmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Phần mềm quản lý danh bạ điện thoại được thực hiện bởi Hoàng Đình Ánh, Mã sinh viên 18t1021007, Trường đại học khoa học Huế.");
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mySave.KT = !mySave.KT;
            Lock_Unlock(mySave.KT);
        }
    }
}