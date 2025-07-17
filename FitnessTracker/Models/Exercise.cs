using System;
using SQLite;
using System.Collections.Generic;

namespace Ableger.Models
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement] //ID ist Primaerschluessel und wird automatisch inkrementiert
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MuscleGroup { get; set; } = string.Empty;
    }
}
