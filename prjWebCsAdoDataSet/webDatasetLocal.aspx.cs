using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjWebCsAdoDataSet
{
    public partial class webDatasetLocal : System.Web.UI.Page
    {
        // Declaration de variables globales

        static DataSet setSport;
        static DataTable tabEkip;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                setSport = CreerDataset();
                RemplirListeEquipes();
                gridJoueurs.DataSource = setSport.Tables["Joueurs"];
                gridJoueurs.DataBind();
            }
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

        private static DataSet CreerDataset()
        {
            DataSet mySet = new DataSet();
            /////////----- creation des tables equipe et joueurs
            // Creer la table Equipes
            DataTable myTB = new DataTable("Equipes");

            // Creer le champ ou column RefEquipe
            DataColumn myCol = new DataColumn("RefEquipe", typeof(Int64));

            myCol.AutoIncrement = true;
            myCol.AutoIncrementSeed = 1;
            myCol.AutoIncrementStep = 1;
            // Sauvegarder le champ dans la table 
            myTB.Columns.Add(myCol);


            // Creer le champ ou column Nom
            myCol = new DataColumn("Nom", typeof(string));

            myCol.MaxLength = 50;
            // Sauvegarder le champ dans la table 
            myTB.Columns.Add(myCol);


            // Creer le champ ou column ville
            myCol = new DataColumn("Ville", typeof(string));

            myCol.MaxLength = 50;
            // Sauvegarder le champ dans la table 
            myTB.Columns.Add(myCol);



            // Creer le champ ou column budget
            myCol = new DataColumn("Budget", typeof(decimal));

            // Sauvegarder le champ dans la table 
            myTB.Columns.Add(myCol);



            // Creer le champ ou column coach
            myCol = new DataColumn("Coach", typeof(string));

            myCol.MaxLength = 50;
            // Sauvegarder le champ dans la table 
            myTB.Columns.Add(myCol);



            // Creer les champs indexes
            DataColumn[] mykey = new DataColumn[1];
            mykey[0] = myTB.Columns["RefEquipe"];
            myTB.PrimaryKey = mykey;


            // sauvegarde la Table
            mySet.Tables.Add(myTB);


            // Creer la table Joueurs
            myTB = new DataTable("Joueurs");

            // Creer le champ ou column RefJoueurs
            myCol = new DataColumn("RefJoueur", typeof(Int64));

            myCol.AutoIncrement = true;
            myCol.AutoIncrementSeed = 1;
            myCol.AutoIncrementStep = 1;
            // Sauvegarder le champ dans la table 
            myTB.Columns.Add(myCol);


            // Creer le champ ou column Nom
            myCol = new DataColumn("Nom", typeof(string));

            myCol.MaxLength = 50;
            // Sauvegarder le champ dans la table 
            myTB.Columns.Add(myCol);



            // Creer le champ ou column Poste
            myCol = new DataColumn("Poste", typeof(string));

            myCol.MaxLength = 50;
            // Sauvegarder le champ dans la table 
            myTB.Columns.Add(myCol);

            // Creer le champ ou column Salaire
            myCol = new DataColumn("Salaire", typeof(decimal));

            // Sauvegarder le champ dans la table 
            myTB.Columns.Add(myCol);


            // Creer le champ ou column Description
            myCol = new DataColumn("Description", typeof(string));
            myCol.MaxLength = 250;
            // Sauvegarder le champ dans la table 
            myTB.Columns.Add(myCol);


            // Creer le champ ou column ReferEquipe
            myCol = new DataColumn("ReferEquipe", typeof(Int64));
            // Sauvegarder le champ dans la table 
            myTB.Columns.Add(myCol);


            // Creer les champs indexes
            mykey = new DataColumn[1];
            mykey[0] = myTB.Columns["RefJoueur"];
            myTB.PrimaryKey = mykey;

            // sauvegarder la table Joueurs 
            mySet.Tables.Add(myTB);


            ///// ------     creation relation les tables        ---------//////
            DataRelation myRel = new DataRelation("Equipes_Joueurs", mySet.Tables["Equipes"].Columns["RefEquipe"],
                                                   mySet.Tables["Joueurs"].Columns["ReferEquipe"]);

            //sauvegarder la relation
            mySet.Relations.Add(myRel);


            ///// ------     remplir les tables de donnees     ---------//////
            myTB = mySet.Tables["Equipes"];
            // Remplir la table Equipes avec des donnees
            DataRow myRow = myTB.NewRow();

            myRow["Nom"] = "Real De Madrid";
            myRow["Ville"] = "Madrid (Espagne)";
            myRow["Budget"] = 150000000;
            myRow["Coach"] = "Carlos Ancelotti";

            // Sauvegarder l'enregistrement
            myTB.Rows.Add(myRow);


            // Remplir la table Equipes avec des donnees
            myRow = myTB.NewRow();

            myRow["Nom"] = "Barcelone";
            myRow["Ville"] = "Barcelone (Espagne)";
            myRow["Budget"] = 150000000;
            myRow["Coach"] = "Xavi";

            // Sauvegarder l'enregistrement
            myTB.Rows.Add(myRow);

            // Remplir la table Equipes avec des donnees
            myRow = myTB.NewRow();

            myRow["Nom"] = "Raja";
            myRow["Ville"] = "Casa (Maroc)";
            myRow["Budget"] = 150000;
            myRow["Coach"] = "Hassane 2";

            // Sauvegarder l'enregistrement
            myTB.Rows.Add(myRow);





            //Remplir tables Joueurs
            myTB = mySet.Tables["Joueurs"];
            myRow = myTB.NewRow();
            myRow["Nom"] = "Vinicius Junior";
            myRow["Poste"] = "Attaque";
            myRow["Salaire"] = 15000000;
            myRow["Description"] = "Meilleur ailier au monde";
            myRow["ReferEquipe"] = 1;

            // Sauvegarder joueur 
            myTB.Rows.Add(myRow);


            //Remplir tables Joueurs
            myRow = myTB.NewRow();
            myRow["Nom"] = "Bellighan Jude";
            myRow["Poste"] = "Attaque";
            myRow["Salaire"] = 15000000;
            myRow["Description"] = "Meilleur joueur au monde";
            myRow["ReferEquipe"] = 1;

            // Sauvegarder joueur 
            myTB.Rows.Add(myRow);


            //Remplir tables Joueurs
            myRow = myTB.NewRow();
            myRow["Nom"] = "Lamine Yamal";
            myRow["Poste"] = "Attaque";
            myRow["Salaire"] = 17000000;
            myRow["Description"] = "Meilleur jeune joueur au monde";
            myRow["ReferEquipe"] = 2;

            // Sauvegarder joueur 
            myTB.Rows.Add(myRow);


            //Remplir tables Joueurs
            myRow = myTB.NewRow();
            myRow["Nom"] = "Lewandosky Robert";
            myRow["Poste"] = "meilleur vieux joueur";
            myRow["Salaire"] = 1500;
            myRow["Description"] = "Neveu du roi";
            myRow["ReferEquipe"] = 2;

            // Sauvegarder joueur 
            myTB.Rows.Add(myRow);



            //Remplir tables Joueurs
            myRow = myTB.NewRow();
            myRow["Nom"] = "Aziz Bouderballa";
            myRow["Poste"] = "Goalkeeper";
            myRow["Salaire"] = 1500;
            myRow["Description"] = "Neveu du roi";
            myRow["ReferEquipe"] = 3;

            // Sauvegarder joueur 
            myTB.Rows.Add(myRow);


            return mySet;
        }

        protected void lstEquipes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // on recupere la value de l'element (Equipe) selectionne
            string refEquipChoisi = lstEquipes.SelectedItem.Value;

            //version Boucle pour trouver equipe
            /*foreach (DataRow myrow in setSport.Tables["Equipes"].Rows)
            {
                if(refEquipChoisi == myrow["RefEquipe"].ToString())
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
            foreach(DataRow myrowJr in setSport.Tables["Joueurs"].Rows)
            {
                if(refEquipChoisi == myrowJr["referEquipe"].ToString())
                {
                    tmpJoureurs.ImportRow(myrowJr);
                }
            }
            gridJoueurs.DataSource = tmpJoureurs;
            gridJoueurs.DataBind();

        }
    }
}
