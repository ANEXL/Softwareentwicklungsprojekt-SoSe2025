using System;
using SQLite;

namespace FitnessTracker.Models
{
    [Table("UebungsTbl")] // Mappt die Klasse auf den Tabellennamen
    public class Uebung
    {
        [PrimaryKey, AutoIncrement]
        [Column("UebungsID")] // Mappt die Property auf den Primärschlüssel-Spaltennamen
        public int UebungID { get; set; }

        [Column("Uebung")] // Mappt die Property auf den Spaltennamen im Diagramm
        public string Bezeichnung { get; set; } = string.Empty;
    }
}
