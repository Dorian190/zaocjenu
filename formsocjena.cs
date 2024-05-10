using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace UserRegistrationApp
{
    public partial class MainForm : Form
    {
        private List<string[]> userData = new List<string[]>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string firstName = textBox1.Text.Trim();
            string lastName = textBox2.Text.Trim();
            string birthYear = textBox3.Text.Trim();
            string email = textBox4.Text.Trim();

            if (ValidateInput(firstName, lastName, birthYear, email))
            {
                string[] userInfo = { firstName, lastName, birthYear, email };
                userData.Add(userInfo);
                SaveToCSV(userInfo);
                ClearFields();
                MessageBox.Show("Podaci su uspješno spremljeni.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool ValidateInput(string firstName, string lastName, string birthYear, string email)
        {
            // Provjera praznih polja
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(birthYear) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Molimo unesite sve potrebne podatke.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Provjera ispravnosti godine rođenja
            if (!int.TryParse(birthYear, out int year) || year <= 0 || year > DateTime.Now.Year)
            {
                MessageBox.Show("Molimo unesite ispravnu godinu rođenja.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Provjera ispravnosti e-mail adrese
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show("Molimo unesite ispravnu e-mail adresu.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void SaveToCSV(string[] userInfo)
        {
            string filePath = "korisnici.csv";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(string.Join(",", userInfo));
            }
        }

        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
    }
}

