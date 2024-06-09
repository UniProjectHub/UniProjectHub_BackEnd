using Application.ViewModels.GroupChatViewModel;
using Application.ViewModels.MemberViewModel;
using Application.ViewModels.ProjectViewModel;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.ViewModels.ProjectViewModel.ProjectViewModel;

namespace Infracstructures.Mappers
{
    public class MapperConfigs : Profile
    {
        public MapperConfigs()
        {
            CreateMap<Member, MemberViewModel>().ReverseMap();
            CreateMap<GroupChat, GroupChatViewModel>().ReverseMap();
            //Project
            CreateMap<Project, ProjectViewModel>().ReverseMap();
            
        }
    }
}