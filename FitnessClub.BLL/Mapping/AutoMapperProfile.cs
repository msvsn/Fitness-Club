using AutoMapper;
using FitnessClub.BLL.DTO;
using FitnessClub.DAL.Models;
using System;

namespace FitnessClub.BLL.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Маппінг FitnessClub <-> FitnessClubDTO
            CreateMap<DAL.Models.FitnessClub, FitnessClubDTO>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
            CreateMap<FitnessClubDTO, DAL.Models.FitnessClub>();

            // Маппінг Location <-> LocationDTO
            CreateMap<Location, LocationDTO>();
            CreateMap<LocationDTO, Location>();

            // Маппінг ClassType <-> ClassTypeDTO
            CreateMap<ClassType, ClassTypeDTO>();
            CreateMap<ClassTypeDTO, ClassType>();

            // Маппінг ClassSchedule <-> ClassScheduleDTO
            CreateMap<ClassSchedule, ClassScheduleDTO>()
                .ForMember(dest => dest.ClassType, opt => opt.MapFrom(src => src.ClassType))
                .ForMember(dest => dest.FitnessClub, opt => opt.MapFrom(src => src.FitnessClub))
                .ForMember(dest => dest.Trainer, opt => opt.MapFrom(src => src.Trainer));
            CreateMap<ClassScheduleDTO, ClassSchedule>();

            // Маппінг ClassRegistration <-> ClassRegistrationDTO
            CreateMap<ClassRegistration, ClassRegistrationDTO>()
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
                .ForMember(dest => dest.ClassSchedule, opt => opt.MapFrom(src => src.ClassSchedule))
                .ForMember(dest => dest.Visit, opt => opt.MapFrom(src => src.Visit));
            CreateMap<ClassRegistrationDTO, ClassRegistration>();

            // Маппінг Client <-> ClientDTO
            CreateMap<Client, ClientDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<ClientDTO, Client>();

            // Маппінг Trainer <-> TrainerDTO
            CreateMap<Trainer, TrainerDTO>();
            CreateMap<TrainerDTO, Trainer>();

            // Маппінг MembershipType <-> MembershipTypeDTO
            CreateMap<MembershipType, MembershipTypeDTO>();
            CreateMap<MembershipTypeDTO, MembershipType>();

            // Маппінг MembershipCard <-> MembershipCardDTO
            CreateMap<MembershipCard, MembershipCardDTO>()
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
                .ForMember(dest => dest.MembershipType, opt => opt.MapFrom(src => src.MembershipType))
                .ForMember(dest => dest.HomeClub, opt => opt.MapFrom(src => src.HomeClub));
            CreateMap<MembershipCardDTO, MembershipCard>();

            // Маппінг Visit <-> VisitDTO
            CreateMap<Visit, VisitDTO>()
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
                .ForMember(dest => dest.FitnessClub, opt => opt.MapFrom(src => src.FitnessClub))
                .ForMember(dest => dest.MembershipCard, opt => opt.MapFrom(src => src.MembershipCard))
                .ForMember(dest => dest.ClassRegistration, opt => opt.MapFrom(src => src.ClassRegistration));
            CreateMap<VisitDTO, Visit>();

            // Маппінг ApplicationUser <-> UserDTO
            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(dest => dest.Role, opt => opt.Ignore()); // Роль встановлюється окремо
            CreateMap<UserDTO, ApplicationUser>();

            // Маппінг RegisterClientDTO <-> Client
            CreateMap<RegisterClientDTO, Client>();
            
            // Маппінг RegisterClientDTO <-> ApplicationUser
            CreateMap<RegisterClientDTO, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
} 