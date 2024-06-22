﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace VeriTabanli_SecimProjesi
{
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-BIP870C;Initial Catalog=DbSecimProje;Integrated Security=True");

        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            //İLÇE LARINI COMBOBOX A ÇEKME
            baglanti.Open();
            SqlCommand komut =new SqlCommand("Select ILCEAD from TBLILCE", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }

            baglanti.Close();

            //Grafiğe toplam Sonucları Getirme--parti bazlı sonuc olacak
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Select SUM(APARTI),SUM(BPARTI), SUM(CPARTI), SUM(DPARTI), SUM(EPARTI) FROM TBLILCE",baglanti);

            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chart1.Series["Partiler"].Points.AddXY("A Parti", dr2[0]);
                chart1.Series["Partiler"].Points.AddXY("B Parti", dr2[1]);
                chart1.Series["Partiler"].Points.AddXY("C Parti", dr2[2]);
                chart1.Series["Partiler"].Points.AddXY("D Parti", dr2[3]);
                chart1.Series["Partiler"].Points.AddXY("E Parti", dr2[4]);
            }
            baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //combobox takı deger degıstıgı zaman ıslem gerceklesssın
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLILCE where ILCEAD=@P1", baglanti);

            komut.Parameters.AddWithValue("@P1", comboBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                progressBar1.Value = int.Parse(dr[2].ToString());
                progressBar2.Value = int.Parse(dr[3].ToString());
                progressBar3.Value = int.Parse(dr[4].ToString());
                progressBar4.Value = int.Parse(dr[5].ToString());
                progressBar5.Value = int.Parse(dr[6].ToString());

                LblA.Text = dr[2].ToString();
                LblB.Text = dr[3].ToString();
                LblC.Text = dr[4].ToString();
                LblD.Text = dr[5].ToString();
                LblE.Text = dr[6].ToString();

            }
            baglanti.Close();
        }
    }
}
