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

namespace Ticari_Otomasyon
{
    public partial class frmAyarlar : Form
    {
        public frmAyarlar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_ADMIN",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void frmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            txtKullanıcıAdı.Text = "";
            txtŞifre.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
           
            
                
                SqlCommand cmd = new SqlCommand("insert into TBL_ADMIN(KullaniciAdi,Sifre) VALUES (@p1,@p2)", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtKullanıcıAdı.Text);
                cmd.Parameters.AddWithValue("@p2", txtŞifre.Text);
                cmd.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yeni Admin Sisteme Kaydedildi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
       
                        
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                txtKullanıcıAdı.Text = dr["KullaniciAdi"].ToString();
                txtŞifre.Text = dr["Sifre"].ToString();
            }
        }

       

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete From TBL_ADMIN where KullaniciAdi=@p1 ", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", txtKullanıcıAdı.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Admin Kaydı Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);
            listele();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            


                SqlCommand komut = new SqlCommand("update TBL_ADMIN set Sifre=@p1 where KullaniciAdi=@p2", bgl.baglanti());
                komut.Parameters.AddWithValue("p2", txtKullanıcıAdı.Text);
                komut.Parameters.AddWithValue("p1", txtŞifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Admin Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();

            
        }

        
    }
}
