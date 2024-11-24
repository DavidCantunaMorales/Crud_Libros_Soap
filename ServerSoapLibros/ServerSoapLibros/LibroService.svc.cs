using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServerSoapLibros
{
    public class Service1 : ILibroService
    {
        private readonly string connectionString = "Server=localhost;Database=Biblioteca;User Id=sa;Password=YourStrong(!)Password;";

        public Libro GetLibroById(int libroID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Libro WHERE LibroID = @LibroID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LibroID", libroID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Libro
                            {
                                LibroID = (int)reader["LibroID"],
                                Titulo = reader["Titulo"].ToString(),
                                Autor = reader["Autor"].ToString(),
                                Genero = reader["Genero"]?.ToString(),
                                Precio = reader["Precio"] as decimal?
                            };
                        }
                    }
                }
            }
            return null; // Devuelve null si no se encuentra el libro
        }

        public List<Libro> GetAllLibros()
        {
            var libros = new List<Libro>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Libro";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            libros.Add(new Libro
                            {
                                LibroID = (int)reader["LibroID"],
                                Titulo = reader["Titulo"].ToString(),
                                Autor = reader["Autor"].ToString(),
                                Genero = reader["Genero"]?.ToString(),
                                Precio = reader["Precio"] as decimal?
                            });
                        }
                    }
                }
            }
            return libros;
        }

        public bool AddLibro(Libro libro)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Libro (Titulo, Autor, Genero, Precio) VALUES (@Titulo, @Autor, @Genero, @Precio)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                    cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                    cmd.Parameters.AddWithValue("@Genero", libro.Genero ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Precio", libro.Precio ?? (object)DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateLibro(Libro libro)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Libro SET Titulo = @Titulo, Autor = @Autor, Genero = @Genero, Precio = @Precio WHERE LibroID = @LibroID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LibroID", libro.LibroID);
                    cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                    cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                    cmd.Parameters.AddWithValue("@Genero", libro.Genero ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Precio", libro.Precio ?? (object)DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteLibro(int libroID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Libro WHERE LibroID = @LibroID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LibroID", libroID);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
