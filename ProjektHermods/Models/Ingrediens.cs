using System.Collections.Generic;

namespace ProjektHermods.Models
{
    public class Ingrediens
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IngrediensBild { get; set; }
        public virtual IList<Recept> Recepies {get;set;}
    }
}