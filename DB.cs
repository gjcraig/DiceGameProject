using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Dice_Game
{
    class DB
    {
        public static void WriteScore(string name, int score)
        {
            SqlConnection oConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Dice;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            oConnection.Open();

            SqlCommand oCommand = new SqlCommand("INSERT INTO HighScores ([Name],[Score]) VALUES(@Name,@Score)", oConnection);
            oCommand.Parameters.Add("@Name", SqlDbType.VarChar, 50);
            oCommand.Parameters.Add("@Score", SqlDbType.Int);

            oCommand.Parameters["@name"].Value = name;
            oCommand.Parameters["@score"].Value = score;
            oCommand.ExecuteNonQuery();

            oConnection.Close();

        }

        public static DataTable ReadScore()
        {
            SqlConnection oConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Dice;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            oConnection.Open();

            SqlCommand oCommand = new SqlCommand("SELECT TOP 10 * FROM HighScores Order By Score DESC ", oConnection);


            SqlDataAdapter oDA = new SqlDataAdapter(oCommand);
            DataSet ds = new DataSet();
            oDA.Fill(ds);

            oConnection.Close();

            Console.WriteLine("Top 10 High Scores");
            Console.WriteLine();
            
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Name        |  Score");
            Console.WriteLine("-----------------------------------");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Console.WriteLine(String.Format("{0,-10}  |  {1,5}", dr["Name"], dr["Score"]));
            }
            Console.WriteLine("-----------------------------------");

            //md5 hash
            return ds.Tables[0];
        }


        public static DataTable GetUsers()
        {
            SqlConnection oConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Login;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            oConnection.Open();
            try
            {
                
                SqlCommand oCommand = new SqlCommand("SELECT [User] FROM Login ", oConnection);
                SqlDataAdapter oDA = new SqlDataAdapter(oCommand);
                DataSet ds = new DataSet();
                oDA.Fill(ds);

                return ds.Tables[0];
   
            }
            finally
            {
                oConnection.Close();
            }
        }

        public static void NewUser(string user, string password)
        {
           
            SqlConnection oConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Login;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            oConnection.Open();
            try
            {
                SqlCommand oCommand = new SqlCommand("INSERT INTO Login ([User],[Password]) VALUES(@User, @Password)", oConnection);
                oCommand.Parameters.AddWithValue("@User", user);
                oCommand.Parameters.AddWithValue("@Password", password);


                oCommand.ExecuteNonQuery();

            }
           
            finally
            {
                oConnection.Close();
            }
        }

        public static string GetHash(string UserName)
        {
            SqlConnection oConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Login;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            oConnection.Open();
            string UserPassword;

            try
            {

                SqlCommand oCommand = new SqlCommand("SELECT Password FROM Login WHERE [User]=@name  ", oConnection);
                oCommand.Parameters.Add("@name", SqlDbType.VarChar);
                oCommand.Parameters["@name"].Value = UserName;
                UserPassword = (String)oCommand.ExecuteScalar();
                return UserPassword;
            }

            finally
            {
                oConnection.Close();
            }
        }
    }
}
