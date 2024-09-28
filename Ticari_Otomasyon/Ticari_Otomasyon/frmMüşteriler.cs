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
using DevExpress.XtraGrid;
using DevExpress.XtraDashboardLayout;

namespace Ticari_Otomasyon
{
    public partial class frmMüşteriler : Form
    {
        public frmMüşteriler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select *From TBL_MUSTERILER",bgl.baglanti()); 
            da.Fill(dt);
            gridControl1.DataSource = dt;   
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

        private void frmMüşteriler_Load(object sender, EventArgs e)
        {
         
            listele();
            sehirlistesi();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            
            SqlCommand komut = new SqlCommand("Insert into TBL_MUSTERILER(AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bgl.baglanti());
            komut.Parameters.AddWithValue("p1", txtAd.Text);
            komut.Parameters.AddWithValue("p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("p3", MskTlfn1.Text);
            komut.Parameters.AddWithValue("p4", MskTlfn2.Text);
            komut.Parameters.AddWithValue("p5", MskTc.Text);
            komut.Parameters.AddWithValue("p6", txtMail.Text);
            komut.Parameters.AddWithValue("p7", comboİl.Text);
            komut.Parameters.AddWithValue("p8", comboİlçe.Text);
            komut.Parameters.AddWithValue("p9", richtxtAdres.Text);
            komut.Parameters.AddWithValue("p10", txtVergiDairesi.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri sisteme eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void comboİl_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboİlçe.Properties.Items.Clear();
            SqlCommand cmd = new SqlCommand(" Select ILCE from TBL_ILCELER where SEHIR=@p1",bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", comboİl.SelectedIndex + 1 );
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) 
            {
                comboİlçe.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //kayıt silme
            SqlCommand komutsil = new SqlCommand("Delete From TBL_MUSTERILER where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", txtID.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Kaydı Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtID.Text = dr["ID"].ToString();
            txtAd.Text = dr["AD"].ToString();
            txtSoyad.Text = dr["SOYAD"].ToString();
            MskTlfn1.Text = dr["TELEFON"].ToString();
            MskTlfn2.Text = dr["TELEFON2"].ToString();
            MskTc.Text = dr["TC"].ToString();
            txtMail.Text = dr["MAIL"].ToString();
            comboİl.Text = dr["IL"].ToString();
            comboİlçe.Text = dr["ILCE"].ToString();
            richtxtAdres.Text = dr["ADRES"].ToString();
            txtVergiDairesi.Text = dr["VERGIDAIRE"].ToString();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_MUSTERILER set AD=@p1,SOYAD=@p2,TELEFON=@p3,TELEFON2=@p4,TC=@p5,MAIL=@p6,IL=@p7,ILCE=@p8, ADRES=@p9, VERGIDAIRE=@p10 where ID=@p11", bgl.baglanti());
            komut.Parameters.AddWithValue("p1", txtAd.Text);
            komut.Parameters.AddWithValue("p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("p3", MskTlfn1.Text);
            komut.Parameters.AddWithValue("p4", MskTlfn2.Text);
            komut.Parameters.AddWithValue("p5", MskTc.Text);
            komut.Parameters.AddWithValue("p6", txtMail.Text);
            komut.Parameters.AddWithValue("p7", comboİl.Text);
            komut.Parameters.AddWithValue("p8", comboİlçe.Text);
            komut.Parameters.AddWithValue("p9", richtxtAdres.Text);
            komut.Parameters.AddWithValue("p10", txtVergiDairesi.Text);
            komut.Parameters.AddWithValue("p11", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
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
            MskTlfn2.Text = "";
            MskTc.Text = "";
            txtVergiDairesi.Text = "";
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        
    }
}
