using EatNow.Models;
using System.Data.SqlClient;

namespace EatNow.DAL
{
    public class EmpleadoDAL
    {
        public readonly string connectionString;
        
        public EmpleadoDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Empleado GetEmployeeByEmailPassword(string email, string password)
        {
            Empleado empleado = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT IdEmpleado, DNI, Nombre, Apellidos, CorreoElectronico, Password, RIdRestaurante " +
                               "FROM Empleado WHERE CorreoElectronico = @Email AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            empleado = new Empleado
                            {
                                IdEmpleado = int.Parse(reader["IdEmpleado"].ToString()),
                                DNI = reader["DNI"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                CorreoElectronico = reader["CorreoElectronico"].ToString(),
                                Password = reader["Password"].ToString(),
                                RIdRestaurante = int.Parse(reader["RIdRestaurante"].ToString())
                            };
                        }
                    }
                }
            }

            return empleado;
        }

        public Empleado GetEmployeeById(int idEmployee)
        {
            Empleado empleado = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT IdEmpleado, DNI, Nombre, Apellidos, CorreoElectronico, Password, RIdRestaurante " +
                               "FROM Empleado WHERE IdEmpleado = @idEmployee";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idEmployee", idEmployee);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            empleado = new Empleado
                            {
                                IdEmpleado = int.Parse(reader["IdEmpleado"].ToString()),
                                DNI = reader["DNI"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                CorreoElectronico = reader["CorreoElectronico"].ToString(),
                                Password = reader["Password"].ToString(),
                                RIdRestaurante = int.Parse(reader["RIdRestaurante"].ToString())
                            };
                        }
                    }
                }
            }

            return empleado;
        }
    }
}
