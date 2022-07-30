using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http.Configuration;
using Moq;
using Moq.Protected;

namespace PetStore.Tests.Helper
{
    public static class HttpClientMock
    {
        // Creates a mockable http client
        public static HttpClient Create(string baseUrl, object? objectToSerialize = null)
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