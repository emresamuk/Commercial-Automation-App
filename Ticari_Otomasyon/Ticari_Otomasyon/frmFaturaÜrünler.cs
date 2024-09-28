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
    public partial class frmFaturaÜrünler : Form
    {
        public frmFaturaÜrünler()
        {
            InitializeComponent();
        }
        public string id;
        sqlbaglantisi bgl = new sqlbaglantisi();    

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select *from TBL_FATURADETAY where  FATURAID='" + id + "'", bgl.baglanti()); 
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;   

        }

        void hesaplama()
        {                 
            SqlCommand komut = new SqlCommand("select sum(TOPLAM) from TBL_FATURADETAY", bgl.baglanti());
            label2.Text = "Toplam Tutar=" + komut.ExecuteScalar() + "TL";
            
        }
        private void frmFaturaÜrünler_Load(object sender, EventArgs e)
        {
            listele();
            hesaplama();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            frmFaturaÜrünDüzenleme fr = new frmFaturaÜrünDüzenleme();
            DataRow dr=gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null )
            {
                fr.ürünid = dr["FATURAURUNID"].ToString();
            }
            fr.Show();
            
        }
    }
}
