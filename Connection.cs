using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace tp1_form_db
{
    class Connection
    {
        static string chaine = @"Data Source=LAPTOP-5FQRSO2V;Initial Catalog=TpForm1;Integrated Security=True";
        static SqlConnection cnx = new SqlConnection(chaine);
        static SqlCommand cmd = new SqlCommand();
        static SqlDataAdapter adapter = new SqlDataAdapter(cmd);


        public static void ajouter(Etudiant et)
        {
            cnx.Open();
            try
            {
                
                cmd.Connection = cnx;
                cmd.CommandText = "insert into dbo.Etudiant values('" + et.id + "','" + et.nom + "','" + et.prenom + "') ";
                int variable = cmd.ExecuteNonQuery();
                if (variable == 1)
                    Console.WriteLine("ouiiiiii");
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

            finally
            {
                cnx.Close();
            }
        }

        public static void supprimer(int id)
        {
            cnx.Open();
            try
            {
               
                cmd.Connection = cnx;
                cmd.CommandText = " delete from dbo.Etudiant where id = '" + id + "' ";
                int variable = cmd.ExecuteNonQuery();
                if (variable == 1)
                    Console.WriteLine("ouiiiiii");
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

            finally
            {
                cnx.Close();
            }
        }

        public static void modifier(Etudiant et)
        {
            cnx.Open();
            try
            {
                cmd.Connection = cnx;
                cmd.CommandText = " update dbo.Etudiant set nom='" + et.nom + "',prenom='" + et.prenom + "' where ID='" + et.id + "' ";
                int variable = cmd.ExecuteNonQuery();
                if (variable == 1)
                    Console.WriteLine("ouiiiiii");
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

            finally
            {
                cnx.Close();
            }

        }

        public static Etudiant selectionner(int id)
        {
            Etudiant et = new Etudiant();
            cnx.Open();
            try
            {
                cmd.CommandText = "select * from dbo.Etudiant where id = @fName ";
                cmd.Connection = cnx;
                cmd.Parameters.AddWithValue("@fName", id);

                SqlDataReader oReader = cmd.ExecuteReader();

                while (oReader.Read())
                {
                    et.id = Int32.Parse(oReader["id"].ToString());
                    et.nom = oReader["nom"].ToString();
                    et.prenom = oReader["prenom"].ToString();
                }
                cmd.Parameters.Clear();
                oReader.Close();          
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

            finally
            {
                cnx.Close();
            }

            return et;
        }

        public static void select_Combobox(ComboBox comboBox1)
        {
            Etudiant et = new Etudiant();
            cnx.Open();
            try
            {

                SqlCommand myCommand = new SqlCommand("SELECT ID,nom,prenom FROM dbo.Etudiant", cnx);
                SqlDataReader Reader = myCommand.ExecuteReader();

                while (Reader.Read())
                {
                    et.id = Int32.Parse(Reader[0].ToString());              
                    comboBox1.Items.Add(et.id );
                }

                Reader.Close();

            }

            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

            finally
            {
                cnx.Close();
            }
        }
    }
}
