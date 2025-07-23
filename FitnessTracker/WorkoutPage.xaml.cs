namespace FitnessTracker;



public partial class WorkoutPage : ContentPage
{
	public WorkoutPage(string WorkoutID)
	{
		InitializeComponent();
		Placeholder.Text = WorkoutID;
    }
}