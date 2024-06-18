using Application.ViewModels.FileViewModel;
using Application.ViewModels.GroupChatViewModel;
using Application.ViewModels.MemberViewModel;
using Application.ViewModels.ProjectViewModel;
using Application.ViewModels.TaskViewModel;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Domain.Models.File;
using static Application.ViewModels.ProjectViewModel.ProjectViewModel;
using Application.ViewModels.SubTaskViewModel;
using Application.ViewModels.MemberInTaskViewModel;

namespace Infracstructures.Mappers
{
    public class MapperConfigs : Profile
    {
        public MapperConfigs()
        {
            CreateMap<Member, MemberViewModel>().ReverseMap();
            CreateMap<GroupChat, GroupChatViewModel>().ReverseMap();
            CreateMap<File, FileViewModel>().ReverseMap();
            //Project
            CreateMap<Project, ProjectViewModel>().ReverseMap();
            //Task
            CreateMap<Domain.Models.Task, TaskViewModel>().ReverseMap();
            //Subtask
            CreateMap<SubTask, SubTaskViewModel>().ReverseMap();
            CreateMap<SubTask, CreateSubTaskRequest>().ReverseMap();
            CreateMap<SubTask, UpdateSubTaskRequest>().ReverseMap();
            //MemberInTask
            CreateMap<MemberInTask, MemberInTaskViewModel>().ReverseMap();
            CreateMap<MemberInTask, MemberInTaskCreateModel>().ReverseMap();
            CreateMap<MemberInTask, MemberInTaskUpdateModel>().ReverseMap();

        }
    }
}