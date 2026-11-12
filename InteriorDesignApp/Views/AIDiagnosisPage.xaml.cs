namespace InteriorDesignApp.Views
{
    public partial class AIDiagnosisPage : ContentPage
    {
        private string _imagePath;

        public AIDiagnosisPage(string imagePath)
        {
            InitializeComponent();
            _imagePath = imagePath;
        }

        private async void OnGenerateClicked(object sender, EventArgs e)
        {
            if (RoomTypePicker.SelectedIndex == -1 || StylePicker.SelectedIndex == -1 || ColorPicker.SelectedIndex == -1 || BudgetPicker.SelectedIndex == -1)
            {
                await DisplayAlert("שגיאה", "אנא מלא את כל השדות בשאלון כדי שהבינה המלאכותית תוכל לייצר את ההדמיה המדויקת ביותר.", "אישור");
                return;
            }

            string roomType = RoomTypePicker.SelectedItem.ToString() ?? "";
            string style = StylePicker.SelectedItem.ToString() ?? "";
            string color = ColorPicker.SelectedItem.ToString() ?? "";
            string budget = BudgetPicker.SelectedItem.ToString() ?? "";

            // מעבר למסך ההדמיה עם כל הנתונים
            await Navigation.PushAsync(new SingleDesignPage(_imagePath, roomType, style, color, budget));
        }
    }
}