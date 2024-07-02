using System.Text;

namespace UsersProject.Middleware
{
    public class DecryptionHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Send the request and get the response
            var response = await base.SendAsync(request, cancellationToken);

            // Ensure response has content and is successful
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                // Decrypt the response content
                var encryptedContent = await response.Content.ReadAsStringAsync();
                var decryptedContent = DecryptionHelper.DecryptString(encryptedContent);

                // Replace the response content with decrypted content
                response.Content = new StringContent(decryptedContent, Encoding.UTF8, "application/json");
            }

            return response;
        }
    }
}
