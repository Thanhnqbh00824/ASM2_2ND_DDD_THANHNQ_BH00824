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

namespace WindowsFormsApp1
{
    public partial class Attendance_Record : Form
    {
        string connectionString = @"Data Source = ADMIN; Initial Catalog = Attendance_System; Integrated Security = True; Encrypt = False";
        DataTable dt = new DataTable();
        private int currentRowIndex;
        SqlConnection conn;
        public Attendance_Record()
        {
            InitializeComponent();

        }

        private void Attendance_Record_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            GetData();
        }
        private void GetData()
        {
            string connectionString = @"Data Source = ADMIN; Initial Catalog = Attendance_System; Integrated Security = True; Encrypt = False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "Attendence";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void btnpresent_Click(object sender, EventArgs e)
        {
            DataTable filteredTable = ((DataTable)dataGridView1.DataSource).Clone();
            foreach (DataRow row in ((DataTable)dataGridView1.DataSource).Rows)
            {
                if (row["attendance_status"].ToString() == "present")
                {
                    filteredTable.ImportRow(row);
                }
            }

            dataGridView1.DataSource = filteredTable;
        }

        private void btnabsent_Click(object sender, EventArgs e)
        {
            {
                DataTable filteredTable = ((DataTable)dataGridView1.DataSource).Clone();
                foreach (DataRow row in ((DataTable)dataGridView1.DataSource).Rows)
                {
                    if (row["attendance_status"].ToString() == "absent")
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                dataGridView1.DataSource = filteredTable;
            }

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source = ADMIN; Initial Catalog = Attendance_System; Integrated Security = True; Encrypt = False";
            int recordId = Convert.ToInt32(txtrecordID.Text);
            int studentId = Convert.ToInt32(txtstudentID.Text);
            string studentName = txtstudentname.Text;
            int sessionId = Convert.ToInt32(txtsessionID.Text);
            string attendanceStatus = cbbstatus.Text;

            string query = "AddStudent";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@record_id", SqlDbType.Int).Value = recordId;
                    command.Parameters.Add("@student_id", SqlDbType.Int).Value = studentId;
                    command.Parameters.Add("@student_name", SqlDbType.NVarChar, 100).Value = studentName;
                    command.Parameters.Add("@session_id", SqlDbType.Int).Value = sessionId;
                    command.Parameters.Add("@attendance_status", SqlDbType.NVarChar, 50).Value = attendanceStatus;

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show("The student has been added successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while adding students: " + ex.Message);
                    }
                }
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void BtnSearch_Click_1(object sender, EventArgs e)
        {
            {

            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string studentName = txtstudentname.Text; 
            string status = cbbstatus.Text; 

            string connectionString = @"Data Source=ADMIN;Initial Catalog=Attendance_System;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("UpdateStudent", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@attendance_status", status);
                    cmd.Parameters.AddWithValue("@Student_name", studentName);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update successful");
                    RefreshStudentList();
                }
            }

        }
        private void RefreshStudentList()
        {

            string connectionString = @"Data Source=ADMIN;Initial Catalog=Attendance_System;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("Attendence", connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        string studentID = reader["Student_ID"].ToString();
                        string className = reader["attendance_status"].ToString();

                    }
                    reader.Close();
                }
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

            txtrecordID.Text = selectedRow.Cells["Record_ID"].Value.ToString();
            txtsessionID.Text = selectedRow.Cells["Session_ID"].Value.ToString();
            txtstudentID.Text = selectedRow.Cells["Student_ID"].Value.ToString();
            txtstudentname.Text = selectedRow.Cells["student_name"].Value.ToString();
            cbbstatus.Text = selectedRow.Cells["attendance_status"].Value.ToString();


            currentRowIndex = e.RowIndex;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source = ADMIN; Initial Catalog = Attendance_System; Integrated Security = True; Encrypt = False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "Attendence";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }

            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            Main Main = new Main();
            Main.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                string recordID = selectedRow.Cells["Record_ID"].Value.ToString();
                string sessionID = selectedRow.Cells["Session_ID"].Value.ToString();
                string studentID = selectedRow.Cells["Student_ID"].Value.ToString();
                string studentname = selectedRow.Cells["student_name"].Value.ToString();
                string attendancestatus = selectedRow.Cells["attendance_status"].Value.ToString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("DeleteStudent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@record_id", SqlDbType.VarChar, 100).Value = recordID;
                        command.Parameters.Add("@student_id", SqlDbType.VarChar, 100).Value = studentID;
                        command.Parameters.Add("@session_id", SqlDbType.VarChar, 100).Value = sessionID;
                        command.Parameters.Add("@student_name", SqlDbType.VarChar, 100).Value = studentname;
                        command.Parameters.Add("@attendance_status", SqlDbType.VarChar, 100).Value = attendancestatus;

                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine("Number of rows deleted: " + rowsAffected);


                        dataGridView1.Rows.Remove(selectedRow);
                    }
                }

            }
        }
    }
}

    
       
