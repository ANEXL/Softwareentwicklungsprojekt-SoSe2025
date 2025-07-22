using System;
using SQLite;

namespace FitnessTracker.Models
{
    [Table("WorkoutTbl")] // Mappt die Klasse auf den Tabellennamen
    public class Workout
    {
        [PrimaryKey, AutoIncrement]
        [Column("WorkoutID")] // Mappt die Property auf den Primärschlüssel-Spaltennamen
        public int WorkoutID { get; set; }

        [Column("UebungID")] // Mappt die Property auf den Fremdschlüssel-Spaltennamen im Diagramm
        public int UebungID { get; set; }

        [Column("Saetze")]
        public int Saetze { get; set; }

        [Column("Wiederholungen")]
        public int Wiederholungen { get; set; }

        [Column("Gewicht")]
        public double Gewicht { get; set; }
    }
}
