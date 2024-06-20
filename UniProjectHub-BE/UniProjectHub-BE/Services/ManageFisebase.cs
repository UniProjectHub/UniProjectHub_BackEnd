using Domain.Data;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.Extensions.Options;
using System.Collections;
namespace UniProjectHub_BE.Services
{
    public class ManageFisebase(IOptions<FirebaseSettings> settings)
    {
        private readonly FirebaseSettings _firebaseSettings = settings.Value;

        public string ImageURL(IFormFile file)
        {
            return UploadImageToFirebase(file).GetAwaiter().GetResult();
        }
        public List<string> MultipleImageURL(List<IFormFile> files)
        {
            return UploadMultipleImageToFirebase(files).GetAwaiter().GetResult();
        }

        private async Task<string> UploadImageToFirebase(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return "File is null or empty.";
            }

            try
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    ms.Seek(0, SeekOrigin.Begin);

                    var auth = new FirebaseAuthProvider(new FirebaseConfig(_firebaseSettings.ApiKey));
                    var authResult = await auth.SignInWithEmailAndPasswordAsync(_firebaseSettings.AuthEmail, _firebaseSettings.AuthPassword);
                    var cancellation = new CancellationTokenSource();

                    var task = new FirebaseStorage(
                        _firebaseSettings.Bucket,
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(authResult.FirebaseToken),
                            ThrowOnCancel = true
                        })
                        .Child("assets")
                        .Child($"{file.FileName}")
                        .PutAsync(ms, cancellation.Token);

                    return await task;
                }
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }

        private async Task<List<string>> UploadMultipleImageToFirebase(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                throw new Exception("No file!");
            }
            List<string> listUrl = new List<string>();

            try
            {
                using (var ms = new MemoryStream())
                {
                    foreach (var file in files)
                    {
                        await file.CopyToAsync(ms);
                        ms.Seek(0, SeekOrigin.Begin);

                        var auth = new FirebaseAuthProvider(new FirebaseConfig(_firebaseSettings.ApiKey));
                        var authResult = await auth.SignInWithEmailAndPasswordAsync(_firebaseSettings.AuthEmail, _firebaseSettings.AuthPassword);
                        var cancellation = new CancellationTokenSource();

                        var task = new FirebaseStorage(
                            _firebaseSettings.Bucket,
                            new FirebaseStorageOptions
                            {
                                AuthTokenAsyncFactory = () => Task.FromResult(authResult.FirebaseToken),
                                ThrowOnCancel = true
                            })
                            .Child("assets")
                            .Child($"{file.FileName}")
                            .PutAsync(ms, cancellation.Token);

                        listUrl.Add(await task);
                    }
                    return listUrl;
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}