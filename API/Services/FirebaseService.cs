using Firebase.Storage;
using Newtonsoft.Json;

namespace API.Services
{
    public class FirebaseService
    {
        private readonly FirebaseStorage _firebaseStorage;
        private readonly HttpClient _httpClient;
        private readonly string _firebaseApiUrl = "https://firebasestorage.googleapis.com/v0/b/your-firebase-app-id.appspot.com/o";

        public FirebaseService(IConfiguration configuration, HttpClient httpClient)
        {
            var firebaseConfig = configuration.GetSection("Firebase");

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

        public async Task DeleteFileAsync(string filePath)
        {
            try
            {
                var fileReference = _firebaseStorage.Child(filePath);
                await fileReference.DeleteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting file from Firebase: {ex.Message}");
            }
        }
        
        public async Task DeleteFolderAsync(string folderPath)
        {
            var filesToDelete = await ListFilesInFolder(folderPath);

            foreach (var file in filesToDelete)
            {
                await DeleteFileAsync(file);
            }
        }
        
        private async Task<List<string>> ListFilesInFolder(string folderPath)
        {
            var fileUrls = new List<string>();

            var requestUri = $"{_firebaseApiUrl}/{folderPath}?&fields=items/name";
            var response = await _httpClient.GetStringAsync(requestUri);
            var fileList = JsonConvert.DeserializeObject<dynamic>(response);

            foreach (var file in fileList.items)
            {
                fileUrls.Add(file.name.ToString());
            }

            return fileUrls;
        }
    }
}
