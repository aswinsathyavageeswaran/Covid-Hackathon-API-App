using HackCovidAPICore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.DataAccess
{
	public interface INoteCosmosDB
	{
		Task<bool> CreateDocumentAsync<NoteModel>(NoteModel schema);
		Task<T> CreateAndReturnDocumentAsync<T>(T schema);
		Task<List<NoteModel>> GetNotes(string phoneNumber);
		Task<NoteModel> GetNote(string noteId);
		Task<bool> ReplaceDocumentAsync<NoteModel>(string selfLink, NoteModel schema);
		Task<bool> DeleteDocumentAsync(string selfLink);
		Task<List<NoteModel>> GetShopNotesAsList(string shopEmail);
	}
}
