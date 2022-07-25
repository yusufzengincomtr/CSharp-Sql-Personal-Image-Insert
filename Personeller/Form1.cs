using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
namespace Personeller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-VBQBHS1\\Z3NG1N;Initial Catalog=dbpersonel;Integrated Security=True");
        SqlCommand cmd;
        private void load_data()
        {
            cmd = new SqlCommand("select * from tblpersonel order by pid desc", conn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.RowTemplate.Height = 75;
            dataGridView1.DataSource = dt;
            DataGridViewImageColumn img1 = new DataGridViewImageColumn();
            img1 = (DataGridViewImageColumn)dataGridView1.Columns[8];
            img1.ImageLayout = DataGridViewImageCellLayout.Zoom;
            //  this.dataGridView1.Columns[0].Visible = false;
            //allowusertoaddrows gereksiz fazla gelen datayı gizler

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            load_data();

        }

        private void btninsert_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("insert into tblpersonel(ad,soyad,telefon,mail,il,ilce,adres,resim)values(@ad,@soyad,@telefon,@mail,@il,@ilce,@adres,@resim)", conn);
            cmd.Parameters.AddWithValue("ad", txtad.Text);
            cmd.Parameters.AddWithValue("soyad", txtsoyad.Text);
            cmd.Parameters.AddWithValue("telefon", txttel.Text);
            cmd.Parameters.AddWithValue("mail", txtmail.Text);
            cmd.Parameters.AddWithValue("il", txtil.Text);
            cmd.Parameters.AddWithValue("ilce", txtilce.Text);
            cmd.Parameters.AddWithValue("adres", txtadres.Text);
            MemoryStream memstr = new MemoryStream();
            personalpicture.Image.Save(memstr, personalpicture.Image.RawFormat);
            cmd.Parameters.AddWithValue("resim", memstr.ToArray());
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Successfullyy");
            load_data();

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void addimage_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "select image(*.JpG; *.png; *.Gif)|*.JpG; *.png; *.Gif";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                personalpicture.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            lblid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtsoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txttel.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtmail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtil.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtilce.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtadres.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            MemoryStream ms = new MemoryStream((byte[])dataGridView1.CurrentRow.Cells[8].Value);
            personalpicture.Image = Image.FromStream(ms);
        }

        private void btnupdate_Click_1(object sender, EventArgs e)
        {
            cmd = new SqlCommand("update tblpersonel set ad=@ad,soyad=@soyad,telefon=@telefon,mail=@mail,il=@il,ilce=@ilce,adres=@adres,resim=@resim where pid=@pid", conn);
            cmd.Parameters.AddWithValue("ad", txtad.Text);
            cmd.Parameters.AddWithValue("soyad", txtsoyad.Text);
            cmd.Parameters.AddWithValue("telefon", txttel.Text);
            cmd.Parameters.AddWithValue("mail", txtmail.Text);
            cmd.Parameters.AddWithValue("il", txtil.Text);
            cmd.Parameters.AddWithValue("ilce", txtilce.Text);
            cmd.Parameters.AddWithValue("adres", txtadres.Text);
            MemoryStream memstr = new MemoryStream();
            personalpicture.Image.Save(memstr, personalpicture.Image.RawFormat);
            cmd.Parameters.AddWithValue("resim", memstr.ToArray());
            cmd.Parameters.AddWithValue("pid", lblid.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Update Successfullyy");
            load_data();
        }

        private void btndelete_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("SEÇİLİ KAYDI SİLMEK İSTEDİĞİNİZDEN EMİN MİSİNİZ?", "DİKKAT", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cmd = new SqlCommand("delete from tblpersonel where pid=@pid", conn);
                cmd.Parameters.AddWithValue("pid", lblid.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Update Successfullyy");
                load_data();
                personalpicture.Image = null;
                txtad.Text = "";
                txtsoyad.Text = "";
                txttel.Text = "";
                txtmail.Text = "";
                txtil.Text = "";
                txtilce.Text = "";
                txtadres.Text = "";
                lblid.Text = "";
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            //label2.Text = " ";
            //label3.Text = " ";
            //txtara.Clear();
            //conn.Open();
            //SqlCommand komut = new SqlCommand("select * from tblpersonel where pid like'%"+txtara.Text+"%' ", conn);

            //SqlDataReader oku = komut.ExecuteReader();
            //while (oku.Read())
            //{
            //    label2.Text = oku.GetValue(1).ToString();
            // //   label3.Text = oku.GetValue(1).ToString();
            //    //label3.Text = oku["soyad"].ToString();
            //}
            //conn.Close();


            conn.Open();
            SqlCommand komut = new SqlCommand("select * from tblpersonel where pid='" + txtara.Text + "'", conn);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                label2.Text = (oku["ad"]).ToString();
                label3.Text = (oku["soyad"]).ToString();

            }
            conn.Close();
        }


    }
}
