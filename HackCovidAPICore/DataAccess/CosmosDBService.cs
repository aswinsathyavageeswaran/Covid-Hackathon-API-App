using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using HackCovidAPICore.Model;
using System.Security.Cryptography;
using System.Text;

namespace HackCovidAPICore.DataAccess
{
	public class CosmosDBService :ICosmosDBService
	{
		private readonly string endPointUrl = "https://covidcosmosdb.documents.azure.com:443/";
		private readonly string primaryKey = "jSSmUe7Q6roHGd8j42YhVCkH9or3lP1rM2IKbkIocWF0NDLlrzp4TQaOldRHYwky9l23nAL6nSiRyULP6000kQ==";
		private readonly string collectionLink = "dbs/S5EfAA==/colls/S5EfAJd6FqA=";
		private DocumentClient client;

		public CosmosDBService()
		{
			try
			{
				client = new DocumentClient(new Uri(endPointUrl), primaryKey);
			}
			catch { }
		}

		public async Task<bool> Register(ShopModel schema, string password)
		{
			try
			{
				CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
				schema.PasswordHash = passwordHash;
				schema.PasswordSalt = passwordSalt;
				Document document = await client.CreateDocumentAsync(collectionLink, schema, null, false);
			}
			catch { }
			return true;
		}

		public bool UserExists(string userEmail)
		{
			return false;
		}


		/// <summary>
		/// Create a passwordhash and passwordsalt from the given password.
		/// </summary>
		/// <param name="password"></param>
		/// <param name="passwordHash"></param>
		/// <param name="passwordSalt"></param>
		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			}
		}
	}
}
