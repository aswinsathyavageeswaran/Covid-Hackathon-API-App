using HackCovidAPICore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HackCovidAPICore.DataAccess
{
	public class NoteCosmosDB : AzureCosmosDBBase, INoteCosmosDB
	{
		public NoteCosmosDB(string noteCosmosDBLink) : base(noteCosmosDBLink) { }

		public async Task<List<NoteModel>> GetNotes(string phoneNumber)
		{
			string query = $"SELECT * FROM c WHERE c.UserId = '{phoneNumber}'";
			return await CreateDocumentQueryAsList<NoteModel>(query);
		}

		public async Task<NoteModel> GetNote(string noteId)
		{
			string query = $"SELECT * FROM c WHERE c.id = '{noteId}'";
			return (await CreateDocumentQuery<NoteModel>(query));
		}
	}
}
