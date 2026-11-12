using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using InteriorDesignApp.Models;

namespace InteriorDesignApp.ViewModels
{
    public class CartViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Product> CartItems { get; set; } = new();

        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get => _totalPrice;
            set
            {
                if (_totalPrice != value)
                {
                    _totalPrice = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand NavigateToStoresCommand { get; }

        public CartViewModel()
        {
            LoadCartItems();
            NavigateToStoresCommand = new Command(OnNavigateToStoresClicked);
        }

        private void LoadCartItems()
        {
            CartItems.Add(new Product { Name = "ספה מודרנית רכה בגוון שמנת", StoreName = "איקאה (ראשון לציון)", Price = 3800, IsInStock = true });
            CartItems.Add(new Product { Name = "שטיח סקנדינבי טבעי", StoreName = "גולף אנד קו (תל אביב)", Price = 890, IsInStock = true });
            CartItems.Add(new Product { Name = "גוף תאורה תלוי מעוצב", StoreName = "מחסני תאורה", Price = 450, IsInStock = true });

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            TotalPrice = CartItems.Sum(p => p.Price);
        }

        private async void OnNavigateToStoresClicked()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert(
                "ניווט חכם לחנויות 🗺️",
                "המסלול האופטימלי לחנויות שיש בהן את המוצרים במלאי מוכן. האם לפתוח את Waze?",
                "כן, פתח Waze",
                "ביטול");

            if (answer)
            {
                try
                {
                    string wazeUrl = "https://waze.com/ul?q=IKEA";
                    await Launcher.Default.OpenAsync(new Uri(wazeUrl));
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("שגיאה", "לא ניתן לפתוח את אפליקציית הניווט: " + ex.Message, "אישור");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}