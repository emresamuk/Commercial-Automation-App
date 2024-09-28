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
    public partial class frmFaturalar : Form
    {
        public frmFaturalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();    

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_FATURABILGI",bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void frmFaturalar_Load(object sender, EventArgs e)
        {
            listele();

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtFaturaID.Text == "")
            {

                double matrah, kdv, toplam;
                kdv = Convert.ToDouble(txtKdv.Text);
                matrah = Convert.ToDouble(txtMatrah.Text);
                toplam = matrah + kdv;
                txtToplam.Text = toplam.ToString();

                SqlCommand cmd = new SqlCommand("insert into TBL_FATURABILGI(FATURANO,TARIH,VERGINO,VERGIDAIRE,KURULUS,MATRAH,KDV,TOPLAM) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)",bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtFaturaNo.Text);
                cmd.Parameters.AddWithValue("@p2", mskTarih.Text);
                cmd.Parameters.AddWithValue("@p3", txtVergiNo.Text);
                cmd.Parameters.AddWithValue("@p4", txtVergiDairesi.Text);
                cmd.Parameters.AddWithValue("@p5", txtKuruluş.Text);
                cmd.Parameters.AddWithValue("@p6", decimal.Parse(txtMatrah.Text));
                cmd.Parameters.AddWithValue("@p7", decimal.Parse(txtKdv.Text));
                cmd.Parameters.AddWithValue("@p8", decimal.Parse(txtToplam.Text));               
                cmd.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Bilgisi Sisteme Kaydedildi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                listele();
            }
            if (txtFaturaID.Text != "")
            {
                double miktar, matrah, fiyat,toplam,kdv;
                fiyat = Convert.ToDouble(txtFiyat.Text);
                miktar = Convert.ToDouble(txtMiktar.Text);
                kdv = Convert.ToDouble(txtKdv2.Text);
                matrah = miktar * fiyat;
                txtMatrah2.Text = matrah.ToString();

                toplam = matrah + kdv;
                txtToplam2.Text = toplam.ToString();


                SqlCommand komut2 = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,FATURAID,MATRAH,KDV,TOPLAM) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", txtÜrünAd.Text);
                komut2.Parameters.AddWithValue("@p2", txtMiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(txtFiyat.Text));
                komut2.Parameters.AddWithValue("@p4", txtFaturaID.Text);
                komut2.Parameters.AddWithValue("@p5", decimal.Parse(txtMatrah2.Text));
                komut2.Parameters.AddWithValue("@p6", decimal.Parse(txtKdv2.Text));
                komut2.Parameters.AddWithValue("@p7", decimal.Parse(txtToplam2.Text));
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Faturaya ait ürün kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();

            }
        }
             

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr!=null)
            {
                txtID.Text = dr["FATURABILGIID"].ToString();
                txtFaturaNo.Text = dr["FATURANO"].ToString();
                mskTarih.Text = dr["TARIH"].ToString();
                txtVergiNo.Text = dr["VERGINO"].ToString();
                txtVergiDairesi.Text = dr["VERGIDAIRE"].ToString();
                txtKuruluş.Text = dr["KURULUS"].ToString();
                txtMatrah.Text = dr["MATRAH"].ToString();
                txtKdv.Text = dr["KDV"].ToString();
                txtToplam.Text = dr["TOPLAM"].ToString();
            }

            
        }

        private void btnSil_Click_1(object sender, EventArgs e)
        {
            // İlgili kayıtları silmek için önce referans kısıtlamalarını devre dışı bırakalım
            SqlCommand disableConstraintCommand = new SqlCommand("ALTER TABLE TBL_FATURADETAY NOCHECK CONSTRAINT FK_TBL_FATURADETAY_TBL_FATURABILGI", bgl.baglanti());
            disableConstraintCommand.ExecuteNonQuery();

            // TBL_FATURADETAY tablosundaki ilgili kayıtları sil
            SqlCommand komutsilDetay = new SqlCommand("DELETE FROM TBL_FATURADETAY WHERE FATURAID=@p1", bgl.baglanti());
            komutsilDetay.Parameters.AddWithValue("@p1", txtID.Text);
            komutsilDetay.ExecuteNonQuery();

            // Referans kısıtlamalarını tekrar etkinleştir
            SqlCommand enableConstraintCommand = new SqlCommand("ALTER TABLE TBL_FATURADETAY CHECK CONSTRAINT FK_TBL_FATURADETAY_TBL_FATURABILGI", bgl.baglanti());
            enableConstraintCommand.ExecuteNonQuery();

            SqlCommand komutsil = new SqlCommand("Delete From TBL_FATURABILGI where FATURABILGIID=@p1 ", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", txtID.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Kaydı Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);
            listele();
            
        }
        void temizle()
        {
            txtID.Text = "";
            txtFaturaNo.Text = "";
            mskTarih.Text = "";
            txtVergiNo.Text = "";
            txtVergiDairesi.Text = "";
            txtKuruluş.Text = "";
            txtMatrah.Text = "";
            txtKdv.Text = "";
            txtToplam.Text = "";

        }
        private void btnTemizle_Click_1(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnGüncelle_Click_1(object sender, EventArgs e)
        {
            if(txtFaturaID.Text == "")
            {
                SqlCommand komut = new SqlCommand("update TBL_FATURABILGI set FATURANO=@p1,TARIH=@p2,VERGINO=@p3,VERGIDAIRE=@p4,KURULUS=@p5,MATRAH=@p6,KDV=@p7,TOPLAM=@p8 where FATURABILGIID=@p9", bgl.baglanti());
                komut.Parameters.AddWithValue("p1", txtFaturaNo.Text);
                komut.Parameters.AddWithValue("p2", mskTarih.Text);
                komut.Parameters.AddWithValue("p3", txtVergiNo.Text);
                komut.Parameters.AddWithValue("p4", txtVergiDairesi.Text);
                komut.Parameters.AddWithValue("p5", txtKuruluş.Text);
                komut.Parameters.AddWithValue("p6", txtMatrah2.Text);
                komut.Parameters.AddWithValue("p7", txtKdv2.Text);
                komut.Parameters.AddWithValue("p8", txtToplam2.Text);
                komut.Parameters.AddWithValue("p9", txtID.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
            }
            
        }
        void temizle2()
        {
            txtÜrünAd.Text = "";
            txtMiktar.Text = "";
            txtFiyat.Text = "";
            //txtTutar.Text = "";
            txtFaturaID.Text = "";
            txtMatrah2.Text = "";
            txtKdv2.Text = "";
            txtToplam2.Text = "";

        }
        private void btnTemizle2_Click(object sender, EventArgs e)
        {
            temizle2();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            frmFaturaÜrünler fr = new frmFaturaÜrünler();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if(dr != null ) 
            {
                fr.id = dr["FATURABILGIID"].ToString();
            }
            fr.Show();
        }
    }
}
