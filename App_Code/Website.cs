using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Web.Security;

/// <summary>
/// Class used to carry out global functions for website
/// </summary>
public class Website
{
    public Website()
    {

    }
    //Calculates the number of years between a date entered as a string and today
    //Returns a String
    public static String calculateYearsString(String text)
    {
        try
        {
            DateTime date = Convert.ToDateTime(text);
            int calculatedYears = DateTime.Today.Year - date.Year;
            if (date > DateTime.Today.AddYears(-calculatedYears))
                calculatedYears--;
            return calculatedYears.ToString();

        }
        catch (Exception ex)
        {
            return "";
        }
    }
    //Calculates the number of years between a date entered as a string and today
    //Returns an int
    public static int calculateYearsInt(String text)
    {
        try
        {
            DateTime date = Convert.ToDateTime(text);
            int calculatedYears = DateTime.Today.Year - date.Year;
            if (date > DateTime.Today.AddYears(-calculatedYears))
                calculatedYears--;
            return calculatedYears;

        }
        catch (Exception ex)
        {
            return -1;
        }
    }
    //Ensures that an age is legitimate. Uses calculateYears to determine age,
    //and if less than 0 returns false
    //Takes one string parameter
    public static bool validAge(String text)
    {
        int years = Website.calculateYearsInt(text);
        if (years < 0)
            return false;
        else
            return true;
    }

    public static string getSafeString(SqlDataReader reader, int colIndex)
    {
        if (!reader.IsDBNull(colIndex))
            return reader.GetString(colIndex);
        else
            return string.Empty;
    }

    public static SqlConnection getSQLConnection()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        return conn;
    }
    public static SqlCommand getSQLCommand(SqlConnection conn)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = conn;
        return cmd;
    }

    //Checks the boolean value. Used for dropdownlists whose indexes
    //are yes/no with a valueless first listitem, no as the second item, 
    // and yes as the third item. The appropriated selectedindex is returned
    public static int checkForBooleans(bool flag)
    {
        if (flag == true)
        {
            return 2;
        }
        else if (flag == false)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    //Ensures that 
    public static String checkForInts(int num)
    {
        if (num == -1)
        {
            return "";
        }
        else
        {
            return num.ToString();
        }
    }
}