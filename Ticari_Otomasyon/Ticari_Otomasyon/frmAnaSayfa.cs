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
using System.Xml;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout.Tab;
using DevExpress.XtraTab;

namespace Ticari_Otomasyon
{
    public partial class frmAnaSayfa : Form
    {
        

        public frmAnaSayfa(DataTable veriKaynagi)
        {
            InitializeComponent();         
        }

        private frmHaber frmHaberForm;

        public frmAnaSayfa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        public frmHaber FrmHaberForm
        {
            get { return frmHaberForm; }
            set { frmHaberForm = value; }
        }

        public XtraTabPage SelectedTabPage { get; private set; }

        void stoklar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select URUNAD,sum(Adet) as 'Adet' From TBL_URUNLER group by URUNAD having sum(adet)<=20 order by sum(adet)",bgl.baglanti());
            da.Fill(dt);
            gridControlAzStok.DataSource = dt;
        }

     
        
        private void frmAnaSayfa_Load(object sender, EventArgs e)
        {
            stoklar();
            listele();

            webBrowser1.Navigate("https://www.tcmb.gov.tr/wps/wcm/connect/tr/tcmb+tr/main+page+site+area/bugun");

            if (FrmHaberForm != null)
            {
                gridHaber.DataSource = ((frmHaber)FrmHaberForm).GridControl1.DataSource;
            }


        }

       
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select *From TBL_HABERLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1. DataSource = dt;
        }
     
        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if(SelectedTabPage == xtraTabPage2)
            {
                listele();
            }
        }

        private void smplbtnHaberEkle_Click_1(object sender, EventArgs e)
        {
            frmHaber komut = new frmHaber();
            komut.Show();
            FrmHaberForm = komut;
        }

        
    }
}
