using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using IHttpClientFactory = Flurl.Http.Configuration.IHttpClientFactory;

namespace UITestingPlayground.Tests.Helper
{
    public static class HttpClientHelper
    {
        // Creates a mockable http client
        public static HttpClient CreateHttpClient(string baseUrl, object? objectToSerialize = null)
        {
            var mockFactory = new Mock<IHttpClientFactory>();

            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
              .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
              .ReturnsAsync(new HttpResponseMessage
              {
                  StatusCode = HttpStatusCode.OK,
                  Content = objectToSerialize != null ? new StringContent(JsonSerializer.Serialize(objectToSerialize)) : null
              });

            var mockHttpClient = new HttpClient(mockMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateHttpClient(mockMessageHandler.Object)).Returns(mockHttpClient).Verifiable();
            mockHttpClient.BaseAddress = new Uri(baseUrl);
            return mockHttpClient;
        }
    }
}