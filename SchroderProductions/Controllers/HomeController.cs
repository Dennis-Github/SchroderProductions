﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchroderProductions.Models;
using MySql.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SchroderProductions.Controllers.Database;

namespace SchroderProductions.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // stel in waar de database gevonden kan worden
        string connectionString = "Server=172.16.160.21;Port=3306;Database=110062;Uid=110062;Pwd=Dennisenleon!;";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // alle namen ophalen
            var products = GetFestivals();

            // stop de namen in de html
            return View(products);
        }
        [Route("festival/{id}")]
        public IActionResult FestivalDetail(string id)
        {
            // alle namen ophalen
            var festival = GetFestival(id);

            // stop de namen in de html
            return View(festival);
        }
        public List<Festival> GetFestivals()
        {
            // stel in waar de database gevonden kan worden
            string connectionString = "Server=172.16.160.21;Port=3306;Database=110062;Uid=110062;Pwd=Dennisenleon!;";

            // maak een lege lijst waar we de namen in gaan opslaan
            List<Festival> products = new List<Festival>();

            // verbinding maken met de database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // verbinding openen
                conn.Open();

                // SQL query die we willen uitvoeren
                MySqlCommand cmd = new MySqlCommand("select * from festival", conn);

                // resultaat van de query lezen
                using (var reader = cmd.ExecuteReader())
                {
                    // elke keer een regel (of eigenlijk: database rij) lezen
                    while (reader.Read())
                    {
                        Festival p = new Festival
                        {
                            // selecteer de kolommen die je wil lezen. In dit geval kiezen we de kolom "naam"
                            Id = Convert.ToInt32(reader["Id"]),
                            Plaats = reader["Plaats"].ToString(),
                            Naam = reader["Naam"].ToString(),
                            Prijs = Convert.ToInt32(reader["Prijs"]),
                            Afbeelding = reader ["Afbeelding"].ToString(),
                            Logo = reader ["Logo"].ToString(),
                        };

                        // voeg de naam toe aan de lijst met namen
                        products.Add(p);
                    }
                }
            }

            // return de lijst met namen
            return products;
        }

        public Festival GetFestival(string id)
        {
            // maak een lege lijst waar we de namen in gaan opslaan
            List<Festival> products = new List<Festival>();

            // verbinding maken met de database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // verbinding openen
                conn.Open();

                // SQL query die we willen uitvoeren
                MySqlCommand cmd = new MySqlCommand($"select * from festival where id ={id}", conn);

                // resultaat van de query lezen
                using (var reader = cmd.ExecuteReader())
                {
                    // elke keer een regel (of eigenlijk: database rij) lezen
                    while (reader.Read())
                    {
                        Festival p = new Festival
                        {
                            // selecteer de kolommen die je wil lezen. In dit geval kiezen we de kolom "naam"
                            Id = Convert.ToInt32(reader["Id"]),
                            Plaats = reader["Plaats"].ToString(),
                            Naam = reader["Naam"].ToString(),
                            Prijs = Convert.ToInt32(reader["Prijs"]),
                            Afbeelding = reader["Afbeelding"].ToString(),
                            Logo = reader["Logo"].ToString(),
                        };

                        // voeg de naam toe aan de lijst met namen
                        products.Add(p);
                    }
                }
            }

            // return de lijst met namen
            return products[0];
        }
        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [Route("Contact")]
        public IActionResult Contact(Person person)        
        {
            if (ModelState.IsValid)
            {
                SavePerson(person);
                return Redirect("/succes");
            }
           
            return View(person);
        }
        private void SavePerson(Person person)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO klant(voornaam, achternaam, emailadres, bericht, geboortedatum) VALUES(?voornaam, ?achternaam, ?email, ?bericht, ?geboortedatum)", conn);

                cmd.Parameters.Add("?voornaam", MySqlDbType.Text).Value = person.FirstName;
                cmd.Parameters.Add("?achternaam", MySqlDbType.Text).Value = person.LastName;
                cmd.Parameters.Add("?email", MySqlDbType.Text).Value = person.Email;
                cmd.Parameters.Add("?bericht", MySqlDbType.Text).Value = person.Subject;
                cmd.Parameters.Add("?geboortedatum", MySqlDbType.DateTime).Value = person.BirthDate;
                cmd.ExecuteNonQuery();
            }
        }

        [Route("Succes")]
        public IActionResult Succes()
        {
            return View(); 
        }


            [Route("Booking")]
        public IActionResult Booking()
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
