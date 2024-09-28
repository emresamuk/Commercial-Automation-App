using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticari_Otomasyon
{
    public partial class frmHaber : Form
    {
       
        public frmHaber()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        public GridControl GridControl1
        {
            get { return gridControl1; }
        }

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select *From TBL_HABERLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Insert into TBL_HABERLER(BASLIK,TARIH,HABER) values (@p1,@p2,@p3)", bgl.baglanti());
            komut.Parameters.AddWithValue("p1", txtBaşlık.Text);
            komut.Parameters.AddWithValue("p2", mskTarih.Text);
            komut.Parameters.AddWithValue("p3", richHaber.Text);
            
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Haber Bilgisi Sisteme Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //SİLME
            SqlCommand komutsil = new SqlCommand("Delete From TBL_HABERLER where HABERID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", txtID.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Haber Kaydı Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            listele();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_HABERLER set BASLIK=@p1, TARIH=@p2, HABER=@p3 where HABERID=@p4", bgl.baglanti());
            komut.Parameters.AddWithValue("p1", txtBaşlık.Text);
            komut.Parameters.AddWithValue("p2", mskTarih.Text);
            komut.Parameters.AddWithValue("p3", richHaber.Text);
            komut.Parameters.AddWithValue("p4", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Haber Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }
        void temizle()
        {
            txtID.Text = "";
            txtBaşlık.Text = "";
            mskTarih.Text = "";
            richHaber.Text = "";
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridControl1.FocusedView is GridView gridView)
            {
                DataRow dr = gridView.GetDataRow(gridView.FocusedRowHandle);
                // Veri satırıyla yapmak istediğiniz işlemleri burada gerçekleştirin
                if (dr != null)
                {
                    txtID.Text = dr["HABERID"].ToString();
                    richHaber.Text = dr["HABER"].ToString();
                    txtBaşlık.Text = dr["BASLIK"].ToString();
                    mskTarih.Text = dr["TARIH"].ToString();

                }
            }

            
        }

        private void frmHaber_Load(object sender, EventArgs e)
        {
            listele();
        }
    }
}
