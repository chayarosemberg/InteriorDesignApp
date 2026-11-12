namespace InteriorDesignApp.Views
{
    public partial class HomePage : ContentPage
    {
        private string _capturedImagePath = string.Empty; // משתנה לשמירת נתיב התמונה

        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnTakePicClicked(object sender, EventArgs e)
        {
            try
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                    if (photo != null)
                    {
                        await ProcessSelectedPhoto(photo);
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("שגיאה", $"לא ניתן לצלם תמונה: {ex.Message}", "אישור");
            }
        }

        private async void OnPickPicClicked(object sender, EventArgs e)
        {
            try
            {
                FileResult photo = await MediaPicker.Default.PickPhotoAsync();
                if (photo != null)
                {
                    await ProcessSelectedPhoto(photo);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("שגיאה", $"לא ניתן לבחור תמונה: {ex.Message}", "אישור");
            }
        }

        private async Task ProcessSelectedPhoto(FileResult photo)
        {
            string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using Stream sourceStream = await photo.OpenReadAsync();
            using FileStream localFileStream = File.OpenWrite(localFilePath);
            await sourceStream.CopyToAsync(localFileStream);

            _capturedImagePath = localFilePath; // שומר את הנתיב האמיתי של תמונת החדר שלך!
            SelectedImage.Source = localFilePath;
            PlaceholderLayout.IsVisible = false;
            ContinueButton.IsVisible = true;
        }

        private async void OnGoToDiagnosisClicked(object sender, EventArgs e)
        {
            // מעבר למסך האבחון תוך העברת נתיב התמונה האמיתית
            await Navigation.PushAsync(new AIDiagnosisPage(_capturedImagePath));
        }
    }
}