using System;
using SQLite;
using System.Collections.Generic;

namespace Ableger.Models
{
    public class TrainingSession
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now; // Datum und Uhrzeit des Trainings
        public string Notes { get; set; } = string.Empty; // Optional: Notizen zum Training
    }
}
