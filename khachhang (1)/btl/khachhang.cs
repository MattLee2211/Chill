using btl.classs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl
{
    public partial class khachhang : Form
    {
        public khachhang()
        {
            InitializeComponent();
        }

        private void khachhang_Load(object sender, EventArgs e)
        {
            classs.function.connect();
            txtmakhach.Enabled = false;
            btnluu.Enabled = false;
            btnboqua.Enabled = false;
            load_datagrid();
        }
        DataTable BTLC;
        private void load_datagrid()
        { string sql;
            sql = "select makh,tenkh,diachi,dienthoai,email,malvhd from khachhang";
            BTLC= classs.function.GetDataTable(sql);
            dgvkhachhang.DataSource=BTLC;
            dgvkhachhang.Columns[0].HeaderText = "mã khách";
            dgvkhachhang.Columns[1].HeaderText = "tên khách";
            dgvkhachhang.Columns[2].HeaderText = "địa chỉ";
            dgvkhachhang.Columns[3].HeaderText = "số điện thoại";
            dgvkhachhang.Columns[4].HeaderText = "email";
            dgvkhachhang.Columns[5].HeaderText = "lĩnh vực hoạt động";
            dgvkhachhang.AllowUserToAddRows = false;
            dgvkhachhang.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        private void dgvkhachhang_Click(object sender, EventArgs e)
        {
            if(btnthem.Enabled==false)
            {
                MessageBox.Show("dang o che do them moi", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (BTLC.Rows.Count == 0 )
            {
                MessageBox.Show("khong co du lieu","thong bao",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            txtmakhach.Text = dgvkhachhang.CurrentRow.Cells["makh"].Value.ToString();
            txttenkhach.Text = dgvkhachhang.CurrentRow.Cells["tenkh"].Value.ToString();
            txtdiachi.Text = dgvkhachhang.CurrentRow.Cells["diachi"].Value.ToString();
            txtsdt.Text = dgvkhachhang.CurrentRow.Cells["dienthoai"].Value.ToString();
            txtemail.Text = dgvkhachhang.CurrentRow.Cells["email"].Value.ToString();
            txtlvhd.Text = dgvkhachhang.CurrentRow.Cells["malvhd"].Value.ToString();
            btnsua.Enabled = true;
            btnxoa.Enabled = true;
            btnboqua.Enabled = true;
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            btnthem.Enabled = false;
            btnsua.Enabled=false;
            btnxoa.Enabled=false;
            btnluu.Enabled = true;
            btnboqua.Enabled=true;
            txtmakhach.Enabled = true;
            txtmakhach.Focus();
            resetvalue();
        }
        private void resetvalue()
        {
            txtmakhach.Text = "";
            txttenkhach.Text = "";
            txtdiachi.Text = "";
            txtemail.Text = "";
            txtsdt.Text = "";
            txtlvhd.Text = "";
        }

        private void btnboqua_Click(object sender, EventArgs e)
        {
            resetvalue();
            btnboqua.Enabled=false;
            btnluu.Enabled=false;
            btnthem.Enabled=true;
            btnxoa.Enabled=true;
            btnsua.Enabled=true;
            txtmakhach.Enabled=false;
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtmakhach.Text=="")
            {
                MessageBox.Show("nhap ma khach hang","thong bao",MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtmakhach.Focus();
                return;
            }
            if (txttenkhach.Text == "")
            {
                MessageBox.Show("nhap ten khach hang", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txttenkhach.Focus();
                return;
            }
            sql = "select makh from khachhang where makh=N'" + txtmakhach.Text.Trim() + "'";
            if (classs.function.checkey(sql)) 
            {
                MessageBox.Show("ma khach da co", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txttenkhach.Focus();
                txtmakhach.Text = "";
                return;
            }
            sql = "INSERT INTO khachhang(makh,tenkh,diachi,dienthoai,email,malvhd) VALUES(N'" + txtmakhach.Text.Trim() + "',N'" + txttenkhach.Text.Trim() + "',N'" + txtdiachi.Text.Trim() + "',N'" + txtsdt.Text.Trim() + "',N'" + txtemail.Text.Trim() + "',N'" + txtlvhd.Text.Trim() + "')";
            function.runsql(sql);
            classs.function.runsql(sql);
            load_datagrid();
            resetvalue();
            btnxoa.Enabled = true;
            btnthem.Enabled = true;
            btnsua.Enabled = true;  
            btnboqua.Enabled = false;
            btnluu.Enabled = false;
            txtmakhach.Enabled = false;
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            string sql;
            if(BTLC.Rows.Count == 0) 
            {
                MessageBox.Show("khong con du lieu","thong bao",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtmakhach.Text=="")
            {
                MessageBox.Show("chua co ban ghi", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(MessageBox.Show("ban co muon xoa khong?","thong bao",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==System.Windows.Forms.DialogResult.Yes)
            {
                sql = "delete khachhang where makh=N'" + txtmakhach.Text + "'";
                classs.function.runsql(sql);
                load_datagrid();
                resetvalue();
            }
        }

        private void btndong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
