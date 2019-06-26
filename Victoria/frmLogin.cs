using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Victoria.AccountMethod;
using Victoria.MySql;

namespace Victoria
{
    public partial class frmLogin : Form
    {
        // Variable d'entrée utilisateur.
        public string strAccName;
        public string strPwd;
        public string strShaName;
        public string strShaFinal;

        // Variable du serveur.
        public string srvIP;
        public string srvName;
        public int srvPort = 9000; // Port de communication du serveur.

        // Variable d'environnement
        public string strOS;

        // Vérification utilisateur
        bool bCreateAccount;


        public frmLogin()
        {
            InitializeComponent();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            strAccName = tbxLogin.Text;
            strPwd = tbxPass.Text;

            if (bCreateAccount)
            {
                try
                {
                    if (strAccName.Length <= 0 || strPwd.Length <= 0)
                    {
                        MessageBox.Show("Une erreur de saisie est survenue ! Les deux cases doivent être rempli.", "Erreur de saisie !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Suite à l'inscription de l'utilisateur, les différents objets reprennent leur nom par défaut.
                    }
                    else
                    {
                        Account.AccountCreation(tbxLogin.Text, tbxPass.Text);
                        MessageBox.Show("Bienvenue au sein du système Victoria ! Votre compte a correctement été crée.", "Bienvenue !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch
                {
                    MessageBox.Show("Erreur inconnue, survenue à la méthode de la création du compte.", "Erreur programme", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }



            }
            else
            {
                // Système de sécurité Victoria (SHA512) : 
                // 1. On hash le nom de l'utilisateur, on sauvegarde le fameux hash dans une variable.
                // 2. Dernière étape, on inverse le SHA final qui comporte le hash de l'utilisateur précédemment hasher, celui du mot de passe ainsi qu'un symbole.
                strShaName = Account.SHA512(tbxLogin.Text);
                strShaFinal = Account.Reverse(Account.SHA512(strShaName + "#@!@#" + tbxPass.Text));

                // Fonction de vérification
                Account.AccountVerification(strShaFinal);
            }

        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            // EULA Check
            // Lors du premier lancement ou si modification d'un règlement, au chargement de la page, le règlement s'affichera.


        }

        private void LblNoAccount_Click(object sender, EventArgs e)
        {
            // Lorsque l'utilisateur clique sur le label, les différents objets s'adaptent à la situation.
            bCreateAccount = true;

            lblNoAccount.Text = "Merci de remplir les cases ci-dessous";
            btnConnect.Text = "S'inscrire";
            lblAccountName.Text = "Pseudonyme ou adresse e-mail : ";


        }
    }
}
