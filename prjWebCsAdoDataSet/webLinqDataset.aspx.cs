using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjWebCsAdoDataSet
{
    public partial class webLinqDataset : System.Web.UI.Page
    {

        // Declaration de variables globales
        static DataSet setSport;
        static DataTable tabEquipes , tabJoueurs;
        static SqlDataAdapter adpEquipe;
        static string mode;
        static string refEquipChoisi;
        static int indiceChoisi;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                setSport = OuvrirRemplirDataSet();
                tabEquipes = setSport.Tables["Equipes"];
                tabJoueurs = setSport.Tables["Joueurs"];
                 RemplirListeEquipes();

                lstEquipes.SelectedIndex = 0; //choisi le premier element
                lstEquipes_SelectedIndexChanged(sender, e);

                //tabEkip = setSport.Tables["Joueurs"];
                //gridJoueurs.DataSource = tabEkip;
                //gridJoueurs.DataBind();


                //tests Linq
                // Single[] tabNotes = new Single[] { 90, 56, 34, 89, 65, 25, 85, 80, 60, 99 };
                // List<Single> bonnesNotes = new List<Single>();
                //version boucle
                // foreach(Single note in tabNotes)
                // {
                //     if(note >= 60)
                //     {
                //         bonnesNotes.Add(note);
                //     }
                // }

                // //version Linq
                //var LesGoodNotes = from Single note in tabNotes 
                //                   where note >=50 
                //                   && note <=60
                //                   orderby note descending
                //                   select note ;


                // gridJoueurs.DataSource = LesGoodNotes ;
                // gridJoueurs.DataBind();

                //var LesEquipesRiches = from DataRow myrow in tabEquipes.Rows
                //                       where Convert.ToDecimal(myrow["Budget"]) >= 100000000
                //                       select new 
                //                       {
                //                        LesEquipes = myrow["Nom"].ToString(),
                //                        Budget = myrow["Budget"].ToString(),
                //                        Villes = myrow["Ville"].ToString(),

                //                       };

                // gridJoueurs.DataSource = LesEquipesRiches.ToList();
                // gridJoueurs.DataBind();
            }
        }

        private void RemplirListeEquipes()
        {
            //Version /databinding avec LINQ
            var LesEquipes = from DataRow myrow in tabEquipes.Rows 
                             select new
                             { 
                              RefEquipe = myrow["RefEquipe"].ToString(),
                              Nom = myrow["Nom"].ToString()
                             };

            //colonne a afficher
            lstEquipes.DataTextField = "Nom";
            //colonne a cacher
            lstEquipes.DataValueField = "RefEquipe";
            lstEquipes.DataSource = LesEquipes;
            lstEquipes.DataBind();
        }

        protected void lstEquipes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // on recupere la value de l'element (Equipe) selectionne
            refEquipChoisi = lstEquipes.SelectedItem.Value;

            indiceChoisi = lstEquipes.SelectedIndex;
            // -- Trouver Equipe choisie --//
            var LesEquipesChoisies = from DataRow myrow in tabEquipes.Rows
                                     where myrow["RefEquipe"].ToString() == refEquipChoisi.ToString()
                                     select new
                                     {
                                         Nom = myrow["Nom"].ToString(),
                                         Ville = myrow["Ville"].ToString(),
                                         Budget = myrow["Budget"].ToString(),
                                         Coach = myrow["Coach"].ToString()

                                     };
           // var myRow = LesEquipesChoisies.First();
            var myRow = LesEquipesChoisies.ElementAt(0);
            txtNom.Text = myRow.Nom;
            txtVille.Text = myRow.Ville;
            txtBudget.Text = myRow.Budget;
            txtCoach.Text = myRow.Coach;

            // -- Trouver Equipe choisie --//

            var LesJoueurs = from DataRow myrowjr in tabJoueurs.Rows
                             where myrowjr["ReferEquipe"].ToString() == refEquipChoisi.ToString()
                             select myrowjr;
            if(LesJoueurs.Count() > 0)
            {
            gridJoueurs.DataSource = LesJoueurs.CopyToDataTable();
            }
            else
            {
                gridJoueurs.DataSource = null;
            }
            gridJoueurs.DataBind();
           



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
    }
}