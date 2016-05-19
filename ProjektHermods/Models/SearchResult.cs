using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektHermods.Models
{
    [Serializable]
    public class SearchResultList
        {
        public int Id { get; set; }
        public string Name { get; set; }

        public int match { get; set; }
        public int noMatch { get; set; }
        public IList<Ingrediens> Excluded { get; set; }

        }
    }
