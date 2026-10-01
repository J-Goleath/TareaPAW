namespace PAW.Models;

public partial class ProductDbContext
{
    public static string ConnectionString { get; set; } =
        "Server=localhost\\SQLEXPRESS;Database=ProductDB;Trusted_Connection=True;TrustServerCertificate=True;";
}
