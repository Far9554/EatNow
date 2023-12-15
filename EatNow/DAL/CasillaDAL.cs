using EatNow.Models;
using System.Data.SqlClient;

namespace EatNow.DAL
{
    public class CasillaDAL
    {
        public readonly string connectionString;

        public CasillaDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int TransactionInsertCasillas(List<Casilla> casillas)
        {
            int affectedRows = 0;

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                // Start a local transaction.
                SqlTransaction transaction = connection.BeginTransaction();            

                try
                {
                    // Bucle para insertar todas las casillas
                    foreach(Casilla casilla in casillas)
                    {
                        string query = "INSERT INTO Casilla (X, Y, EsMesa, EstaOcupada, RIdRestaurante) " +
                               "VALUES (@X, @Y, @EsMesa, false, @IdRestaurante)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Transaction = transaction;

                            command.Parameters.AddWithValue("@X", casilla.X);
                            command.Parameters.AddWithValue("@Y", casilla.Y);
                            command.Parameters.AddWithValue("@EsMesa", casilla.EsMesa);
                            command.Parameters.AddWithValue("@IdRestaurante", casilla.RIdRestaurante);

                            affectedRows += command.ExecuteNonQuery();
                        }
                    }

                    // Commit the transaction.
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Handle the exception if the transaction fails to commit.
                    Console.WriteLine(ex.Message);

                    try
                    {
                        // Attempt to roll back the transaction.
                        transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        // Throws an InvalidOperationException if the connection
                        // is closed or the transaction has already been rolled
                        // back on the server.
                        Console.WriteLine(exRollback.Message);
                    }

                    affectedRows = -1;
                }
            }

            return affectedRows;
        }

        public List<Casilla> GetCasillasByRestaurantId(int idRestaurant)
        {
            List<Casilla> casillas = new List<Casilla>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT IdCasilla, X, Y, NumeroMesa, EsMesa, RIdRestaurante " +
                               "FROM Casilla WHERE RIdRestaurante = @IdRestaurante";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdRestaurante", idRestaurant);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Casilla casilla = new Casilla
                            {
                                IdCasilla = int.Parse(reader["IdCasilla"].ToString()),
                                X = int.Parse(reader["X"].ToString()),
                                Y = int.Parse(reader["Y"].ToString()),
                                NumeroMesa = int.Parse(reader["NumeroMesa"].ToString()),
                                EsMesa = (bool) reader["EsMesa"],
                                RIdRestaurante = int.Parse("RIdRestaurante")
                            };
                            casillas.Add(casilla);
                        }
                    }
                }
            }

            return casillas;
        }
    }
}
