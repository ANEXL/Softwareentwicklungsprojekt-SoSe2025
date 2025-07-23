using FitnessTracker.Models;
using FitnessTracker.Services;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System.Linq;
using System.Globalization;
using System.Collections.Specialized; // Für NotifyCollectionChangedEventHandler
using FitnessTracker.Converters;

namespace FitnessTracker
{
    public partial class WorkoutPage : ContentPage
    {
        private readonly DatabaseService _datenbankService;
        public string WorkoutIdString { get; set; }

        private ObservableCollection<Uebung> _alleVerfuegbareUebungen = new ObservableCollection<Uebung>();
        private ObservableCollection<Uebung> _uebungenFuerDropdown = new ObservableCollection<Uebung>();
        private ObservableCollection<Workout> _aktuelleWorkoutSaetze = new ObservableCollection<Workout>();

        public WorkoutPage(string workoutID)
        {
            Console.WriteLine("WorkoutPage Konstruktor gestartet");
            InitializeComponent();
            Console.WriteLine("InitializeComponent abgeschlossen");

            WorkoutIdString = workoutID;
            Console.WriteLine($"WorkoutIdString gesetzt: {WorkoutIdString}");

            WorkoutTitleLabel.Text = workoutID;
            Console.WriteLine("WorkoutTitleLabel.Text gesetzt");

            _datenbankService = MauiProgram.Current.Services.GetService<DatabaseService>();
            Console.WriteLine(_datenbankService == null
                ? "DatabaseService ist NULL!"
                : "DatabaseService erfolgreich geladen");

            UebungsPicker.ItemsSource = _uebungenFuerDropdown;
            Console.WriteLine("UebungsPicker.ItemsSource gesetzt");

            WorkoutListView.ItemsSource = _aktuelleWorkoutSaetze;
            Console.WriteLine("WorkoutListView.ItemsSource gesetzt");

            _aktuelleWorkoutSaetze.CollectionChanged += OnAktuelleWorkoutSaetzeChanged;
            Console.WriteLine("CollectionChanged-Handler gesetzt");

            this.Appearing += WorkoutPage_Appearing;
            Console.WriteLine("Appearing-Handler gesetzt");

            UpdateWorkoutListVisibility();
            Console.WriteLine("UpdateWorkoutListVisibility aufgerufen");

            Console.WriteLine("WorkoutPage Konstruktor beendet");
        }

        private async void WorkoutPage_Appearing(object sender, EventArgs e)
        {
            await LoadWorkouts();
            await LoadUebungen();
        }

        private async Task LoadWorkouts()
        {
            try
            {
                // WorkoutID aus dem letzten Zeichen extrahieren
                int workoutId = int.Parse(WorkoutIdString[^1].ToString());
                var geladeneWorkouts = await _datenbankService.GetWorkoutsAsync();
                _aktuelleWorkoutSaetze.Clear();
                foreach (var workout in geladeneWorkouts.Where(w => w.WorkoutID == workoutId))
                {
                    _aktuelleWorkoutSaetze.Add(workout);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fehler", $"Workouts konnten nicht geladen werden: {ex.Message}", "OK");
            }
        }

        private async Task LoadUebungen()
        {
            try
            {
                int workoutId = int.Parse(WorkoutIdString[^1].ToString());
                var geladeneUebungen = await _datenbankService.GetUebungenAsync();
                var geladeneWorkouts = await _datenbankService.GetWorkoutsAsync();
                var verwendeteUebungIDs = geladeneWorkouts
                    .Where(w => w.WorkoutID == workoutId)
                    .Select(w => w.UebungID)
                    .ToHashSet();

                _alleVerfuegbareUebungen.Clear();
                _uebungenFuerDropdown.Clear();

                foreach (var uebung in geladeneUebungen)
                {
                    _alleVerfuegbareUebungen.Add(uebung);
                    if (!verwendeteUebungIDs.Contains(uebung.UebungID))
                    {
                        _uebungenFuerDropdown.Add(uebung);
                    }
                }

                ((UebungIdToBezeichnungConverter)Resources["UebungIdToBezeichnungConverter"]).Uebungen = _alleVerfuegbareUebungen;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler in LoadUebungen: " + ex.Message);
                await DisplayAlert("Fehler", $"Übungen konnten nicht geladen werden: {ex.Message}", "OK");
            }
        }

        private async void NeueUebungHinzufuegenBtn_Clicked(object sender, EventArgs e)
        {
            string neueUebungBezeichnung = NeueUebungEntry.Text?.Trim();
            if (string.IsNullOrWhiteSpace(neueUebungBezeichnung))
            {
                await DisplayAlert("Fehler", "Bitte eine Bezeichnung für die neue Übung eingeben.", "OK");
                return;
            }

            try
            {
                if (_alleVerfuegbareUebungen.Any(u => u.Bezeichnung.Equals(neueUebungBezeichnung, StringComparison.OrdinalIgnoreCase)))
                {
                    await DisplayAlert("Hinweis", $"Die Übung '{neueUebungBezeichnung}' existiert bereits.", "OK");
                    NeueUebungEntry.Text = string.Empty;
                    return;
                }

                var neueUebung = new Uebung { Bezeichnung = neueUebungBezeichnung };
                await _datenbankService.SaveUebungAsync(neueUebung);

                _alleVerfuegbareUebungen.Add(neueUebung);
                _uebungenFuerDropdown.Add(neueUebung);
                NeueUebungEntry.Text = string.Empty;

                UebungsPicker.SelectedItem = neueUebung;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fehler", $"Übung konnte nicht hinzugefügt werden: {ex.Message}", "OK");
            }
        }

        private void NeueUebungEntry_Completed(object sender, EventArgs e)
        {
            NeueUebungHinzufuegenBtn_Clicked(sender, e);
        }

        private async void AddWorkoutSatzBtn_Clicked(object sender, EventArgs e)
        {
            Uebung ausgewaehlteUebung = UebungsPicker.SelectedItem as Uebung;

            if (ausgewaehlteUebung == null)
            {
                await DisplayAlert("Fehler", "Bitte eine Übung auswählen.", "OK");
                return;
            }

            if (!int.TryParse(SaetzeEntry.Text, out int saetze) || saetze <= 0)
            {
                await DisplayAlert("Fehler", "Bitte eine gültige Anzahl für Sätze (> 0) eingeben.", "OK");
                return;
            }
            if (!int.TryParse(WiederholungenEntry.Text, out int wiederholungen) || wiederholungen <= 0)
            {
                await DisplayAlert("Fehler", "Bitte eine gültige Anzahl für Wiederholungen (> 0) eingeben.", "OK");
                return;
            }
            if (!double.TryParse(GewichtEntry.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double gewicht) || gewicht < 0)
            {
                await DisplayAlert("Fehler", "Bitte ein gültiges Gewicht (>= 0) eingeben.", "OK");
                return;
            }

            try
            {
                var neuerWorkoutSatz = new Workout
                {
                    UebungID = ausgewaehlteUebung.UebungID,
                    Saetze = saetze,
                    Wiederholungen = wiederholungen,
                    Gewicht = gewicht
                };

                await _datenbankService.SaveWorkoutAsync(neuerWorkoutSatz);
                _aktuelleWorkoutSaetze.Add(neuerWorkoutSatz);

                // Entferne die Übung aus dem Dropdown
                _uebungenFuerDropdown.Remove(ausgewaehlteUebung);
                UebungsPicker.SelectedItem = null;

                // Eingabefelder leeren
                SaetzeEntry.Text = string.Empty;
                WiederholungenEntry.Text = string.Empty;
                GewichtEntry.Text = string.Empty;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fehler", $"Workout-Satz konnte nicht hinzugefügt werden: {ex.Message}", "OK");
            }
        }

        private void UebungsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Keine spezielle Logik hier erforderlich
        }

        // NEU: Methode zur Steuerung der Sichtbarkeit
        private void OnAktuelleWorkoutSaetzeChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateWorkoutListVisibility();
        }

        private void UpdateWorkoutListVisibility()
        {
            bool hasWorkouts = _aktuelleWorkoutSaetze.Any();
            ErfassteWorkoutsLabel.IsVisible = hasWorkouts;
            WorkoutListView.IsVisible = hasWorkouts;
            KeineWorkoutsLabel.IsVisible = !hasWorkouts;
        }
    }
}