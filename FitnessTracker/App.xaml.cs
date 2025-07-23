using System.Threading.Tasks;
using FitnessTracker.Services;

namespace FitnessTracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var dbService = MauiProgram.Current.Services.GetService<DatabaseService>();
            if (dbService != null)
                Task.Run(async () => await dbService.InitialisiereDatenbankAsync());
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}