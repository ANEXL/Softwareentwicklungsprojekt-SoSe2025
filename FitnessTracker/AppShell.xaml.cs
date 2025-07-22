namespace FitnessTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(WorkoutPage), typeof(WorkoutPage));
        }
    }
}
