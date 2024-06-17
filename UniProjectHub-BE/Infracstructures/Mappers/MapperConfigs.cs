using Application.ViewModels.FileViewModel;
using Application.ViewModels.GroupChatViewModel;
using Application.ViewModels.MemberViewModel;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Domain.Models.File;

namespace Infracstructures.Mappers
{
    public class MapperConfigs : Profile
    {
        public MapperConfigs()
        {
            CreateMap<Member, MemberViewModel>().ReverseMap();
            CreateMap<GroupChat, GroupChatViewModel>().ReverseMap();
            CreateMap<File, FileViewModel>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Users.UserName))
            .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.Task.TaskName));
        }
    }
}