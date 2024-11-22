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
                gridJoueurs.DataSource = setSport.Tables[0] ;
                gridJoueurs.DataBind();
            }
        }

        private static DataSet CreerDataset()
        {
            DataSet mySet = new DataSet();
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



            // sauvegarde la Table
            mySet.Tables.Add(myTB);

            ///////////////////////////////////////////////////////////////////////////////////

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


            // sauvegarde la Table
            mySet.Tables.Add(myTB);




            return mySet;
        }
    }
}