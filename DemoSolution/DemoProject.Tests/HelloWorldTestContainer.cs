using System.Diagnostics;
using DotNet.Testcontainers.Builders;

namespace DemoProject.Tests;

[TestClass]
public class HelloWorldTestContainer
{
	[TestMethod]
	public async Task DoContainerStuff()
	{
		var container = new ContainerBuilder("testcontainers/helloworld:1.3.0")
			.WithPortBinding(8080, true)
			.WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPort(8080)))
			.Build();

		await container.StartAsync().ConfigureAwait(false);

		using var httpClient = new HttpClient();

		var requestUri = new UriBuilder(Uri.UriSchemeHttp, container.Hostname, container.GetMappedPublicPort(8080), "uuid").Uri;

		var guid = await httpClient.GetStringAsync(requestUri)
			.ConfigureAwait(false);

		Debug.Assert(Guid.TryParse(guid, out _));
	}
}