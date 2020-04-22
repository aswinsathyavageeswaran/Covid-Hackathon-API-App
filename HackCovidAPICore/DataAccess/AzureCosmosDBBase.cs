using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.DataAccess
{
	public abstract class AzureCosmosDBBase
	{
		private readonly string endPointUrl = "https://covidcosmosdb.documents.azure.com:443/";
		private readonly string primaryKey = "jSSmUe7Q6roHGd8j42YhVCkH9or3lP1rM2IKbkIocWF0NDLlrzp4TQaOldRHYwky9l23nAL6nSiRyULP6000kQ==";
		private string collectionLink = null;
		private DocumentClient client = null;

		public AzureCosmosDBBase(string collectionLink)
		{
			try
			{
				this.collectionLink = collectionLink;
				client = new DocumentClient(new Uri(endPointUrl), primaryKey);
			}
			catch { }
		}

		public async Task<bool> CreateDocumentAsync<T>(T schema)
		{
			try
			{
				await client.CreateDocumentAsync(collectionLink, schema, null, false);
				return true;
			}
			catch { }
			return false;
		}

		public async Task<T> CreateAndReturnDocumentAsync<T>(T schema)
		{
			try
			{
				Document document = await client.CreateDocumentAsync(collectionLink, schema, null, false);
				return (dynamic)document;
			}
			catch { }
			return default;
		}

		public async Task<List<T>> CreateDocumentQueryAsList<T>(string queryString)
		{
			try
			{
				var query = client.CreateDocumentQuery<T>(collectionLink, queryString, new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true }).AsDocumentQuery();
				if (query.HasMoreResults)
				{
					var results = await query.ExecuteNextAsync<T>();
					if (results.Any())
					{
						return results.ToList();
					}
				}
			}
			catch { }
			return new List<T>();
		}

		public async Task<T> CreateDocumentQuery<T>(string queryString)
		{
			try
			{
				var query = client.CreateDocumentQuery<T>(collectionLink, queryString, new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true }).AsDocumentQuery();
				if (query.HasMoreResults)
				{
					var results = await query.ExecuteNextAsync<T>();
					if (results.Any())
					{
						return results.ToList().SingleOrDefault();
					}
				}
			}
			catch { }
			return default;
		}

		public async Task<bool> ReplaceDocumentAsync<T>(string selfLink, T schema)
		{
			try
			{
				await client.ReplaceDocumentAsync(selfLink, schema);
				return true;
			}
			catch { }
			return false;
		}

		public async Task<bool> DeleteDocumentAsync(string selfLink)
		{
			try
			{
				await client.DeleteDocumentAsync(selfLink, new RequestOptions { PartitionKey = new PartitionKey(Undefined.Value) });
				return true;
			}
			catch { }
			return false;
		}
	}
}
