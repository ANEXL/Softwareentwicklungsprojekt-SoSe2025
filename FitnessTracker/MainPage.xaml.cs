using Microsoft.Maui.Controls;
using System.Linq;
using FitnessTracker.Services;

namespace FitnessTracker
{
    public partial class MainPage : ContentPage
    {
        int workoutCount = 0;
        private DatabaseService _dbService = new DatabaseService();

        public MainPage()
        {
            InitializeComponent();
            LoadWorkoutButtons();
        }

        private async void LoadWorkoutButtons()
        {
            var workouts = await _dbService.GetWorkoutsAsync();
            foreach (var workout in workouts)
            {
                string btnName = $"Workout {workout.WorkoutID}";
                Button workoutBtn = new Button
                {
                    Text = btnName,
                    BackgroundColor = Color.FromArgb("#4CAF50"),
                    TextColor = Color.Parse("red"),
                    FontSize = 20,
                    Padding = 10,
                    Margin = 20
                };

                workoutBtn.Clicked += async (s, args) =>
                {
                    await Navigation.PushAsync(new WorkoutPage(workout.WorkoutID.ToString()));
                };

                MainStack.Children.Add(workoutBtn);
            }

            // Setze workoutCount auf die höchste vorhandene WorkoutID
            if (workouts.Any())
                workoutCount = workouts.Max(w => w.WorkoutID);
            else
                workoutCount = 0;
        }

        private void AddWorkoutBtn_Clicked(object sender, EventArgs e)
        {
            workoutCount++;
            string btnName = $"Workout {workoutCount}";

            Button newWorkout = new Button();

            MainStack.Children.Add(newWorkout);
            newWorkout.Text = btnName;
            newWorkout.BackgroundColor = Color.FromArgb("#4CAF50");
            newWorkout.TextColor = Color.Parse("red");
            newWorkout.FontSize = 20;
            newWorkout.Padding = 10;
            newWorkout.Margin = 20;

            newWorkout.Clicked += async (s, args) =>
            {
                await Navigation.PushAsync(new WorkoutPage(btnName));
            };
        }
    }
}