using System.Net.Http.Headers;
using Firebase.Storage;
using Newtonsoft.Json;

namespace API.Services
{
    public class FirebaseService
    {
        private readonly FirebaseStorage _firebaseStorage;
        private readonly HttpClient _httpClient;
        private readonly string _firebaseApiUrl;
        public FirebaseService(IConfiguration configuration, HttpClient httpClient)
        {
            var firebaseConfig = configuration.GetSection("Firebase");
            _firebaseApiUrl = $"https://firebasestorage.googleapis.com/v0/b/{firebaseConfig["BucketName"]}/o";
            _firebaseStorage = new FirebaseStorage(
                firebaseConfig["BucketName"],
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = async () =>
                    {
                        var token = await GetFirebaseToken(firebaseConfig["PrivateKey"]);
                        return token;
                    }
                });

            _httpClient = httpClient;
        }

        private async Task<string> GetFirebaseToken(string privateKey)
        {
            return privateKey;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderPath)
        {
            using (var stream = file.OpenReadStream())
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var fileUrl = await _firebaseStorage
                    .Child(folderPath)
                    .Child(uniqueFileName)
                    .PutAsync(stream);
                return fileUrl;
            }
        }
        public async Task DeleteFolderAsync(string folderPath)
        {
            try
            {
                var listUrl = $"{_firebaseApiUrl}?prefix={Uri.EscapeDataString(folderPath)}";

                var request = new HttpRequestMessage(HttpMethod.Get, listUrl);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to retrieve file list from Firebase Storage. Status code: {response.StatusCode}");
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();

                dynamic jsonObject = JsonConvert.DeserializeObject(jsonResponse);
                if (jsonObject.items == null)
                {
                    return;
                }

                foreach (var item in jsonObject.items)
                {
                    string filePath = item.name;
                    await _firebaseStorage.Child(filePath).DeleteAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete folder: {ex.Message}");
            }
        }

    }
}
