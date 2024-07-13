namespace AdoDotNetExample;

public class AdoDotNetExample
{
    #region Connection

    private readonly SqlConnectionStringBuilder _stringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = ".",
        InitialCatalog = "MTKDotNetCore",
        UserID = "sa",
        Password = "sasa@123",
        TrustServerCertificate = true
    };

    #endregion

    public void Run()
    {
        Read();
    }

    public void Read()
    {
        SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);

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
}