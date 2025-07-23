
namespace FitnessTracker
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
        }
        // This variable keeps track of the number of workouts added
        int workoutCount = 0;

        private void AddWorkoutBtn_Clicked(object sender, EventArgs e)
        {
            workoutCount++;

            // Generate a unique name for the new workout button
            string btnName = $"Workout {workoutCount}";

            // Create a new button dynamically
            Button newworkout = new Button();


            // Set properties for the new button
            MainStack.Children.Add(newworkout);
            newworkout.Text = btnName;
            newworkout.BackgroundColor = Color.FromArgb("#4CAF50");
            newworkout.TextColor = Color.Parse("red");
            newworkout.FontSize = 20;
            newworkout.Padding = 10;
            newworkout.Margin = 20;

            // Add an event handler for the button click
            newworkout.Clicked += (s, args) =>
            {
                // Navigate to the WorkoutPage with the button name as a parameter
                Navigation.PushAsync(new WorkoutPage(btnName));
            };


        }
    }
}