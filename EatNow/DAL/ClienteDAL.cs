using EatNow.Models;
using System.Data.SqlClient;

namespace EatNow.DAL
{
    public class ClienteDAL
    {
        public readonly string connectionString;

        public ClienteDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Cliente GetClientByEmailPassword(string email, string password)
        {
            Cliente cliente = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT IdCliente, CorreoElectronico, Password, Nombre, Apellidos, Telefono, URLFoto " +
                               "FROM Cliente WHERE CorreoElectronico = @Email AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("password", password);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                IdCliente = int.Parse(reader["IdCliente"].ToString()),
                                CorreoElectronico = reader["CorreoElectronico"].ToString(),
                                Password = reader["Password"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                URLFoto = (reader["URLFoto"] != DBNull.Value) ? reader["URLFoto"].ToString() : null
                            };
                        }
                    }
                }
            }

            return cliente;
        }
    }
}
