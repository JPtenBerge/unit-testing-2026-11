using AwesomeAssertions;
using Npgsql;
using Testcontainers.PostgreSql;

namespace DemoProject.Tests;

[TestClass]
public class PostgresTestContainer
{
	[TestMethod]
	public async Task DoContainerStuff()
	{
		await using var postgres = new PostgreSqlBuilder().Build();
		await postgres.StartAsync();

		await using var connection = new NpgsqlConnection(postgres.GetConnectionString());
		await connection.OpenAsync();

		await using var command = new NpgsqlCommand("SELECT 'hi';", connection);
		var result = await command.ExecuteScalarAsync();
		result.Should().Be("hqi");
	}
}