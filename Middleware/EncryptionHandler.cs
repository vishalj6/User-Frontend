using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UsersProject.Middleware
{
    public class EncryptionHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content != null)
            {
                var originalContent = await request.Content.ReadAsStringAsync();
                var encryptedContent = EncryptionHelper.EncryptString(originalContent);
                request.Content = new StringContent(encryptedContent, Encoding.UTF8, "application/json");
            }

            return await base.SendAsync(request, cancellationToken);
        }

    }

}
