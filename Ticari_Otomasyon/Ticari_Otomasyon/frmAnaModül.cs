using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticari_Otomasyon
{
    public partial class frmAnaModül : Form
    {
        public frmAnaModül()
        {
            InitializeComponent();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        FrmÜrünler fr;
        private void btnÜrünler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr == null || fr.IsDisposed )
            {
                fr = new FrmÜrünler();
                fr.MdiParent = this;
                fr.Show();
            }
             
        }
        frmMüşteriler fr2;
        private void btnMüşteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr2 == null || fr2.IsDisposed)
            {
                fr2 = new frmMüşteriler();
                fr2.MdiParent = this;
                fr2.Show();
            }
        }
        frmPersoneller fr3;
        private void btnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr3 == null || fr3.IsDisposed)
            {
                fr3 = new frmPersoneller();
                fr3.MdiParent = this;
                fr3.Show();
            }

        }
        frmStoklar fr4;
        private void btnStoklar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr4 == null || fr4.IsDisposed)
            {
                fr4 = new frmStoklar();
                fr4.MdiParent = this;
                fr4.Show();
            }
        }
        frmFaturalar fr5;
        private void btnFaturalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr5 == null || fr5.IsDisposed)
            {
                fr5 = new frmFaturalar();
                fr5.MdiParent = this;
                fr5.Show();
            }
        }
        frmAnaSayfa fr6;
        private void btnAnasayfa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr6 == null || fr6.IsDisposed)
            {
                fr6 = new frmAnaSayfa();
                fr6.MdiParent = this;
                fr6.Show();
            }
        }
        frmAyarlar fr7;
        private void btnAyarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr7 == null || fr7.IsDisposed)
            {
                fr7 = new frmAyarlar();
                fr7.Show();
            }
        }
        
    }
}
