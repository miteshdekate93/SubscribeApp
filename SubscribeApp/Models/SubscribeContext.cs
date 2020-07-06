using System.ComponentModel.DataAnnotations;

namespace DemoApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SubscribeContext : DbContext
    {
        // Your context has been configured to use a 'SubscribeContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DemoApp.Models.SubscribeContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'SubscribeContext' 
        // connection string in the application configuration file.
        public SubscribeContext()
            : base("name=SubscribeContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<SubscriberDetail> SubscriberDetail { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    public partial class SubscriberDetail
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name required", AllowEmptyStrings = false)]
        public string Name { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
    }
}