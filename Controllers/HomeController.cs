using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using iztacalco.Models;
using System.Data.SqlClient;

namespace iztacalco.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private string connection = "Server=tcp:registros.database.windows.net,1433;Initial Catalog=Iztacalco;User ID=dbger;Password=S1st3m4sr3l04D1";
        public IActionResult Index()
        {
            var data = new List<Registro>();
            SqlConnection con = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand ("SELECT [Id], [SECTOR], [JSECTOR], [TATENDIDOS], [TNATENDIDOS], [CALLES], [IMAGEN], [OBSERVACIONES] FROM Registros", con);
       try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    data.Add(new Registro
                    {
                        Id = (Guid)dr["Id"],
                        Sector = (string)dr["Sector"],
                        JSector = (string)dr["JSector"],
                        TAtendidos = (string)dr["Tatendidos"],
                        TNAtendidos = (string)dr["TNAtendidos"],
                        Calles = (string)dr["Calles"],
                        Imagen = (string)dr["Imagen"],
                        Observaciones = (string)dr["Observaciones"]
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            return View(data);
        }
        public IActionResult Details(Guid id)
        {
            var data = new Registro();
            var con = new SqlConnection(connection);
            var cmd = new SqlCommand("SELECT [Id], [SECTOR] , [JSECTOR], [TATENDIDOS], [TNATENDIDOS], [CALLES], [IMAGEN], [OBSERVACIONES] FROM [Registros] WHERE [ID] = @i", con);
           
           cmd.Parameters.Add("@i", SqlDbType.UniqueIdentifier).Value = id;
         try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    data.Id = (Guid)dr["Id"];
                    data.Sector = (string)dr["Sector"];
                    data.JSector = (string)dr["JSector"];
                    data.TAtendidos = (string)dr["TAtendidos"];
                    data.TNAtendidos = (string)dr["TNAtendidos"];
                    data.Calles = (string)dr["Calles"];
                    data.Imagen = (string)dr["Imagen"];
                    data.Observaciones = (string)dr["Observaciones"];
                }
                return PartialView(data);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Registro data)
        {
            var con = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [Registros] ([Id],[SECTOR],[JSECTOR],[TATENDIDOS],[TNATENDIDOS],[CALLES],[IMAGEN],[OBSERVACIONES]) VALUES (NEWID(), @s, @j, @ta, @tna,@c,@im,@o);", con);


            cmd.Parameters.Add("@s", SqlDbType.NVarChar, 10).Value = data.Sector;
            cmd.Parameters.Add("@j", SqlDbType.NVarChar, 100).Value = data.JSector;
            cmd.Parameters.Add("@ta", SqlDbType.NVarChar, 50).Value = data.TAtendidos;
            cmd.Parameters.Add("@tna", SqlDbType.NVarChar, 50).Value = data.TNAtendidos;
            cmd.Parameters.Add("@c", SqlDbType.NVarChar, 1000).Value = data.Calles;
            cmd.Parameters.Add("@im", SqlDbType.VarChar, 1000).Value = data.Imagen;
            cmd.Parameters.Add("@o", SqlDbType.NVarChar, 2500).Value = data.Observaciones;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public IActionResult Edit(Guid id)
        {
            var data = new Registro();
            var con = new SqlConnection(connection);
            var cmd = new SqlCommand("SELECT [Id], [SECTOR], [JSECTOR], [TATENDIDOS], [TNATENDIDOS], [CALLES], [IMAGEN], [OBSERVACIONES] FROM [Registros] WHERE [Id] = @i", con);

            cmd.Parameters.Add("@i", SqlDbType.UniqueIdentifier).Value = id;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    data.Id = (Guid)dr["Id"];
                    data.Sector = (string)dr["Sector"];
                    data.JSector = (string)dr["JSector"];
                    data.TAtendidos = (string)dr["TAtendidos"];
                    data.TNAtendidos = (string)dr["TNAtendidos"];
                    data.Calles = (string)dr["Calles"];
                    data.Imagen = (string)dr["Imagen"];
                    data.Observaciones = (string)dr["Observaciones"];

                }
                return View(data);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Registro data)
        {
            var con = new SqlConnection(connection);
            var cmd = new SqlCommand(@"UPDATE [Registros] SET [SECTOR] = @s, [JSECTOR] = @j, [TATENDIDOS] = @ta, [TNATENDIDOS] = @tna, [CALLES] = @c, [IMAGEN] = @im, [OBSERVACIONES] = @o	WHERE [Id] = @i;", con);

            cmd.Parameters.Add("@i", SqlDbType.UniqueIdentifier).Value = data.Id;
            cmd.Parameters.Add("@s", SqlDbType.NVarChar, 10).Value = data.Sector;
            cmd.Parameters.Add("@j", SqlDbType.NVarChar, 100).Value = data.JSector;
            cmd.Parameters.Add("@ta", SqlDbType.NVarChar, 50).Value = data.TAtendidos;
            cmd.Parameters.Add("@tna", SqlDbType.NVarChar, 50).Value = data.TNAtendidos;
            cmd.Parameters.Add("@c", SqlDbType.NVarChar, 1000).Value = data.Calles;
            cmd.Parameters.Add("@im", SqlDbType.VarChar, 1000).Value = data.Imagen;
            cmd.Parameters.Add("@o", SqlDbType.NVarChar, 2500).Value = data.Observaciones;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
    /*
         public IActionResult Delete(Guid id)
        {
            var con = new SqlConnection(connection);
            var cmd = new SqlCommand("DELETE FROM [Registros] WHERE [Id] = @i", con);

            cmd.Parameters.Add("@i", SqlDbType.UniqueIdentifier).Value = id;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        */

        public IActionResult Privacy()
        {
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
