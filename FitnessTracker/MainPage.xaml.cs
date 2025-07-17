//using static Android.Print.PrintAttributes;
//using static Java.Util.Jar.Attributes;
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

            Button newworkout = new Button();



            MainStack.Children.Add(newworkout);

            newworkout.Text = btnName;
            newworkout.BackgroundColor = Color.FromArgb("#4CAF50");
            newworkout.TextColor = Color.Parse("red");
            newworkout.FontSize = 20;
            newworkout.Padding = 10;
            newworkout.Margin = 20;
            newworkout.Clicked += (s, args) =>
            {
                //Navigation.PushAsync(new AddWorkoutPage());
                newworkout.Text = "Clicked!";
            };


        }
    }
}