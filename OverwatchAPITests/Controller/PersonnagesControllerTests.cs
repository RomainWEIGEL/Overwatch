using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OverwatchAPI.Controller;
using OverwatchAPI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OverwatchAPI.Controller.Tests
{



    [TestClass()]
    public class PersonnagesControllerTests
    {

        private PersonnagesController _controller;

        private MapDBContext _context;

        public PersonnagesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<MapDBContext>().UseNpgsql("Server=51.83.36.122;port=5432;Database=vcout2; uid=vcout; password=LesMacaronsCBon!");
            _context = new MapDBContext(builder.Options);
            _controller = new PersonnagesController(_context);
        }

        [TestMethod()]
        public void Postutilisateur_ModelValidated_CreationOK()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du

            Personnage userAtester = new Personnage()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Pays = "France",
                Note = 2
            };
            // Act
            var result = _controller.PostSerie(userAtester.Nom, userAtester.Prenom, userAtester.Pays, userAtester.Note).Result; // .Result pour appeler la méthode async de manière synchrone, afin


            var listutilisateur = _context.Personnages.ToList();
            Personnage? userRecupere = new Personnage();

            foreach (Personnage ut in listutilisateur)
            {
                if (ut.Nom == userAtester.Nom)
                {
                   userRecupere = ut;
                }
            }
            

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            userAtester.PersonnageId = userRecupere.PersonnageId;
            Assert.AreEqual(userAtester, userRecupere, "Utilisateurs pas identiques");
        }
    }
}