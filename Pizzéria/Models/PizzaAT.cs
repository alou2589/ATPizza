using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace Pizzéria.Models
{
    public class PizzaAT
    {
        [Key]
        public int IdAT{get;set;}
        public string? NomAT{get;set;}
        public string? DescriptionAT{get;set;}

    }

    class PizzaATDB:DbContext
    {
        public PizzaATDB(DbContextOptions options):base(options){}
        public DbSet<PizzaAT> Pizzas{get;set;}=null!;
    }
}