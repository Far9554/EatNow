using EatNow.Models;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace EatNow.DAL
{
    public class RestauranteDAL
    {
        private readonly string connectionString;

        public RestauranteDAL(string connectionString) {
            this.connectionString = connectionString;
        }
        
        public List<Restaurante> GetAllRestaurants()
        {
            List<Restaurante> restaurants = new List<Restaurante>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT IdRestaurante, Nombre, Direccion, Telefono, Web, Descripcion, " +
                               "CONVERT(VARCHAR(5), HoraApertura, 108) AS HoraApertura, " +
                               "CONVERT(VARCHAR(5), HoraCierre, 108) AS HoraCierre FROM Restaurante";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Restaurante restaurant = new Restaurante
                            {
                                IdRestaurante = int.Parse(reader["IdRestaurante"].ToString()),
                                Nombre = reader["Nombre"].ToString(),
                                Direccion = reader["Direccion"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Web = (reader["Web"] != DBNull.Value) ? reader["Web"].ToString() : null,
                                Descripcion = (reader["Descripcion"] != DBNull.Value) ? reader["Web"].ToString() : null,
                                HoraApertura = reader["HoraApertura"].ToString(),
                                HoraCierre = reader["HoraCierre"].ToString()
                            };
                            restaurants.Add(restaurant);
                        }
                    }
                }
            }

            return restaurants;
        }

        public Restaurante GetRestaurantById(int idRestaurant)
        {
            Restaurante restaurant = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT IdRestaurante, Nombre, Direccion, Telefono, Web, Descripcion, " +
                               "CONVERT(VARCHAR(5), HoraApertura, 108) AS HoraApertura, " +
                               "CONVERT(VARCHAR(5), HoraCierre, 108) AS HoraCierre FROM Restaurante WHERE IdRestaurante = @IdRestaurante";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdRestaurante", idRestaurant);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            restaurant = new Restaurante
                            {
                                IdRestaurante = int.Parse(reader["IdRestaurante"].ToString()),
                                Nombre = reader["Nombre"].ToString(),
                                Direccion = reader["Direccion"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Web = (reader["Web"] != DBNull.Value) ? reader["Web"].ToString() : null,
                                Descripcion = (reader["Descripcion"] != DBNull.Value) ? reader["Descripcion"].ToString() : null,
                                HoraApertura = reader["HoraApertura"].ToString(),
                                HoraCierre = reader["HoraCierre"].ToString()
                            };
                        }
                    }
                }
            }

            return restaurant;
        }
    }
}
