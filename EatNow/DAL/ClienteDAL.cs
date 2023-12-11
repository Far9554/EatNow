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
                    command.Parameters.AddWithValue("@Password", password);

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
                                Apellidos = (reader["Apellidos"] != DBNull.Value) ? reader["Apellidos"].ToString() : null,
                                Telefono = reader["Telefono"].ToString(),
                                URLFoto = (reader["URLFoto"] != DBNull.Value) ? reader["URLFoto"].ToString() : null
                            };
                        }
                    }
                }
            }

            return cliente;
        }

        public int InsertClient(Cliente cliente)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Cliente (CorreoElectronico, Password, Nombre, Telefono) " +
                               "VALUES (@CorreoElectronico, @Password, @Nombre, @Telefono)";
                               
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CorreoElectronico", cliente.CorreoElectronico);
                    command.Parameters.AddWithValue("@Password", cliente.Password);
                    command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    command.Parameters.AddWithValue("@Telefono", cliente.Telefono);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
