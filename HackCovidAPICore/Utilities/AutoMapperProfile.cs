using AutoMapper;
using HackCovidAPICore.Model;
using HackCovidAPICore.DTO;
using HackCovidAPICore.ResponseModel;
using System.Security.Cryptography;
using Microsoft.Azure.Cosmos;

namespace HackCovidAPICore.Utilities
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<UserForRegisterDTO, ShopModel>()
				.ForMember(dest => dest.Location, opt => opt.MapFrom<RegisterDTOShopModelResolver>())
				.ForMember(dest=>dest.UserEmail, opt=>opt.MapFrom(src=>src.UserEmail.ToLower()));

			CreateMap<ShopModel, UserInfo>()
				.ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Location.Position.Longitude))
				.ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Location.Position.Latitude));

			CreateMap<UpdateProfileDTO, ShopModel>()
				.ForMember(dest => dest.Location, opt => opt.MapFrom<UpdateProfileDTOShopModelResolver>())
				.ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.UserEmail.ToLower()))
				.ForMember(dest => dest.SelfLink, opt => opt.UseDestinationValue())
				.ForMember(dest => dest.Id, opt => opt.UseDestinationValue())
				.ForMember(dest => dest.PasswordHash, opt => opt.UseDestinationValue())
				.ForMember(dest => dest.PasswordSalt, opt => opt.UseDestinationValue());

			CreateMap<NoteDTO, NoteModel>()
				.ForMember(dest => dest.Location, opt => opt.MapFrom<NoteDTONoteModelResolver>())
				.ForMember(dest=>dest.UserId,opt=>opt.MapFrom(src=>src.UserPhoneNumber));

			CreateMap<DTO.Note, Model.Note>();

			CreateMap<ShopModel, Model.Shop>()
				.ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.ShopEmail, opt => opt.MapFrom(src => src.UserEmail));

			CreateMap<ShopModel, ResponseModel.Shop>()
				.ForMember(dest=>dest.ShopId, opt=>opt.MapFrom(src=>src.Id))
				.ForMember(dest => dest.ShopStatus, opt => opt.MapFrom(src => src.Status))
				.ForMember(dest=>dest.ShopEmail, opt=>opt.MapFrom(src=>src.UserEmail));

			CreateMap<ShopsModel, ResponseModel.Shops>()
				.ForMember(dest=>dest.Shop, opt=>opt.MapFrom(src=>src.ShopModel));

			CreateMap<ShopsModel, Model.Shops>()
				.ForMember(dest => dest.Shop, opt => opt.MapFrom(src => src.ShopModel));

			CreateMap<Model.Note, ResponseModel.Note>();

			CreateMap<Model.Shop, ResponseModel.Shop>();

			CreateMap<NoteModel, NoteInfo>()
				.ForMember(dest=>dest.Notes, opt=>opt.MapFrom(src=>src.Notes))
				.ForMember(dest => dest.Shops, opt => opt.MapFrom(src => src.Shops));

			CreateMap<Model.Note, ResponseModel.Note>();

			CreateMap<NotesModel, NotesInfo>()
				.ForMember(dest => dest.NoteInfo, opt => opt.MapFrom(src => src.NoteModel));

			CreateMap<ConfirmNoteItemsDTO, NoteModel>()
				.ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes));
		}
	}
}
