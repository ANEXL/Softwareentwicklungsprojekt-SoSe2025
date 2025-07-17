using System;
using SQLite;
using System.Collections.Generic;

namespace Ableger.Models
{
    public class Set
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TrainingSessionId { get; set; } // Fremdschlüssel zur TrainingSession
        public int ExerciseId { get; set; }        // Fremdschlüssel zur Exercise
        public int Repetitions { get; set; }       // Anzahl der Wiederholungen
        public double Weight { get; set; }         // Verwendetes Gewicht in kg (hinzugefügt, da im Kontext üblich)
    }
}
