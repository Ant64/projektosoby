using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjektOsoby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektOsoby.DAL
{
    public class MyInitializer
        : System.Data.Entity.DropCreateDatabaseIfModelChanges<MPSContext>


    {
        protected override void Seed(MPSContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new Microsoft.AspNet.Identity.UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            //uzytkownicy o roli Admin
            roleManager.Create(new IdentityRole("Admin"));
            var u1 = new ApplicationUser
            {
                Email = "admin@biblioteka.com",
                UserName = "admin@biblioteka.com"
            };
            string passwor = "Biblioteka1@";
            userManager.Create(u1, passwor);
            userManager.AddToRole(u1.Id, "Admin");

            var uu1 = new UserNew
            {
                UserName = "admin@biblioteka.com",
                Id = u1.Id

            };
            context.User.Add(uu1);
            context.SaveChanges();

         /*   var P = new Person
            {
                Name = "",
                Surname = ""

            };*/
           // context.Person.Add(P);
         //   context.SaveChanges();
        }
    }
}
