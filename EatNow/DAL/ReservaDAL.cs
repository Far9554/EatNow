﻿using EatNow.Models;
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

        public List<Reserva> GetAllReservasRestauranteId(int Id)
        {
            List<Reserva> reservas = new List<Reserva>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = "SELECT Re.Nombre AS 'Restaurante', Cl.Nombre AS 'Nombre Cliente', Cl.Apellidos AS 'Apellido Cliente', R.Inicio, R.Fin, ER.Nombre AS 'Estado' FROM Reserva R " +
                               "INNER JOIN Casilla C ON C.IdCasilla = R.RIdCasilla " +
                               "INNER JOIN Restaurante Re ON C.RIdRestaurante = Re.IdRestaurante " +
                               "INNER JOIN Cliente Cl ON Cl.IdCliente = R.RIdCliente " +
                               "INNER JOIN EstadoReserva ER ON R.RIdEstadoReserva = ER.IdEstado " +
                               "GROUP BY Re.Nombre, Cl.Nombre, Cl.Apellidos , R.Inicio, R.Fin, ER.Nombre ";

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

    }
}
