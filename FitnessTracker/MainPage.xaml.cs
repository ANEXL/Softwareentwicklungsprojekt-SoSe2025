using Microsoft.Maui.Controls;

namespace FitnessTracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        int workoutCount = 0;

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