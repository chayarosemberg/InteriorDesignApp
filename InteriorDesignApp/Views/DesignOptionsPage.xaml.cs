using System.Collections.ObjectModel;

namespace InteriorDesignApp.Views
{
    public partial class DesignOptionsPage : ContentPage
    {
        public class DesignOption
        {
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public decimal EstimatedCost { get; set; }
            public string ImageUrl { get; set; } = string.Empty;
        }

        public ObservableCollection<DesignOption> Options { get; set; } = new();

        public DesignOptionsPage()
        {
            InitializeComponent();
            LoadMockDesigns();

            DesignsCarousel.ItemsSource = Options;
            DesignsCarousel.IndicatorView = IndicatorView;
        }

        private void LoadMockDesigns()
        {
            Options.Add(new DesignOption
            {
                Title = "אופציה 1: מודרני חמים",
                Description = "שילוב גווני שמנת, עץ אלון טבעי ותאורה רכה ליצירת תחושת מרחב.",
                EstimatedCost = 8500,
                ImageUrl = "https://picsum.photos/600/400?random=1"
            });

            Options.Add(new DesignOption
            {
                Title = "אופציה 2: סקנדינבי מינימליסטי",
                Description = "קווים נקיים, טקסטורות פשתן וגווני אפור-לבן המעניקים שקט נפשי.",
                EstimatedCost = 6200,
                ImageUrl = "https://picsum.photos/600/400?random=2"
            });

            Options.Add(new DesignOption
            {
                Title = "אופציה 3: בוהו-שיק עשיר",
                Description = "טקסטיל עשיר, צמחייה ביתית, גווני חמרה ואלמנטים מקש.",
                EstimatedCost = 11000,
                ImageUrl = "https://picsum.photos/600/400?random=3"
            });
        }

        private async void OnSendChatClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ChatInput.Text))
            {
                string userReq = ChatInput.Text;
                ChatInput.Text = string.Empty;
                await DisplayAlert("עדכון AI", $"הבקשה שלך ('{userReq}') נשלחה ל-AI! ההדמיה מתעדכנת כעת...", "אישור");
            }
        }

        private async void OnSelectDesignClicked(object sender, EventArgs e)
        {
            await DisplayAlert("הבחירה נשמרה!", "המוצרים מהעיצוב הנבחר נוספו לסל הקניות שלך.", "לסל הקניות והמלאי");
            await Navigation.PushAsync(new CartPage());
        }
    }
}