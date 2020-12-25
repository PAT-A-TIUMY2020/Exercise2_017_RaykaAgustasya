using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service_017_RaykaAgustasya
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TI_UMY : ITI_UMY
    {
        private SqlConnection sqlConnection;
        private string connectionString;

        public string CreateMahasiswa(Mahasiswa mhs)
        {
            string msg = "GAGAL";
            SqlConnection sqlcon = new SqlConnection("Data Source=DESKTOP-O339P25;Initial Catalog=TI UMY;Persist Security Info=True;User ID=sa;Password=dede162700");
            string query = string.Format("Insert into dbo.Mahasiswa values ('{0}', '{1}', '{2}', '{3}',)", mhs.nama, mhs.nim, mhs.prodi, mhs.angkatan);
            //NIM = '{0}'", nim
            SqlCommand sqlcom = new SqlCommand(query, sqlcon); //yang dikirim ke sql

            try
            {
                sqlcon.Open(); //membuka connection sql
                Console.WriteLine(query);
                sqlcom.ExecuteNonQuery(); //mengeksusi untuk memasukkan data
                sqlcon.Close();
                msg = "sukses";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
                msg = "GAGAL";
            }
            return msg;
        }

        public string DeleteMahasiswa(string nim)
        {
            sqlConnection = new SqlConnection(connectionString);
            string query = string.Format("DELETE from dbo.Mahasiswa where NIM = '{0}'", nim);
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            int result = 0;
            string a = "Gagal";

            try
            {
                sqlConnection.Open();
                result = cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                a = ex.ToString();
            }

            if (result != 0)
            {
                a = "Sukses";
            }
            return a;
        }


        public List<Mahasiswa> GetAllMahasiswa()
        {
            List<Mahasiswa> mahas = new List<Mahasiswa>();

            SqlConnection con = new SqlConnection("Data Source= DESKTOP-O339P25;Initial Catalog=\"TI UMY\";Persist Security Info=True;User ID=sa;Password=dede162700");
            string query = "select Nama, NIM, Prodi, Angkatan from dbo.Mahasiswa";
            SqlCommand com = new SqlCommand(query, con); //yang dikirim ke sql

            try
            {
                con.Open(); //membuka connection sql
                SqlDataReader reader = com.ExecuteReader(); //mendapatkan data telah dieksekusi, dari select. hasil query ditaro di reader
                while (reader.Read())
                {
                    Mahasiswa mhs = new Mahasiswa();
                    mhs.nama = reader.GetString(0);
                    mhs.nim = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);

                    mahas.Add(mhs);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return mahas; //output
        }

        public Mahasiswa GetMahasiswaByNIM(string nim)
        {
            Mahasiswa mhs = new Mahasiswa();
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-O339P25;Initial Catalog=TI UMY;Persist Security Info=True;User ID=sa;Password=dede162700");
            string query = string.Format("select Nama, NIM, Prodi, Angkatan from dbo.Mahasiswa where NIM = '{0}'", nim);
            SqlCommand com = new SqlCommand(query, con);
            try
            {
                con.Open(); //membuka connection sql
                SqlDataReader reader = com.ExecuteReader(); //mendapatkan data telah dieksekusi, dari select. hasil query ditaro di reader
                while (reader.Read())
                {
                    mhs.nama = reader.GetString(0);
                    mhs.nim = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return mhs; //output
        }

        public string UpdateMahasiswaByNIM(Mahasiswa mhs)
        {
            string msg = "Gagal";

            sqlConnection = new SqlConnection(connectionString);
            string query = string.Format("Update dbo.Mahasiswa set Nama = '{0}', Prodi = '{1}', Angkatan = '{2}' where NIM = '{3}'", mhs.nama, mhs.prodi, mhs.angkatan, mhs.nim);
            SqlCommand cmd = new SqlCommand(query, sqlConnection);

            try
            {
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                msg = "Sukses";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return msg;
        }
    }
}