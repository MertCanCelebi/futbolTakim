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
using Npgsql;

namespace futbolTakim
{
    public partial class Form1 : Form
    {
        NpgsqlConnection baglanti;
        DataSet ds;
        NpgsqlDataAdapter veriadaptoru;
        public Form1()
        {
            InitializeComponent();
        }
        void oyuncuGetir()
        {
            string sorgu = "select *from oyuncu";
            baglanti = new NpgsqlConnection("server=localHost;port=5432;Database=futboltakimlari; User ID=postgres; Password=mert1903");
            veriadaptoru = new NpgsqlDataAdapter(sorgu, baglanti);
            ds = new DataSet();
            veriadaptoru.Fill(ds);
            dgwOyuncu.DataSource = ds.Tables[0];
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            oyuncuGetir();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into oyuncu(tcno,adi,deger,yasi,ulkekodu,sozlesmeid,takimno) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)",baglanti);
            komut1.Parameters.AddWithValue("@p1", txtTcno.Text);
            komut1.Parameters.AddWithValue("@p2", txtAdi.Text);
            komut1.Parameters.AddWithValue("@p3", int.Parse(txtDeger.Text));
            komut1.Parameters.AddWithValue("@p4", int.Parse(txtYas.Text));
            komut1.Parameters.AddWithValue("@p5", int.Parse(txtUlkeKodu.Text));
            komut1.Parameters.AddWithValue("@p6", int.Parse(txtSozlesmeId.Text));
            komut1.Parameters.AddWithValue("@p7", int.Parse(txtTakimNo.Text));
            komut1.ExecuteNonQuery();
            baglanti.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("delete from oyuncu where tcno=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", txtTcno.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update oyuncu set adi=@p1,deger=@p2,yasi=@p3,ulkekodu=@p4,sozlesmeid=@p5,takimno=@p6 where tcno=@p7", baglanti);
            komut3.Parameters.AddWithValue("@p1", txtAdi.Text);
            komut3.Parameters.AddWithValue("@p2", int.Parse(txtDeger.Text));
            komut3.Parameters.AddWithValue("@p3", int.Parse(txtYas.Text));
            komut3.Parameters.AddWithValue("@p4", int.Parse(txtUlkeKodu.Text));
            komut3.Parameters.AddWithValue("@p5", int.Parse(txtSozlesmeId.Text));
            komut3.Parameters.AddWithValue("@p6", int.Parse(txtTakimNo.Text));
            komut3.Parameters.AddWithValue("@p7", txtTcno.Text);
            komut3.ExecuteNonQuery();
            baglanti.Close();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut4 = new NpgsqlCommand("select *from oyuncu where adi like '%"+txtAra.Text+"%'", baglanti);
            veriadaptoru = new NpgsqlDataAdapter(komut4);
            ds = new DataSet();
            veriadaptoru.Fill(ds);
            dgwOyuncu.DataSource = ds.Tables[0];
            baglanti.Close();

        }
    }
    
}
