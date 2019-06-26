using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Victoria.AccountMethod
{
    public class Account
    {
        #region SHA512_System
        public static string SHA512(string StringIn)
        {
            string hashString;
            using (var sha512 = SHA512Managed.Create())
            {
                var hash = sha512.ComputeHash(Encoding.Default.GetBytes(StringIn));
                hashString = ToHex(hash, true);
            }

            return hashString;
        }

        public static string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "X2"));

            return result.ToString();
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        #endregion
        #region Account Creation & Verification
        public static void AccountCreation(string user, string pwd)
        {
            // Variable HASH
            string strHash;
            string strHashFinal;

            // Note : L'utilisateur peut éventuellement utiliser une adresse e-mail. 

            bool isEmail = Regex.IsMatch(user, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (user.Contains("@"))
            {
                if (isEmail)
                {
                    strHash = SHA512(user);
                    strHashFinal = Reverse(SHA512(strHash + "#@!@#" + pwd));

                    // Ajout du compte dans la base de donnée MySql


                }
                else
                {
                    MessageBox.Show("L'adresse e-mail n'est pas conforme au standard.", "Utilisation d'une adresse e-mail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // Création du compte avec le pseudonyme.
                strHash = SHA512(user);
                strHashFinal = Reverse(SHA512(strHash + "#@!@#" + pwd)); 

                // Ajout du compte dans la base de donnée MySql
            }

        }
        public static void AccountVerification(string hash)
        {

        }
        #endregion

    }
}
