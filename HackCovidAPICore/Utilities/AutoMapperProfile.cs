using AutoMapper;
using HackCovidAPICore.Model;
using HackCovidAPICore.DTO;
using HackCovidAPICore.ResponseModel;

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
				.ForMember(dest => dest.Location, opt => opt.MapFrom<NoteDTONoteModelResolver>());

			CreateMap<DTO.Note, Model.Note>();

			CreateMap<ShopModel, Model.Shop>();

			CreateMap<ShopsModel, Shops>()
				.ForMember(dest=>dest.Shop, opt=>opt.MapFrom(src=>src.ShopModel));

			CreateMap<NoteModel, NoteInfo>();

			CreateMap<Model.Note, ResponseModel.Note>();

			CreateMap<Model.Shop, ResponseModel.Shop>();
		}
	}
}
