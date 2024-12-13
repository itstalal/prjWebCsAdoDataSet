using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjWebCsAdoDataSet
{
    public partial class webConnectDataset : System.Web.UI.Page
    {

        // Declaration de variables globales
        static DataSet setSport;
        static DataTable tabEkip;
        static SqlDataAdapter adpEquipe;
        static string mode;
        static string refEquipChoisi;
        static int indiceChoisi;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                //premier point
                setSport = OuvrirRemplirDataSet();
                RemplirListeEquipes();

                 lstEquipes.SelectedIndex = 0; //choisi le premier element
                lstEquipes_SelectedIndexChanged(sender, e);

                tabEkip = setSport.Tables["Joueurs"];
                gridJoueurs.DataSource = tabEkip;
                gridJoueurs.DataBind();
                cacherBoutons(true, false);

            }
        }

        private DataSet OuvrirRemplirDataSet()
        {
            // se connecter a la BD, recuperer les deux tables et les inserer dans le dataset (une copie)

            DataSet mySet = new DataSet();

            // etape 1 connecter a la BD
            SqlConnection mycon = new SqlConnection();
            mycon.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\elmou\\Session4_Cs\\prjWebCsAdoDataSet\\prjWebCsAdoDataSet\\prjWebCsAdoDataSet\\App_Data\\SportDb.mdf;Integrated Security=True";
            mycon.Open();

            //INSERER table Equipes

            // etape 2 creer la commande 
            string sql = "SELECT RefEquipe , Nom , Ville , Budget , Coach FROM Equipes";
            SqlCommand mycmd = new SqlCommand(sql, mycon);

            //etape 3 creer le dataadapter
             adpEquipe = new SqlDataAdapter(mycmd);
            // Remplir le dataset avec la table  
            adpEquipe.Fill(mySet, "Equipes");

            //INSERER table Joueurs

            // etape 2 creer la commande 
            sql = "SELECT RefJoueur , Nom , Poste , Salaire , Description, ReferEquipe FROM Joueurs";
            SqlCommand mycmd2 = new SqlCommand(sql, mycon);


            //etape 3 creer le dataadapter
            SqlDataAdapter adpJoueur = new SqlDataAdapter(mycmd2);
            // Remplir le dataset avec la table  
            adpJoueur.Fill(mySet, "Joueurs");

            // indexer le champ RefEquipe de la table equipe comme cle primaire
            DataColumn[] mykey = new DataColumn[1];
            mykey[0] = mySet.Tables["Equipes"].Columns["RefEquipe"];
            mySet.Tables["Equipes"].PrimaryKey = mykey;



            return mySet;
        }

        private void RemplirListeEquipes()
        {
            //Remplir le listbox avec les equipe (Nom , Ref equipe)
            //version Boucle
            /* foreach(DataRow myrow in setSport.Tables["Equipes"].Rows) 
             {
                 ListItem elm = new ListItem();
                 elm.Text = myrow["Nom"].ToString();
                 elm.Value = myrow["RefEquipe"].ToString() ;
                 lstEquipes.Items.Add(elm);
             }*/
            //version databinding
            //colonne a afficher
            lstEquipes.DataTextField = "Nom";
            //colonne a cacher
            lstEquipes.DataValueField = "RefEquipe";
            lstEquipes.DataSource = setSport.Tables["Equipes"];
            lstEquipes.DataBind();
        }

        protected void lstEquipes_SelectedIndexChanged(object sender, EventArgs e)
        {
            indiceChoisi = lstEquipes.SelectedIndex;
            // on recupere la value de l'element (Equipe) selectionne
            refEquipChoisi = lstEquipes.SelectedItem.Value;

            //version Boucle pour trouver equipe
            /*  foreach (DataRow myrow in setSport.Tables["Equipes"].Rows)
              {
                  if (refEquipChoisi == myrow["RefEquipe"].ToString())
                  {
                      txtNom.Text = myrow["Nom"].ToString();
                      txtVille.Text = myrow["Ville"].ToString();
                      txtBudget.Text = myrow["Budget"].ToString();
                      txtCoach.Text = myrow["Coach"].ToString();
                      break;
                  }*/
            //version POO avec la methode Find de Rows 
            DataRow myrow = setSport.Tables["Equipes"].Rows.Find(refEquipChoisi);
                 txtNom.Text = myrow["Nom"].ToString();
                 txtVille.Text = myrow["Ville"].ToString();
                 txtBudget.Text = myrow["Budget"].ToString();
                 txtCoach.Text = myrow["Coach"].ToString();



                 //-------- Trouver les joueurs de cette equipe --------///
                 //version boucle 
                 //creer une table temporaire de la meme structure que la table joueurs 
                 DataTable tmpJoureurs = setSport.Tables["Joueurs"].Clone();
                 foreach (DataRow myrowJr in setSport.Tables["Joueurs"].Rows)
                 {
                     if (refEquipChoisi == myrowJr["referEquipe"].ToString())
                     {
                         tmpJoureurs.ImportRow(myrowJr);
                     }
                 }
                 gridJoueurs.DataSource = tmpJoureurs;
                 gridJoueurs.DataBind();
            }
        private void cacherBoutons(bool btnAjModSup, bool btnSauvAn)
        {
            btnAjouter.Visible = btnAjModSup;
            btnModifier.Visible = btnAjModSup;
            btnSupprimer.Visible = btnAjModSup;
            btnSauvgarder.Visible = btnSauvAn;
            btnAnnuler.Visible = btnSauvAn;
        }

        protected void btnAjouter_Click(object sender, EventArgs e)
        {
            
                mode = "ajout";
                txtBudget.Text = txtCoach.Text = txtNom.Text = txtVille.Text = " ";
                txtNom.Focus();
                cacherBoutons(false, true);
            
        }

        protected void btnModifier_Click(object sender, EventArgs e)
        {
            mode = "modif";
            txtNom.Focus();
            cacherBoutons(false, true);
        }

        protected void btnSupprimer_Click(object sender, EventArgs e)
        {
            DataRow myrow = setSport.Tables["Equipes"].Rows.Find(refEquipChoisi);
            myrow.Delete();
            //Syncroniser ou mettre a jour Dataset vers Database
            SqlCommandBuilder myBuilder = new SqlCommandBuilder(adpEquipe);
            adpEquipe.Update(setSport, "Equipes");
            RemplirListeEquipes();
            lstEquipes.SelectedIndex = 0; //choisi le premier element
            lstEquipes_SelectedIndexChanged(sender, e);

        }

        protected void btnSauvgarder_Click(object sender, EventArgs e)
        {
            DataTable myTb = setSport.Tables["Equipes"];
            if (mode == "ajout")
            {
                // Remplir la table Equipes avec des donnees
                DataRow myRow = myTb.NewRow();
                myRow["RefEquipe"] = myTb.Rows.Count + 1;

                myRow["Nom"] = txtNom.Text;
                myRow["Ville"] = txtVille.Text;
                myRow["Budget"] = Convert.ToDecimal(txtBudget.Text);
                myRow["Coach"] = txtCoach.Text;

                // Sauvegarder l'enregistrement Dans le dataset
                myTb.Rows.Add(myRow);
                //Syncroniser ou mettre a jour Dataset vers Database
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(adpEquipe);
                adpEquipe.Update(setSport, "Equipes");

                lstEquipes.SelectedIndex = lstEquipes.Items.Count - 1; //choisi le premier element

            }

            if (mode == "modif")
            {
                DataRow myRow = myTb.Rows.Find(refEquipChoisi);
                myRow["Nom"] = txtNom.Text;
                myRow["Ville"] = txtVille.Text;
                myRow["Budget"] = Convert.ToDecimal(txtBudget.Text);
                myRow["Coach"] = txtCoach.Text;


                lstEquipes.SelectedIndex = indiceChoisi; //choisi le premier element


            }
            mode = "";
            RemplirListeEquipes();
            lstEquipes_SelectedIndexChanged(sender, e);
            cacherBoutons(true, false);
        }

        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            lstEquipes.SelectedIndex = indiceChoisi; //choisi le premier element
            cacherBoutons(true,false);
            lstEquipes_SelectedIndexChanged(sender, e);
        }
    }
    }
