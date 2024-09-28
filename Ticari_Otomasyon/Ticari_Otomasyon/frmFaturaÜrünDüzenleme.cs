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
    public partial class frmFaturaÜrünDüzenleme : Form
    {
        public frmFaturaÜrünDüzenleme()
        {
            InitializeComponent();
        }
        public string ürünid;
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void frmFaturaÜrünDüzenleme_Load(object sender, EventArgs e)
        {
            txtÜrünID.Text = ürünid;

            SqlCommand komut = new SqlCommand("Select * from TBL_FATURADETAY where FATURAURUNID=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", ürünid);
            SqlDataReader dr= komut.ExecuteReader();    
            while(dr.Read())
            {
                txtFiyat.Text = dr[3].ToString();
                txtMiktar.Text = dr[2].ToString();
                txtTutar.Text = dr[4].ToString();
                txtÜrünAd.Text = dr[1].ToString();

                bgl.baglanti().Close();
            }
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_FATURADETAY set URUNAD=@p1,MIKTAR=@p2,FIYAT=@p3,TUTAR=@p4 where FATURAURUNID=@p5",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtÜrünAd.Text);
            komut.Parameters.AddWithValue("@p2", txtMiktar.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtFiyat.Text));
            //miktar değişiminde fiyatın otomatik değişmesi
            decimal miktar = decimal.Parse(txtMiktar.Text);
            decimal fiyat = decimal.Parse(txtFiyat.Text);
            decimal tutar = miktar * fiyat;
            komut.Parameters.AddWithValue("@p4", tutar);
            komut.Parameters.AddWithValue("@p5", txtÜrünID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Değişiklikler Kaydedildi", "Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete From TBL_FATURADETAY where FATURAURUNID=@p1 ", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", txtÜrünID.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
