using System;
using System.Collections.Generic;
using System.Text;

namespace Relish.Models
{
    public class SimpleIngredient
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public float Quantity { get; set; }
        public string OriginalString { get; set; }
        public bool UserHasIngredient { get; set; }
    }
}
