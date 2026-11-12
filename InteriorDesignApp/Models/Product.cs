namespace InteriorDesignApp.Models
{
    public class Product
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;          // שם המוצר (למשל: ספה אפורה)
        public string Category { get; set; } = string.Empty;      // קטגוריה (ספה, תאורה, שטיח וכו')
        public decimal Price { get; set; }                        // מחיר בשקלים
        public string ImageUrl { get; set; } = string.Empty;      // קישור לתמונה של המוצר
        public bool IsOnlineOnly { get; set; }                    // האם נמכר אונליין בלבד או גם בחנות פיזית
        public bool IsInStock { get; set; } = true;               // בדיקת מלאי בזמן אמת
        public string StoreName { get; set; } = string.Empty;     // שם החנות (למשל: איקאה, גולף אנד קו)
    }
}