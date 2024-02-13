using ASP_MVC_MYSQL.Models;
using MySql.Data.MySqlClient;

namespace ASP_MVC_MYSQL.Data.DAO
{
    public class LibroDAO
    {
        //Dependencia del servicio MySqlConnection que se registró en program
        private readonly MySqlConnection _connection;


        public LibroDAO(MySqlConnection connection)
        {
            _connection = connection;
        }


        public async Task<List<Libro>> Listar()
        {
            string query = "SELECT * FROM libro";

            List<Libro> libros = new List<Libro>();

            try
            {
                await _connection.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, _connection);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        Libro MiLibro = new Libro();
                        MiLibro.Id = rdr.GetInt32(0);
                        MiLibro.Titulo = rdr.GetString(1);
                        MiLibro.Autor = rdr.GetString(2);
                        MiLibro.NumPag = rdr.GetInt32(3);
                        libros.Add(MiLibro);
                    }
                }

                return libros;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }



        public async Task Agregar(Libro libro)
        {
            string query = "INSERT INTO libro (libro,titulo, autor, numpag) VALUES (@id,@titulo, @autor, @numpag)";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@id", libro.Id);
            cmd.Parameters.AddWithValue("@titulo", libro.Titulo);
            cmd.Parameters.AddWithValue("@autor", libro.Autor);
            cmd.Parameters.AddWithValue("@numpag", libro.NumPag);
            try
            {
                await _connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar el libro: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task Borrar(int id)
        {
            string query = "DELETE FROM libro WHERE libro = @Id";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", id);
            try
            {
                await _connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al borrar el libro: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task Editar(Libro libro)
        {
            string query = "UPDATE libro SET titulo = @titulo, autor = @autor , numpag = @numpag WHERE libro = @Id";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@id", libro.Id);
            cmd.Parameters.AddWithValue("@titulo", libro.Titulo);
            cmd.Parameters.AddWithValue("@autor", libro.Autor);
            cmd.Parameters.AddWithValue("@numpag", libro.NumPag);
            try
            {
                await _connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al editar el libro: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

    }
}
