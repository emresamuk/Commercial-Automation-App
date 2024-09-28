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
    public partial class frmPersoneller : Form
    {
        public frmPersoneller()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void personelliste()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(" select *from TBL_PERSONELLER",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void frmPersoneller_Load(object sender, EventArgs e)
        {
            personelliste();
            sehirlistesi();
        }
        void sehirlistesi()
        {
            SqlCommand cmd = new SqlCommand("Select SEHIR from TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboİl.Properties.Items.Add(dr[0]);

            }
            bgl.baglanti().Close();
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Insert into TBL_PERSONELLER(AD,SOYAD,TELEFON,TC,MAIL,IL,ILCE,ADRES,GOREV) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
            komut.Parameters.AddWithValue("p1", txtAd.Text);
            komut.Parameters.AddWithValue("p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("p3", MskTlfn1.Text);
            komut.Parameters.AddWithValue("p4", MskTc.Text);
            komut.Parameters.AddWithValue("p5", txtMail.Text);
            komut.Parameters.AddWithValue("p6", comboİl.Text);
            komut.Parameters.AddWithValue("p7", comboİlçe.Text);
            komut.Parameters.AddWithValue("p8", richtxtAdres.Text);
            komut.Parameters.AddWithValue("p9", txtGörev.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel sisteme eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personelliste();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtID.Text = dr["ID"].ToString();
            txtAd.Text = dr["AD"].ToString();
            txtSoyad.Text = dr["SOYAD"].ToString();
            MskTlfn1.Text = dr["TELEFON"].ToString();
            MskTc.Text = dr["TC"].ToString();
            txtMail.Text = dr["MAIL"].ToString();
            comboİl.Text = dr["IL"].ToString();
            comboİlçe.Text = dr["ILCE"].ToString();
            richtxtAdres.Text = dr["ADRES"].ToString();
            txtGörev.Text = dr["GOREV"].ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete From TBL_PERSONELLER where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", txtID.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Kaydı Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            personelliste();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_PERSONELLER set AD=@p1,SOYAD=@p2,TELEFON=@p3,TC=@p4,MAIL=@p5,IL=@p6,ILCE=@p7, ADRES=@p8, GOREV=@p9 where ID=@p10", bgl.baglanti());
            komut.Parameters.AddWithValue("p1", txtAd.Text);
            komut.Parameters.AddWithValue("p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("p3", MskTlfn1.Text);
            komut.Parameters.AddWithValue("p4", MskTc.Text);
            komut.Parameters.AddWithValue("p5", txtMail.Text);
            komut.Parameters.AddWithValue("p6", comboİl.Text);
            komut.Parameters.AddWithValue("p7", comboİlçe.Text);
            komut.Parameters.AddWithValue("p8", richtxtAdres.Text);
            komut.Parameters.AddWithValue("p9", txtGörev.Text);
            komut.Parameters.AddWithValue("p10", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            personelliste();
        }
        void temizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            MskTlfn1.Text = "";
            txtMail.Text = "";
            comboİl.Text = "";
            comboİlçe.Text = "";
            richtxtAdres.Text = "";
            txtGörev.Text = "";
            MskTc.Text = "";
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void comboİl_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboİlçe.Properties.Items.Clear();
            SqlCommand cmd = new SqlCommand(" Select ILCE from TBL_ILCELER where SEHIR=@p1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", comboİl.SelectedIndex + 1);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboİlçe.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
    }
}
