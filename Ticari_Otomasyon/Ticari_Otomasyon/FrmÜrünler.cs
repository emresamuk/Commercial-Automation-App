using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data. SqlClient;
using DevExpress.XtraGrid;

namespace Ticari_Otomasyon
{
    public partial class FrmÜrünler : Form
    {
        public FrmÜrünler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select *From TBL_URUNLER",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;   
        }


        private void FrmÜrünler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //verileri kaydetme kısmı

            SqlCommand komut = new SqlCommand("insert into TBL_URUNLER(URUNAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
            komut.Parameters.AddWithValue("p1", txtAd.Text);
            komut.Parameters.AddWithValue("p2", txtMarka.Text);
            komut.Parameters.AddWithValue("p3", txtModel.Text);
            komut.Parameters.AddWithValue("p4", MskYıl.Text);         
            komut.Parameters.AddWithValue("p5", int.Parse((NudAdet.Value).ToString()));  
            komut.Parameters.AddWithValue("p6", decimal.Parse(txtAlışFiyatı.Text));
            komut.Parameters.AddWithValue("p7", decimal.Parse(txtSatışFiyatı.Text));
            komut.Parameters.AddWithValue("p8", richtxtDetay.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün sisteme eklendi.", "Bilgi", MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //kayıt silme
            SqlCommand komutsil = new SqlCommand("Delete From TBL_URUNLER where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1",txtID.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            listele();
        }

       

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtID.Text = dr["ID"].ToString();
            txtAd.Text = dr["URUNAD"].ToString();
            txtMarka.Text = dr["MARKA"].ToString();
            txtModel.Text = dr["MODEL"].ToString();
            MskYıl.Text = dr["YIL"].ToString();
            NudAdet.Value = decimal.Parse(dr["ADET"].ToString());
            txtAlışFiyatı.Text = dr["ALISFIYAT"].ToString();
            txtSatışFiyatı.Text = dr["SATISFIYAT"].ToString();
            richtxtDetay.Text = dr["DETAY"].ToString();


        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_URUNLER set URUNAD=@p1,MARKA=@p2,MODEL=@p3,YIL=@p4,ADET=@p5,ALISFIYAT=@p6,SATISFIYAT=@p7,DETAY=@p8 where ID=@p9", bgl.baglanti());
            komut.Parameters.AddWithValue("p1", txtAd.Text);
            komut.Parameters.AddWithValue("p2", txtMarka.Text);
            komut.Parameters.AddWithValue("p3", txtModel.Text);
            komut.Parameters.AddWithValue("p4", MskYıl.Text);
            komut.Parameters.AddWithValue("p5", int.Parse((NudAdet.Value).ToString()));
            komut.Parameters.AddWithValue("p6", decimal.Parse(txtAlışFiyatı.Text));
            komut.Parameters.AddWithValue("p7", decimal.Parse(txtSatışFiyatı.Text));
            komut.Parameters.AddWithValue("p8", richtxtDetay.Text);
            komut.Parameters.AddWithValue("p9", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close() ;
            MessageBox.Show("Ürün Bilgisi Güncellendi","Bilgi", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            listele();

        }
        void temizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
            txtMarka.Text = "";
            txtModel.Text = "";
            MskYıl.Text = "";
            NudAdet.Text = "";
            txtAlışFiyatı.Text = "";
            txtSatışFiyatı.Text = "";
            richtxtDetay.Text = "";

        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
