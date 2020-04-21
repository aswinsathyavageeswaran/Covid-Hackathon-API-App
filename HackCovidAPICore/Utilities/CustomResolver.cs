using AutoMapper;
using HackCovidAPICore.DTO;
using HackCovidAPICore.Model;
using HackCovidAPICore.ResponseModel;
using Microsoft.Azure.Cosmos.Spatial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackCovidAPICore.Utilities
{
	public class RegisterDTOShopModelResolver : IValueResolver<UserForRegisterDTO,ShopModel,Point>
	{
		public Point Resolve(UserForRegisterDTO registerDTO, ShopModel shopModel, Point Location, ResolutionContext context)
		{
			return new Point(registerDTO.Longitude, registerDTO.Latitude);
		}
	}
	
	public class UpdateProfileDTOShopModelResolver : IValueResolver<UpdateProfileDTO,ShopModel,Point>
	{
		public Point Resolve(UpdateProfileDTO updateProfileDTO, ShopModel shopModel, Point Location, ResolutionContext context)
		{
			return new Point(updateProfileDTO.Longitude, updateProfileDTO.Latitude);
		}
	}

	public class NoteDTONoteModelResolver : IValueResolver<NoteDTO, NoteModel, Point>
	{
		public Point Resolve(NoteDTO noteDTO, NoteModel noteModel, Point Location, ResolutionContext context)
		{
			return new Point(noteDTO.Longitude, noteDTO.Latitude);
		}
	}
}
