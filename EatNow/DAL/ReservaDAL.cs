using EatNow.Models;
using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace EatNow.DAL
{
    public class ReservaDAL
    {
        private readonly string connectionString;

        public ReservaDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Reserva> GetAllReservasRestauranteId(int idRestaurant)
        {
            List<Reserva> reservas = new List<Reserva>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = "SELECT IdReserva, Re.Nombre AS 'Restaurante', C.NumeroMesa AS 'NumeroMesa', Cl.Nombre AS 'Nombre Cliente', Cl.Apellidos AS 'Apellido Cliente', R.Inicio, R.Fin, RIdCliente, RIdEstadoReserva, RIdCasilla, ER.Nombre AS 'Estado' FROM Reserva R " +
                                "INNER JOIN Casilla C ON C.IdCasilla = R.RIdCasilla " +
                                "INNER JOIN Restaurante Re ON C.RIdRestaurante = Re.IdRestaurante " +
                                "INNER JOIN Cliente Cl ON Cl.IdCliente = R.RIdCliente " +
                                "INNER JOIN EstadoReserva ER ON R.RIdEstadoReserva = ER.IdEstado " +
                                "WHERE Re.IdRestaurante = @IdRestaurante";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdRestaurante", idRestaurant);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Reserva reserva = new Reserva
                            {
                                IdReserva = int.Parse(reader["IdReserva"].ToString()),
                                Inicio = Convert.ToDateTime(reader["Inicio"]),
                                Fin = Convert.ToDateTime(reader["Fin"]),
                                RIdCliente = int.Parse(reader["RIdCliente"].ToString()),
                                RIdEstadoReserva = int.Parse(reader["RIdEstadoReserva"].ToString()),
                                RIdCasilla = int.Parse(reader["RIdCasilla"].ToString()),
                                NumeroMesa = int.Parse(reader["NumeroMesa"].ToString()),
                                NombreRestaurante = reader["Restaurante"].ToString(),
                                NombreCliente = reader["Nombre Cliente"].ToString(),
                                ApellidoCliente = reader["Apellido Cliente"].ToString(),
                                EstadoReservaNombre = reader["Estado"].ToString()
                            };
                            reservas.Add(reserva);
                        }
                    }
                }
            }

            return reservas;
        }

        public List<Reserva> ListReservasUsuario(int Id)
        {
            List<Reserva> reservas = new List<Reserva>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = "SELECT IdReserva, Re.Nombre AS 'Restaurante', C.NumeroMesa AS 'NumeroMesa', Cl.Nombre AS 'Nombre Cliente', Cl.Apellidos AS 'Apellido Cliente', R.Inicio, R.Fin, RIdCliente, RIdEstadoReserva, RIdCasilla, ER.Nombre AS 'Estado', C.RIdRestaurante FROM Reserva R " +
                                "INNER JOIN Casilla C ON C.IdCasilla = R.RIdCasilla " +
                                "INNER JOIN Restaurante Re ON C.RIdRestaurante = Re.IdRestaurante " +
                                "INNER JOIN Cliente Cl ON Cl.IdCliente = R.RIdCliente " +
                                "INNER JOIN EstadoReserva ER ON R.RIdEstadoReserva = ER.IdEstado " +
                                "WHERE Cl.IdCliente = '1'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Reserva reserva = new Reserva
                            {
                                IdReserva = int.Parse(reader["IdReserva"].ToString()),
                                Inicio = Convert.ToDateTime(reader["Inicio"]),
                                Fin = Convert.ToDateTime(reader["Fin"]),
                                RIdCliente = int.Parse(reader["RIdCliente"].ToString()),
                                RIdEstadoReserva = int.Parse(reader["RIdEstadoReserva"].ToString()),
                                RIdCasilla = int.Parse(reader["RIdCasilla"].ToString()),
                                NumeroMesa = int.Parse(reader["NumeroMesa"].ToString()),
                                NombreRestaurante = reader["Restaurante"].ToString(),
                                NombreCliente = reader["Nombre Cliente"].ToString(),
                                ApellidoCliente = reader["Apellido Cliente"].ToString(),
                                EstadoReservaNombre = reader["Estado"].ToString()
                            };
                            reservas.Add(reserva);
                        }
                    }
                }
            }

            return reservas;
        }

        public bool? IsMesaReservedAt(int idCasilla, string hour) {
            DateTime currentTime = DateTime.Now;
            string dateTime = currentTime.ToString("yyyy-MM-dd");
            dateTime += " " + hour;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(IdReserva) AS NumReservas " +
                               "FROM Reserva WHERE Inicio BETWEEN CAST(@DateTime AS DATETIME) AND " +
                               "DATEADD(HOUR, 2, CAST(@DateTime AS DATETIME)) AND " + 
                               "RIdCasilla = @IdCasilla AND RIdEstadoReserva = 1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCasilla", idCasilla);
                    command.Parameters.AddWithValue("@DateTime", dateTime);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int numReservas = int.Parse(reader["NumReservas"].ToString());

                            if (numReservas > 0)
                                return true;
                            else
                                return false;
                        }
                    }
                }
            }

            return null;
        }

        public int GetNumberOfBookingsFromRestaurantAt(int idRestaurante, string hour)
        {
            int numReservas = -1;

            DateTime currentTime = DateTime.Now;
            string dateTime = currentTime.ToString("yyyy-MM-dd");
            dateTime += " " + hour;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(IdReserva) AS NumReservas " +
                               "FROM Reserva INNER JOIN Casilla ON IdCasilla = RIdCasilla " + 
                               "WHERE Inicio = CAST(@DateTime AS DATETIME) AND " +
                               "RIdRestaurante = @IdRestaurante AND RIdEstadoReserva = 1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdRestaurante", idRestaurante);
                    command.Parameters.AddWithValue("@DateTime", dateTime);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            numReservas = int.Parse(reader["NumReservas"].ToString());
                            return numReservas;
                        }
                    }
                }
            }

            return numReservas;
        }


        public List<Reserva> LastFiveReservation(int idCliente)
        {
            List<Reserva> reservas = new List<Reserva>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = "SELECT TOP 5 Re.Nombre AS NombreRestaurante, C.RIdRestaurante AS RIdRestaurante FROM Reserva R " +
                               "INNER JOIN Casilla C ON C.IdCasilla = R.RIdCasilla " +
                               "INNER JOIN Restaurante Re ON C.RIdRestaurante = Re.IdRestaurante " +
                               "WHERE R.RIdCliente = @IdCliente " +
                               "ORDER BY R.IdReserva DESC;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCliente", idCliente);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Reserva reserva = new Reserva
                            {
                                NombreRestaurante = reader["NombreRestaurante"].ToString(),
                                RIdRestaurante = int.Parse(reader["RIdRestaurante"].ToString())
                            };
                            reservas.Add(reserva);
                        }
                    }
                }
            }

            return reservas;
        }
    }
}
