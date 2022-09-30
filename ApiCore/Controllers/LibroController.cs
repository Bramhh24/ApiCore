using ApiCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ApiCore.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly string connectionString;

        public LibroController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Libro> listaLibros = new List<Libro>();

            try
            {
                using (var conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_lista_libros", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            listaLibros.Add(new Libro()
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                Titulo = rd["Titulo"].ToString(),
                                Descripcion = rd["Descripcion"].ToString(),
                                FechaLanzamiento = Convert.ToDateTime(rd["FechaLanzamiento"]),
                                Autor = rd["Autor"].ToString(),
                                Precio = Convert.ToDouble(rd["Precio"])
                            });
                        }
                    }
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", lista = listaLibros });

            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, lista = listaLibros });
            }
        }


        [HttpGet]
        [Route("Obtener/{idLibro:int}")]
        public IActionResult Obtener(int idLibro)
        {
            List<Libro> listaLibros = new List<Libro>();
            Libro libro = new Libro();

            try
            {
                using (var conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_lista_libros", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            listaLibros.Add(new Libro()
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                Titulo = rd["Titulo"].ToString(),
                                Descripcion = rd["Descripcion"].ToString(),
                                FechaLanzamiento = Convert.ToDateTime(rd["FechaLanzamiento"]),
                                Autor = rd["Autor"].ToString(),
                                Precio = Convert.ToDouble(rd["Precio"])
                            });
                        }
                    }
                }

                libro = listaLibros.Where(x => x.Id == idLibro).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", objeto = libro });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, objeto = libro });
            }
        }

        [HttpPost]
        [Route("Crear")]
        public IActionResult Crear([FromBody] Libro objLibro)
        {
            

            try
            {
                using (var conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_crear_libro", conexion);
                    cmd.Parameters.AddWithValue("titulo", objLibro.Titulo);
                    cmd.Parameters.AddWithValue("descripcion", objLibro.Descripcion);
                    cmd.Parameters.AddWithValue("fechalanzamiento", objLibro.FechaLanzamiento);
                    cmd.Parameters.AddWithValue("autor", objLibro.Autor);
                    cmd.Parameters.AddWithValue("precio", objLibro.Precio);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok"});

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }


        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Libro objLibro)
        {


            try
            {
                using (var conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_editar_libro", conexion);
                    cmd.Parameters.AddWithValue("idLibro", objLibro.Id);
                    cmd.Parameters.AddWithValue("titulo", objLibro.Titulo);
                    cmd.Parameters.AddWithValue("descripcion", objLibro.Descripcion);
                    cmd.Parameters.AddWithValue("fechalanzamiento", objLibro.FechaLanzamiento);
                    cmd.Parameters.AddWithValue("autor", objLibro.Autor);
                    cmd.Parameters.AddWithValue("precio", objLibro.Precio);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Editado" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }


        [HttpDelete]
        [Route("Eliminar/{idLibro:int}")]
        public IActionResult Eliminar(int idLibro)
        {


            try
            {
                using (var conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_eliminar_libro", conexion);
                    cmd.Parameters.AddWithValue("idLibro", idLibro);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Eliminado" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
