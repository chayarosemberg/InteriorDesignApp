using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Platform;

namespace InteriorDesignApp.Views
{
    public static class AIService
    {
        private const string ApiEndpoint = "https://api.stability.ai/v1/generation/stable-diffusion-xl-1024-v1-0/image-to-image";

        // המפתח האמיתי שלך מוזנים כאן ישירות, ללא תלות באחסון חיצוני
        private const string ApiKey = "sk-vKsAlNfHStNOEwEmPfamN35excsPWJbyjnGwgairUvy2ffPd";

        public static async Task<string> GenerateAiDesignAsync(string imagePath, string roomType, string style, string color, string budget)
        {
            try
            {
                if (string.IsNullOrEmpty(ApiKey))
                {
                    throw new Exception("אנא הגדר מפתח API תקין.");
                }

                // שינוי גודל אוטומטי למידות המדויקות של השרת (1024x1024)
                string processedImagePath = ResizeImageToStandard(imagePath);

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using var form = new MultipartFormDataContent();

                byte[] imageBytes = File.ReadAllBytes(processedImagePath);
                form.Add(new ByteArrayContent(imageBytes), "init_image", "user_room.jpg");

                form.Add(new StringContent("0.35"), "image_strength");
                form.Add(new StringContent("IMAGE_STRENGTH"), "init_image_mode");
                form.Add(new StringContent("7"), "cfg_scale");
                form.Add(new StringContent("30"), "steps");

                string prompt = $"A professional photorealistic interior design transformation of a {roomType}, featuring {style} style, dominant color palette: {color}, targeting budget level: {budget}, highly detailed, 8k resolution, architectural photography.";
                form.Add(new StringContent(prompt), "text_prompts[0][text]");
                form.Add(new StringContent("1"), "text_prompts[0][weight]");

                HttpResponseMessage response = await client.PostAsync(ApiEndpoint, form);

                if (!response.IsSuccessStatusCode)
                {
                    string errorDetails = await response.Content.ReadAsStringAsync();
                    throw new Exception($"שגיאת שרת: {response.StatusCode} - {errorDetails}");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                var root = doc.RootElement;

                if (root.TryGetProperty("artifacts", out var artifacts) && artifacts.GetArrayLength() > 0)
                {
                    string base64Image = artifacts[0].GetProperty("base64").GetString() ?? "";
                    return SaveBase64ImageToCache(base64Image);
                }

                throw new Exception("השרת לא החזיר תמונה תקינה.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AI Error: {ex.Message}");
                throw;
            }
        }

        private static string ResizeImageToStandard(string originalPath)
        {
            try
            {
                using var stream = File.OpenRead(originalPath);
                var image = PlatformImage.FromStream(stream);

                var resizedImage = image.Resize(1024, 1024, ResizeMode.Stretch, true);

                string resizedPath = Path.Combine(FileSystem.CacheDirectory, $"sdxl_room_{Guid.NewGuid()}.jpg");
                using var outputStream = File.OpenWrite(resizedPath);
                resizedImage.Save(outputStream, ImageFormat.Jpeg, 0.9f);

                return resizedPath;
            }
            catch
            {
                return originalPath;
            }
        }

        private static string SaveBase64ImageToCache(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            string filePath = Path.Combine(FileSystem.CacheDirectory, $"ai_real_result_{Guid.NewGuid()}.jpg");
            File.WriteAllBytes(filePath, bytes);
            return filePath;
        }
    }
}