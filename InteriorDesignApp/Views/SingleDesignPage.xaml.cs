namespace InteriorDesignApp.Views
{
    public partial class SingleDesignPage : ContentPage
    {
        private string _imagePath;

        public SingleDesignPage(string imagePath, string roomType, string style, string color, string budget)
        {
            InitializeComponent();
            _imagePath = imagePath;
            RunRealAIDesign(roomType, style, color, budget);
        }

        private async void RunRealAIDesign(string roomType, string style, string color, string budget)
        {
            try
            {
                ProgressPercentLabel.Text = "יוצר חיבור מוצפן לשרת ה-AI... 20%";
                await Task.Delay(500);

                ProgressPercentLabel.Text = "מעלה את תמונת החדר שלך לענן... 50%";

                // קריאה אמיתית לשרת ה-AI
                string finalResultUrl = await AIService.GenerateAiDesignAsync(_imagePath, roomType, style, color, budget);

                ProgressPercentLabel.Text = "ההדמיה הושלמה בהצלחה! 100%";
                await Task.Delay(300);

                // הצגת התמונה האמיתית שנוצרה בשרת
                GeneratedDesignImage.Source = finalResultUrl;
                LoadingLayout.IsVisible = false;
                LoadingLayout.IsEnabled = false;
                GeneratedDesignImage.IsVisible = true;

                StatusSubtitleLabel.Text = "הבינה המלאכותית עיבדה את החדר שלך בהצלחה!";
                StyleTitleLabel.Text = $"קונספט נבחר: {style}";
                ConceptDetailsLabel.Text = $"סוג חדר: {roomType} | צבעים: {color} | תקציב: {budget}";
            }
            catch (Exception ex)
            {
                // תיקון: הפרדת ההשמות בצורה חוקית ב-C#
                LoadingLayout.IsVisible = false;
                LoadingLayout.IsEnabled = false;

                await DisplayAlert("שגיאה במנוע ה-AI", ex.Message, "אישור");
                await Navigation.PopAsync(); // חזרה אחורה לשאלון
            }
        }

        private async void OnGoToCartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartPage());
        }
    }
}