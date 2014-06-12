using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

public partial class Wepages_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void UserName_TextChanged(object sender, EventArgs e)
    {
        /*Boolean inUse = false;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        conn.Open();
        SqlCommand cmd = new SqlCommand("select loweredusername from aspnet_Users where loweredusername = @username", conn);
        cmd.Parameters.AddWithValue("@username",CreateUserWizard1.UserName);
        cmd.Connection = conn;
        SqlDataReader reader = new SqlDataReader();
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            if (reader.GetString(4) == CreateUserWizard1.UserName.ToLower())
            {
                
            }
        }*/
    }
}
    /*protected void CreateUser_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtUserName.Text != "" && txtPassword.Text != "" && txtConfirmPassword.Text != ""
                && txtEmail.Text != "" && txtSecretQuestion.Text != "" && txtSecretAnswer.Text != "")
            {
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                    conn.Open();
                    String command = "Insert into Users(username, password, email, secretQuestion, secretAnswer)" +
                        "VALUES(@username, @password, @email, @secretQuestion, @secretAnswer)";
                    SqlCommand cmd = new SqlCommand(command, conn);
                    cmd.Parameters.AddWithValue("@username", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@password", FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1"));
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@secretQuestion", txtSecretQuestion.Text);
                    cmd.Parameters.AddWithValue("@secretAnswer", txtSecretAnswer.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    lblStatus.Text = "Your account has been created.";
                }
                else
                {
                    lblStatus.Text = "Please make sure your passwords are the same.";
                }
            }
            else
            {
                lblStatus.Text = "Please enter a value for each required field";
            }
        }
        catch (SqlException ex)
        {
            Response.Write(ex.ToString());
        }
    }
}*/