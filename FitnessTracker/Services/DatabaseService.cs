using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using FitnessTracker.Models;
using Microsoft.Maui.Storage;
using SQLite;

namespace FitnessTracker.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _datenbank;

        public DatabaseService()
        {
            // Konstruktor: Initialisiert die Datenbankverbindung und erstellt Tabellen.
            var datenbankPfad = Path.Combine(FileSystem.AppDataDirectory, "FitnessTracker.db3");
            _datenbank = new SQLiteAsyncConnection(datenbankPfad);
        }

        // Initialisiert die Datenbank: Erstellt Tabellen, falls sie noch nicht existieren.
        // Fügt beispielhaft einige Übungen hinzu, wenn die Tabelle leer ist.
        public async Task InitialisiereDatenbankAsync()
        {
            // Tabellen erstellen mit den neuen Klassennamen
            await _datenbank.CreateTableAsync<Uebung>();    // Tabelle für Übungen erstellen (UebungsTbl)
            await _datenbank.CreateTableAsync<Workout>();   // Tabelle für Workouts/Sätze erstellen (WorkoutTbl)

            // Füge einige Standardübungen hinzu, falls die Tabelle leer ist.
            if (await _datenbank.Table<Uebung>().CountAsync() == 0)
            {
                await _datenbank.InsertAsync(new Uebung { Bezeichnung = "Bankdrücken" });
                await _datenbank.InsertAsync(new Uebung { Bezeichnung = "Kniebeugen" });
                await _datenbank.InsertAsync(new Uebung { Bezeichnung = "Kreuzheben" });
                await _datenbank.InsertAsync(new Uebung { Bezeichnung = "Schulterdrücken" });
                await _datenbank.InsertAsync(new Uebung { Bezeichnung = "Bizeps-Curls" });
            }
        }

        // CRUD-Operationen für Uebung

        // Ruft alle Übungen aus der Datenbank ab.
        public Task<List<Uebung>> GetUebungenAsync()
        {
            return _datenbank.Table<Uebung>().ToListAsync();
        }

        // Speichert eine Übung in der Datenbank (fügt hinzu oder aktualisiert).
        public Task<int> SaveUebungAsync(Uebung uebung)
        {
            if (uebung.UebungID != 0)
            {
                return _datenbank.UpdateAsync(uebung);
            }
            else
            {
                return _datenbank.InsertAsync(uebung);
            }
        }

        // Löscht eine Übung aus der Datenbank.
        public Task<int> DeleteUebungAsync(Uebung uebung)
        {
            return _datenbank.DeleteAsync(uebung);
        }

        // CRUD-Operationen für Workout

        // Speichert einen Workout-Satz in der Datenbank (fügt hinzu oder aktualisiert).
        public Task<int> SaveWorkoutAsync(Workout workout)
        {
            if (workout.WorkoutID != 0)
            {
                return _datenbank.UpdateAsync(workout);
            }
            else
            {
                return _datenbank.InsertAsync(workout);
            }
        }

        // Ruft alle Workout-Sätze ab.
        public Task<List<Workout>> GetWorkoutsAsync()
        {
            return _datenbank.Table<Workout>().ToListAsync();
        }

        // HINWEIS: Methoden für TrainingSession bleiben weiterhin entfernt,
        // da sie nicht Teil des fixen Datenbankdiagramms sind.
    }
}
