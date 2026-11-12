namespace InteriorDesignApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        // מגדיר את העמוד הראשון שהאפליקציה טוענת (ניתן לשנות ל-AppShell או ל-OnboardingPage)
        return new Window(new OnboardingPage());
    }
}