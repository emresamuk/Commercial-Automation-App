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
    public partial class frmStoklar : Form
    {
        public frmStoklar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();    
        private void frmStoklar_Load(object sender, EventArgs e)
        { 
            SqlDataAdapter da = new SqlDataAdapter("select URUNAD, sum(Adet) as 'Miktar'from TBL_URUNLER group by UrunAd",bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;   


            //charta stok miktarı listeleme:

            SqlCommand komut = new SqlCommand("select URUNAD, sum(Adet) as 'Miktar' from TBL_URUNLER group by UrunAd",bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse (dr[1].ToString()));
            }
            bgl.baglanti().Close();
        }
    }
}
