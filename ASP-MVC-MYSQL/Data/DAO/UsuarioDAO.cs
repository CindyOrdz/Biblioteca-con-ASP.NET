using ASP_MVC_MYSQL.Models;
using MySql.Data.MySqlClient;

namespace ASP_MVC_MYSQL.Data.DAO
{
    public class UsuarioDAO
    {
        //Dependencia del servicio MySqlConnection que se registró en program
        private readonly MySqlConnection _connection;

        public UsuarioDAO(MySqlConnection connection)
        {
            _connection = connection;
        }

      
        public async Task<List<Usuario>> Listar()
        {
            string query = "SELECT * FROM usuario";

            List<Usuario> Usuarios = new List<Usuario>();

            try
            {
                await _connection.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, _connection);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        Usuario MiUsuario = new Usuario();
                        MiUsuario.Correo = rdr.GetString(0);
                        MiUsuario.Contraseña = rdr.GetString(1);
                        MiUsuario.Rol = rdr.GetString(2);
                        Usuarios.Add(MiUsuario);
                    }
                }

                return Usuarios;
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

        //Verifica que el correo existe
        public async Task<Usuario> ExistenciaCorreo(string _correo)
        {
            var usuarios = await Listar();

            return usuarios.FirstOrDefault(item => item.Correo == _correo);
        }

        public async Task Agregar(Usuario usuario)
        {
            string query = "INSERT INTO usuario (correo,contraseña,rol) VALUES (@correo, @clave, @rol)";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@correo", usuario.Correo);
            cmd.Parameters.AddWithValue("@clave", usuario.Contraseña);
            cmd.Parameters.AddWithValue("@rol", usuario.Rol);
            
            try
            {
                await _connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar el usuario: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }



    }
}
