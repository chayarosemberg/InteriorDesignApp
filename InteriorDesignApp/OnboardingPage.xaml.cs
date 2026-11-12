using InteriorDesignApp.Views;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace InteriorDesignApp;

public partial class OnboardingPage : ContentPage
{
    private class OnboardingItem
    {
        public string ImagePath { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    private List<OnboardingItem> _slides = new();

    public OnboardingPage()
    {
        InitializeComponent();
        LoadSlides();
    }

    private void LoadSlides()
    {
        _slides = new List<OnboardingItem>
        {
            new OnboardingItem
            {
                ImagePath = "slide1.jpg",
                Title = "576,000 רהיטים ופריטי עיצוב!",
                Description = "השתמש בקטלוג עם למעלה מ- 576,000 פריטי תכנון פנים ממותגים מוכרים לעיצוב החלל שלך."
            },
            new OnboardingItem
            {
                ImagePath = "slide2.jpg",
                Title = "תודה שהכל מתאים",
                Description = "תכנון הרכישות תוך התחשבות בגודל 😉"
            },
            new OnboardingItem
            {
                ImagePath = "slide3.jpg",
                Title = "לבחור את הצבע האהוב",
                Description = "יש לנו את כל מה שאתה צריך כדי ליצור עיצוב מעולה ולמצוא את הפריטים הנכונים בחנויות."
            },
            new OnboardingItem
            {
                ImagePath = "slide4.jpg",
                Title = "עיצוב מחדש מיידי של החלל שלך עם AI",
                Description = "ליצור עיצובים חדשים לגמרי עם AI בכמה שלבים בלבד."
            },
            new OnboardingItem
            {
                ImagePath = "slide5.jpg",
                Title = "המלצת ריהוט לפי הפניות לסגנון שלך ועיצוב החדר",
                Description = "קבלת התאמות מדויקות של פריטים שמשתלבים בצורה מושלמת עם החלל שלך."
            }
        };

        OnboardingCarousel.ItemsSource = _slides;
        SlideIndicator.ItemsSource = _slides;
    }

    private void OnNextClicked(object? sender, EventArgs e)
    {
        int nextPosition = OnboardingCarousel.Position + 1;

        if (nextPosition < _slides.Count)
        {
            OnboardingCarousel.Position = nextPosition;
        }
        else
        {
            // מעבר נקי לדף הבית בסיום הסליידים
            Application.Current!.Windows[0].Page = new NavigationPage(new HomePage());
        }
    }

    private void OnboardingCarousel_PositionChanged(object? sender, PositionChangedEventArgs e)
    {
        if (e.CurrentPosition == _slides.Count - 1)
        {
            NextButton.Text = "התחל עכשיו";
        }
        else
        {
            NextButton.Text = "הבא";
        }
    }
}