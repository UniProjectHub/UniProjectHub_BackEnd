using Application.ViewModels.BlogModelView;
using Application.ViewModels.CategoryViewModel;
using Application.ViewModels.CommentViewModel;
using Application.ViewModels.FileViewModel;
using Application.ViewModels.GroupChatViewModel;
using Application.ViewModels.MemberInTaskViewModel;
using Application.ViewModels.MemberViewModel;
using Application.ViewModels.ProjectViewModel;
using Application.ViewModels.ScheduleViewModel;
using Application.ViewModels.SubTaskViewModel;
using Application.ViewModels.TaskViewModel;
using AutoMapper;
using Domain.Models;
using File = Domain.Models.File;

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
            //Blog
            CreateMap<Blog, BlogModelView>().ReverseMap();
            CreateMap<Blog, BlogCreateModel>().ReverseMap();
            CreateMap<Blog, BlogUpdateModel>().ReverseMap();
            //Category
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            //comment
            CreateMap<Comment, CommentViewModel>().ReverseMap();

            CreateMap<File, FileViewModel>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Users.UserName))
                        .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.Task.TaskName));

            CreateMap<ScheduleViewModel, Schedule>()
    .ForMember(dest => dest.Id, opt => opt.Ignore()); // Assuming Id is an identity column
            CreateMap<Schedule, ScheduleViewModel>();

        }
    }
}