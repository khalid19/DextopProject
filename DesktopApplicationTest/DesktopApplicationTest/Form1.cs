using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DesktopApplicationTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = " Image Files(*.jpg;*.jpeg;*.gif;*.bmp)|*.jpg;*.jpeg;*.gif;*.bmp";

            if (open.ShowDialog() == DialogResult.OK)
            {

                //pictureBox.ImageLocation = open.FileName;
                pictureBox.ImageLocation = open.FileName;
                pathTextBox.Text = open.FileName;

                pictureBox.ImageLocation = pathTextBox.Text;

            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var connectionString = ConnectionString();


            SqlConnection connection = new SqlConnection(connectionString);

            Person person = new Person();

            person.FirstName = firstNameTextBox.Text;
            person.LastName = lastNameTextBox.Text;
            person.Contact = contactTextBox.Text;
            person.Address = addressTextBox.Text;

            try
            {
                connection.Open();
                string query = "insert into Person values('" + person.FirstName + "','" + person.LastName + "','" +
                               person.Contact + "','" + person.Address + "','" + pathTextBox.Text + "')";
                SqlCommand command = new SqlCommand(query, connection);

                int affectedRow = command.ExecuteNonQuery();

                connection.Close();

                firstNameTextBox.Text = "";
                lastNameTextBox.Text = "";
                contactTextBox.Text = "";
                addressTextBox.Text = "";
                pictureBox.Refresh();

                MessageBox.Show("Data Saved Successfully" + "\n" + affectedRow + " Row Affected");
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
        }

        private static string ConnectionString()
        {
            string connectionString = "server=DESKTOP-30ONMO9;initial catalog=Test1; integrated security=sspi";
            return connectionString;
        }



        private void seachButton_Click(object sender, EventArgs e)
        {
            var connectionString = ConnectionString();

            Person person = new Person();

            person.PersonId = Convert.ToInt32(idTextBox.Text);
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "select * from Person where PersonId='" + person.PersonId + "'";

                SqlCommand command = new SqlCommand(query, connection);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    person.FirstName = reader[1].ToString();
                    person.LastName = reader[2].ToString();
                    person.Contact = reader[3].ToString();
                    person.Address = reader[4].ToString();
                    pathTextBox.Text = (reader[5]).ToString();



                }

                firstNameTextBox.Text = person.FirstName;
                lastNameTextBox.Text = person.LastName;
                contactTextBox.Text = person.Contact;
                addressTextBox.Text = person.Address;
                pictureBox.ImageLocation = pathTextBox.Text;

            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message + "Not working properly");
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            var connectionString = ConnectionString();


            SqlConnection connection = new SqlConnection(connectionString);

            Person person = new Person();
            person.PersonId = Convert.ToInt32(idTextBox.Text);
            person.FirstName = firstNameTextBox.Text;
            person.LastName = lastNameTextBox.Text;
            person.Contact = contactTextBox.Text;
            person.Address = addressTextBox.Text;

            try
            {
                connection.Open();
                //string query = "insert into Person values('" + person.FirstName + "','" + person.LastName + "','" + person.Contact + "','" + person.LastName + "','" + pathTextBox.Text + "')";
                string query = "update Person set FirstName='" + person.FirstName + "',LastName='" + person.LastName +
                               "',Contact='" + person.Contact + "',Address='" + person.Address + "',Photo='" +
                               pathTextBox.Text + "' where PersonId='" + person.PersonId + "' ";
                SqlCommand command = new SqlCommand(query, connection);

                int affectedRow = command.ExecuteNonQuery();

                connection.Close();

                firstNameTextBox.Text = "";
                lastNameTextBox.Text = "";
                contactTextBox.Text = "";
                addressTextBox.Text = "";
                pictureBox.Refresh();

                MessageBox.Show("Data Update Successfully" + "\n" + affectedRow + " Row Affected");
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {

            var connectionString = ConnectionString();


            SqlConnection connection = new SqlConnection(connectionString);

            Person person = new Person();
            person.PersonId = Convert.ToInt32(idTextBox.Text);


            try
            {
                connection.Open();
                //string query = "insert into Person values('" + person.FirstName + "','" + person.LastName + "','" + person.Contact + "','" + person.LastName + "','" + pathTextBox.Text + "')";
                string query = "delete from Person  where PersonId='" + person.PersonId + "' ";
                SqlCommand command = new SqlCommand(query, connection);

                int affectedRow = command.ExecuteNonQuery();

                connection.Close();

                firstNameTextBox.Text = "";
                lastNameTextBox.Text = "";
                contactTextBox.Text = "";
                addressTextBox.Text = "";
                pictureBox.Refresh();

                MessageBox.Show("Data Update Successfully" + "\n" + affectedRow + " Row Affected");
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }

        }

        private void showButton_Click(object sender, EventArgs e)
        {
            var connectionString = ConnectionString();

         

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "select * from Person ";

                List<Person> persons=new List<Person>();

                SqlCommand command = new SqlCommand(query, connection);


                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    Person person = new Person();
                    person.PersonId = Convert.ToInt32(reader[0]);
                    person.FirstName = reader[1].ToString();
                    person.LastName = reader[2].ToString();
                    person.Contact = reader[3].ToString();
                    person.Address = reader[4].ToString();
                    person.Photo = (reader[5]).ToString();

                    persons.Add(person);

                }
                foreach (Person person in persons)
                {
                    ListViewItem items=new ListViewItem();

                    items.Tag = person;
                    items.Text = person.PersonId.ToString();
                    items.SubItems.Add(person.FirstName);
                    items.SubItems.Add(person.LastName);
                    items.SubItems.Add(person.Contact);
                    items.SubItems.Add(person.Address);
                    items.SubItems.Add(person.Photo);
                    showListView.Items.Add(items);




                }

            }
            catch (Exception exception)
            {
                {

                    MessageBox.Show(exception.Message + "Not working properly");
                }


            }




        }
    }
}
