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

namespace tp1_form_db
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }
        string btn_confirm;
        int? selectedIndex = null ;

        private void Form1_Load(object sender, EventArgs e)
        {
            Connection.select_Combobox(comboBox1);
            btn_ajt.Enabled = true;
            btn_annuler.Enabled = false;
            btn_supp.Enabled = false;
            btn_modifier.Enabled = false;
            btn_confirmer.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
        }

        private void btn_ajt_Click(object sender, EventArgs e)
        {
            btn_confirm = "ajout";
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            btn_confirmer.Enabled = true;
            btn_annuler.Enabled = true;
            btn_ajt.Enabled = false;
           
        }

        private void btn_supp_Click(object sender, EventArgs e)
        {
            
            btn_confirm = "supp";
            btn_confirmer.Enabled = true;
            btn_annuler.Enabled = true;
            btn_supp.Enabled = false;
            btn_modifier.Enabled = false;
        }

        private void btn_modifier_Click(object sender, EventArgs e)
        {
            btn_confirm = "modifier";
            textBox1.Enabled = false;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            btn_confirmer.Enabled = true;
            btn_annuler.Enabled = true;
            btn_supp.Enabled = false;
            btn_modifier.Enabled = false;
        }

        private void btn_confirmer_Click(object sender, EventArgs e)
        {
            switch (btn_confirm)
            {
                case ("ajout"):
                    
                    string message = " voulez vous ajouter cet etudiant ? ";
                    string title = "confirmer l'ajout !";
                    MessageBoxButtons btns = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show(message, title, btns);
                    if (result == DialogResult.Yes)
                    {
                        Etudiant et = new Etudiant();
                        et.id = Int32.Parse(textBox1.Text);
                        et.nom = textBox2.Text;
                        et.prenom = textBox3.Text;
                        Connection.ajouter(et);
                        comboBox1.Items.Add(et.id);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                    }
                    
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    btn_confirmer.Enabled = false;
                    btn_annuler.Enabled = false;
                    btn_ajt.Enabled = true;
                    break;

                case ("supp"):
                    string message1 = " etes-vous sur supprimer cet etudiant ?  ";
                    string title1 = "confirmer la suppression";
                    MessageBoxButtons btns1 = MessageBoxButtons.YesNo;
                    DialogResult result1 = MessageBox.Show(message1, title1, btns1);
                    if (result1 == DialogResult.Yes)
                    {                       
                        Connection.supprimer(Int32.Parse(comboBox1.Text));
                        comboBox1.Items.RemoveAt(selectedIndex.Value);
                        selectedIndex = null;
                    }                  
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    btn_ajt.Enabled = true;
                    btn_supp.Enabled = false;
                    btn_modifier.Enabled = false;
                    btn_confirmer.Enabled = false;
                    btn_annuler.Enabled = false;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    break;

                case ("modifier"):
                    string message2 = " voulez vous modifier cet etudiant ? ";
                    string title2 = " confirmer la modification !";
                    MessageBoxButtons btns2 = MessageBoxButtons.YesNo;
                    DialogResult result2 = MessageBox.Show(message2, title2, btns2);
                    if (result2 == DialogResult.Yes)
                    {
                        
                        comboBox1.Items.Remove(comboBox1.SelectedItem);
                        if (selectedIndex == null) return;
                        Etudiant et = new Etudiant();
                        et.id = Int32.Parse(textBox1.Text);
                        et.nom = textBox2.Text;
                        et.prenom = textBox3.Text;
                        Connection.modifier(et);
                        comboBox1.Items.Insert(selectedIndex.Value, et.id );
                    }
                    selectedIndex = null;
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    btn_ajt.Enabled = true;
                    btn_supp.Enabled = false;
                    btn_modifier.Enabled = false;
                    btn_confirmer.Enabled = false;
                    btn_annuler.Enabled = false;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    break;

            }
           
        }

        private void btn_annuler_Click(object sender, EventArgs e)
        {
            switch (btn_confirm)
            {
                case ("ajout"):  
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    btn_confirmer.Enabled = false;
                    btn_annuler.Enabled = false;
                    btn_ajt.Enabled = true;
                    break;

                case ("supp"):
                   
                    btn_confirmer.Enabled = false;
                    btn_annuler.Enabled = false;
                    btn_modifier.Enabled = true;
                    btn_supp.Enabled = true;
                    btn_ajt.Enabled = false;
                    break;

                case ("modifier"):
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    btn_confirmer.Enabled = false;
                    btn_annuler.Enabled = false;
                    btn_modifier.Enabled = true;
                    btn_supp.Enabled = true;
                    btn_ajt.Enabled = false;
                    break;
            }

            }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = comboBox1.SelectedIndex;
            Etudiant et = new Etudiant();
            et = Connection.selectionner(Int32.Parse(comboBox1.Text));
            btn_supp.Enabled = true;
            btn_modifier.Enabled = true;
            btn_ajt.Enabled = false;
            textBox1.Text = et.id.ToString();
            textBox2.Text = et.nom;
            textBox3.Text = et.prenom;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
