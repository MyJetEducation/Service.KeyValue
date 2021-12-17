using System;
using System.Text.Json;
using System.Threading.Tasks;
using ProtoBuf.Grpc.Client;
using Service.Core.Grpc.Models;
using Service.KeyValue.Client;
using Service.KeyValue.Grpc;
using Service.KeyValue.Grpc.Models;

namespace TestApp
{
	internal class Program
	{
		private static async Task Main()
		{
			GrpcClientFactory.AllowUnencryptedHttp2 = true;

			Console.Write("Press enter to start");
			Console.ReadLine();

			var factory = new KeyValueClientFactory("http://localhost:5001");
			IKeyValueService client = factory.GetKeyValueRepository();

			var userId1 = Guid.NewGuid();

			Console.WriteLine($"{Environment.NewLine}Put new values to user {userId1}");
			CommonGrpcResponse putResult1 = await client.Put(new ItemsPutGrpcRequest
			{
				UserId = userId1,
				Items = new[]
				{
					new KeyValueGrpcModel {Key = "key1", Value = "value1"},
					new KeyValueGrpcModel {Key = "key2", Value = "value2"},
					new KeyValueGrpcModel {Key = "key3", Value = "value3"}
				}
			});
			LogData(putResult1);

			var userId2 = Guid.NewGuid();

			Console.WriteLine($"{Environment.NewLine}Put new values to user {userId2}");
			CommonGrpcResponse putResult2 = await client.Put(new ItemsPutGrpcRequest
			{
				UserId = userId2,
				Items = new[]
				{
					new KeyValueGrpcModel {Key = "key1", Value = "value4"},
					new KeyValueGrpcModel {Key = "key2", Value = "value5"},
					new KeyValueGrpcModel {Key = "key3", Value = "value6"}
				}
			});
			LogData(putResult2);

			Console.WriteLine($"{Environment.NewLine}Get all user {userId2} keys");
			KeysGrpcResponse keysGrpcResponse = await client.GetKeys(new GetKeysGrpcRequest {UserId = userId2});
			LogData(keysGrpcResponse);

			string[] keys1 = {"key1", "key2", "some_key"};
			Console.WriteLine($"{Environment.NewLine}Get values for user {userId1}, keys {JsonSerializer.Serialize(keys1)}");
			ItemsGrpcResponse getResult1 = await client.Get(new ItemsGetGrpcRequest {UserId = userId1, Keys = keys1});
			LogData(getResult1);

			string[] keys2 = {"key2", "key3"};
			Console.WriteLine($"{Environment.NewLine}Get values for user {userId2}, keys {JsonSerializer.Serialize(keys2)}");
			ItemsGrpcResponse getResult2 = await client.Get(new ItemsGetGrpcRequest {UserId = userId2, Keys = keys2});
			LogData(getResult2);

			string[] keys3 = {"key1", "key3"};
			Console.WriteLine($"{Environment.NewLine}Delete values for user {userId1}, keys {JsonSerializer.Serialize(keys3)}");
			CommonGrpcResponse deletetResult = await client.Delete(new ItemsDeleteGrpcRequest {UserId = userId1, Keys = keys3});
			LogData(deletetResult);

			string[] keys4 = {"key1", "key2", "key3"};
			Console.WriteLine($"{Environment.NewLine}Get values for user {userId1}, keys {JsonSerializer.Serialize(keys4)}");
			ItemsGrpcResponse getResult3 = await client.Get(new ItemsGetGrpcRequest {UserId = userId1, Keys = keys4});
			LogData(getResult3);

			Console.WriteLine($"{Environment.NewLine}Change values for user {userId2}, keys {JsonSerializer.Serialize(keys2)}");
			CommonGrpcResponse putResult3 = await client.Put(new ItemsPutGrpcRequest
			{
				UserId = userId2,
				Items = new[]
				{
					new KeyValueGrpcModel {Key = "key2", Value = "new_value4"},
					new KeyValueGrpcModel {Key = "key3", Value = "new_value5"}
				}
			});
			LogData(putResult3);

			Console.WriteLine($"{Environment.NewLine}Get values for user {userId2}, keys {JsonSerializer.Serialize(keys2)}");
			ItemsGrpcResponse getResult4 = await client.Get(new ItemsGetGrpcRequest {UserId = userId2, Keys = keys2});
			LogData(getResult4);

			Console.WriteLine("End");
			Console.ReadLine();
		}

		private static void LogData(object data) => Console.WriteLine(JsonSerializer.Serialize(data));
	}
}