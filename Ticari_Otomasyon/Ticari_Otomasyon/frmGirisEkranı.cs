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
    public partial class frmGirisEkranı : Form
    {
        string connectionString = "Data Source=emre-samuk;Initial Catalog=DboTicariOtomasyon;Integrated Security=True";
        public frmGirisEkranı()
        {
            InitializeComponent();
            button1.Click += button1_Click;
        }

        void renklendirme()
        {
            label1.BackColor = Color.Transparent; 
            label2.BackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullanıcıAdı.Text;
            string sifre = txtŞifre.Text;

            // Veritabanına bağlantı oluştur
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Kullanıcı adı ve şifreyi kontrol etmek için SQL sorgusu
                    string query = "SELECT COUNT(*) FROM TBL_ADMIN WHERE KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Parametreleri eşleştir
                        command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                        command.Parameters.AddWithValue("@Sifre", sifre);

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            // Giriş başarılı
                           // MessageBox.Show("Giriş başarılı!");
                               

                            // Yönlendirilecek formun örneğini oluştur
                            frmAnaModül frmAnaModul = new frmAnaModül();
                            frmAnaModul.Show();

                            this.Hide(); // Giriş formunu gizleme
                        }
                        else
                        {
                            // Giriş başarısız
                            MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Hata durumunda işlemler
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }

        }

        private void frmGirisEkranı_Load(object sender, EventArgs e)
        {
            renklendirme();
        }
    }


}
    


    

