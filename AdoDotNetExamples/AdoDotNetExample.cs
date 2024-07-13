using AdoDotNetExample.Shared;

namespace AdoDotNetExample.AdoDotNetExamples;

public class AdoDotNetExample
{

    #region Run

    public void Run()
    {
        //Create("T", "T", "t");
        //Update(1, "Blog", "blog", "Blog");
        Read();
        ////ReadOnlyBlog(1);
        //ReadOnlyBlog(1);
        //Delete(1);
        //ReadOnlyBlog(1);
    }

    #endregion

    #region Read

    public void Read()
    {
        SqlConnection connection = new SqlConnection(Connection.connectionString.ConnectionString);

        connection.Open();

        string query = "select * from Tbl_Blog";
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sqlDataAdapter.Fill(dt);

        connection.Close();

        foreach (DataRow dr in dt.Rows)
        {
            Console.WriteLine("BlogId => " + dr["BlogId"]);
            Console.WriteLine("BlogTitle => " + dr["BlogTitle"]);
            Console.WriteLine("BlogAuthor => " + dr["BlogAuthor"]);
            Console.WriteLine("BlogContent => " + dr["BlogContent"]);
            Console.WriteLine("---------------------------------------");
        }

    }

    #endregion

    #region CreateBlog

    public void Create(string title, string author, string content)
    {
        SqlConnection connection = new SqlConnection(Connection.connectionString.ConnectionString);
        connection.Open();

        string query = @"INSERT INTO [dbo].[Tbl_Blog]
                            ([BlogTitle],
                            [BlogAuthor],
                            [BlogContent])
                            VALUES
                (@BlogTitle,
                @BlogAuthor,
                @BlogContent)";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@BlogTitle", title);
        command.Parameters.AddWithValue("@BlogAuthor", author);
        command.Parameters.AddWithValue("@BlogContent", content);
        int result = command.ExecuteNonQuery();

        string message = result > 0 ? "Create Blog Success" : "Create Blog Fail";
        Console.WriteLine(message);
    }

    #endregion

    #region ReadOnlyBlog Edit

    public void ReadOnlyBlog(int blogId)
    {
        try
        {
            if (blogId <= 0)
            {
                Console.WriteLine("Required blogid");
                return;
            }
            SqlConnection sqlConnection = new SqlConnection(Connection.connectionString.ConnectionString);
            sqlConnection.Open();

            string query = "select * from Tbl_Blog where BlogId = @BlogId";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogId", blogId);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No blog not found");
                return;
            }
            DataRow dr = dt.Rows[0];
            Console.WriteLine("BlogTitle is " + dr["BlogTitle"]);
            Console.WriteLine("BlogAuthor is " + dr["BlogAuthor"]);
            Console.WriteLine("BlogContent is " + dr["BlogContent"]);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return;
        }
    }

    #endregion

    #region Update

    private void Update(int id, string title, string author, string content)
    {
        SqlConnection connection = new SqlConnection(Connection.connectionString.ConnectionString);
        connection.Open();

        string query = @"UPDATE [dbo].[Tbl_Blog]
                            SET [BlogTitle] = @BlogTitle
                            ,[BlogAuthor] = @BlogAuthor
                            ,[BlogContent] = @BlogContent
                            WHERE BlogId=@BlogId";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        cmd.Parameters.AddWithValue("@BlogTitle", title);
        cmd.Parameters.AddWithValue("@BlogAuthor", author);
        cmd.Parameters.AddWithValue("@BlogContent", content);
        int result = cmd.ExecuteNonQuery();
        connection.Close();

        string message = result > 0 ? "Blog update successful" : "Blog update fail";
        Console.WriteLine(message);
    }

    #endregion

    #region Delete

    public void Delete(int blogId)
    {
        if (blogId <= 0)
        {
            Console.WriteLine("Required blogid");
            return;
        }
        SqlConnection sqlConnection = new SqlConnection(Connection.connectionString.ConnectionString);
        sqlConnection.Open();
        string query = @"DELETE FROM [dbo].[Tbl_Blog]
                          WHERE BlogId=@BlogId";
        SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
        sqlCommand.Parameters.AddWithValue("@BlogId", blogId);
        int result = sqlCommand.ExecuteNonQuery();
        sqlConnection.Close();

        string message = result > 0 ? "Blog delete successful" : "Blog delete fail";
        Console.WriteLine(message);
    }

    #endregion

}