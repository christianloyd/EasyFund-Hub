using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Xml.Schema;

namespace EasyFund_Hub
{

    public partial class Form1 : Form
    {
        private OleDbConnection conn;
        private OleDbCommand cmd;
        private OleDbDataAdapter adapter;
        private DataTable dt;

        public Form1()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            GetMembers();
        }

        // Method to initialize the database connection
        private void InitializeDatabaseConnection()
        {
            conn = new OleDbConnection("Provider=Microsoft.ACE.OleDb.16.0;Data Source=C:\\Users\\ADMIN\\Desktop\\easyfund.accdb");
        }

        // Method to retrieve member data from the MS Access database
        private void GetMembers()
        {
            dt = new DataTable();
            adapter = new OleDbDataAdapter();

            try
            {
                string query = "SELECT * FROM Members";

                using (cmd = new OleDbCommand(query, conn))
                {
                    adapter.SelectCommand = cmd;

                    conn.Open();
                    adapter.Fill(dt);
                }

                // Assign the data outside of this method
                BindDataToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // Separate method to bind data to DataGridView
        private void BindDataToGrid()
        {
            dgwMembers.DataSource = dt;
        }



        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNumber.Text) ||
                string.IsNullOrWhiteSpace(txtNationality.Text) ||
                string.IsNullOrWhiteSpace(txtHomeAddress.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            if (!long.TryParse(txtPhoneNumber.Text, out _))
            {
                MessageBox.Show("Please enter a valid phone number.");
                return;
            }

            string query = "INSERT INTO Members (FirstName, MiddleName, LastName, Birthdate, PhoneNumber, Nationality, HomeAddress, DateBecomeMember) " +
                           "VALUES (@firstname, @middlename, @lastname, @birthdate, @phonenumber, @nationality, @Homeaddress, @datebecomemember)";

            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@firstname", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@middlename", txtMiddleName.Text);
                cmd.Parameters.AddWithValue("@lastname", txtLastName.Text);
                cmd.Parameters.AddWithValue("@birthdate", dtpBirthdate.Value.Date);
                cmd.Parameters.AddWithValue("@phonenumber", txtPhoneNumber.Text);
                cmd.Parameters.AddWithValue("@nationality", txtNationality.Text);
                cmd.Parameters.AddWithValue("@Homeaddress", txtHomeAddress.Text);
                cmd.Parameters.AddWithValue("@datebecomemember", dtpDateBecomeMember.Value.Date);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Member Added Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    GetMembers(); // Refresh DataGridView
                    ClearFields(); // Clear input fields
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void ClearFields()
        {
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
            dtpBirthdate.Value = DateTime.Today;
            txtPhoneNumber.Text = "";
            txtNationality.Text = "";
            txtHomeAddress.Text = "";
            dtpDateBecomeMember.Value = DateTime.Today;
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgwMembers.SelectedRows.Count > 0)
                {
                    // Get the selected member's account number
                    string accountNumber = dgwMembers.SelectedRows[0].Cells["AccountNumber"].Value.ToString();

                    // Debugging message to show the selected account number
                    MessageBox.Show("Selected Member's Account Number: " + accountNumber,
                                    "Debugging Info",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    // Show confirmation dialog before deleting
                    DialogResult result = MessageBox.Show($"Are you sure you want to delete the member with Account Number '{accountNumber}'?",
                                                          "Confirm Deletion",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        string query = "DELETE FROM Members WHERE AccountNumber=@AccountNumber";

                        // Open database connection
                        conn.Open();

                        // Create command and add parameters
                        using (cmd = new OleDbCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
                            cmd.ExecuteNonQuery();
                        }

                        // Close database connection
                        conn.Close();

                        // Show success message
                        MessageBox.Show("Member deleted successfully.",
                                        "Success",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                        // Refresh the DataGridView
                        GetMembers();
                    }
                    // If No is clicked, do nothing
                }
                else
                {
                    MessageBox.Show("Please select a member to delete.",
                                    "No Selection",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        { 
        
        }

            

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }



        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMiddleName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNationality_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtHomeAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpBirthdate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpDateBecomeMember_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgwMembers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}



