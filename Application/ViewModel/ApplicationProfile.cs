using Application.ViewModel.ElementsViewModel;
using Application.ViewModel.GenerationRequest;
using Application.ViewModel.PagesViewModel;
using Application.ViewModel.ProjectsViewModel;
using Application.ViewModel.SettingsViewModel;
using Application.ViewModel.StyleRulesViewModel;
using Application.ViewModel.UsersViewModel;
using AutoMapper;
using Domain.AccountEntity;
using Domain.Entities;

namespace Application.ViewModel
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            MapUsers();
            MapProjects();
            MapSettings();
            MapPages();
            MapElements();
            MapStyleRules();
        }

        private void MapUsers()
        {
            CreateMap<User, UserDto>();

            CreateMap<User, UserProfileDto>()
                .ForMember(dest => dest.ProjectsCount,
                    opt => opt.MapFrom(
                        src => src.Projects != null ? src.Projects.Count() : 0));

            CreateMap<RegisterRequest, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Projects, opt => opt.Ignore());
        }

        private void MapProjects()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
                    src.User != null ? src.User.Username : string.Empty))
                .ForMember(dest => dest.PagesCount, opt => opt.MapFrom(src =>
                    src.Pages != null ? src.Pages.Count() : 0));

            CreateMap<CreateProjectRequest, Project>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description)
                    )
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Pages, opt => opt.Ignore())
                .ForMember(dest => dest.Settings, opt => opt.Ignore())
                .ForMember(dest => dest.StyleRules, opt => opt.Ignore());

            CreateMap<UpdateProjectRequest, Project>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description)
                    )
                .ForAllMembers(
                opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }

        private void MapSettings()
        {
            CreateMap<Settings, SettingsDto>()
                .ForMember(dest => dest.SettingsId, opt => opt.MapFrom(src => src.SettingsId))
                .ForMember(
                    dest => dest.IncludeDefaultCss,
                    opt => opt.MapFrom(src => src.IncludeDefaultCss)
                    )
                .ForMember(dest => dest.ResetCss, opt => opt.MapFrom(src => src.ResetCss))
                .ForMember(dest => dest.Font, opt => opt.MapFrom(src => src.Font))
                .ForMember(
                    dest => dest.ProjectType,
                    opt => opt.MapFrom(src => src.ProjectType)
                    )
                .ForMember(
                    dest => dest.BootstrapVersion,
                    opt => opt.MapFrom(src => src.BootstrapVersion)
                    )
                .ForMember(
                    dest => dest.IncludeBootstrapIcons,
                    opt => opt.MapFrom(src => src.IncludeBootstrapIcons)
                    )
                .ForMember(
                    dest => dest.IncludeBootstrapJS,
                    opt => opt.MapFrom(src => src.IncludeBootstrapJS)
                    )
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));

            CreateMap<CreateSettingsRequest, Settings>()
                .ForMember(
                    dest => dest.IncludeDefaultCss,
                    opt => opt.MapFrom(src => src.IncludeDefaultCss)
                    )
                .ForMember(dest => dest.ResetCss, opt => opt.MapFrom(src => src.ResetCss))
                .ForMember(dest => dest.Font, opt => opt.MapFrom(src => src.Font ?? "Arial"))
                .ForMember(
                    dest => dest.ProjectType,
                    opt => opt.MapFrom(src => src.ProjectType)
                    )
                .ForMember(
                    dest => dest.BootstrapVersion,
                    opt => opt.MapFrom(src => src.BootstrapVersion ?? "5.3.0")
                    )
                .ForMember(
                    dest => dest.IncludeBootstrapIcons,
                    opt => opt.MapFrom(src => src.IncludeBootstrapIcons)
                    )
                .ForMember(
                    dest => dest.IncludeBootstrapJS,
                    opt => opt.MapFrom(src => src.IncludeBootstrapJS)
                    );

            CreateMap<UpdateSettingsRequest, Settings>()
                .ForMember(
                    dest => dest.IncludeDefaultCss,
                    opt => opt.MapFrom(src => src.IncludeDefaultCss)
                    )
                .ForMember(dest => dest.ResetCss, opt => opt.MapFrom(src => src.ResetCss))
                .ForMember(dest => dest.Font, opt => opt.MapFrom(src => src.Font))
                .ForMember(
                    dest => dest.ProjectType,
                    opt => opt.MapFrom(src => src.ProjectType)
                    )
                .ForMember(
                    dest => dest.BootstrapVersion,
                    opt => opt.MapFrom(src => src.BootstrapVersion)
                    )
                .ForMember(
                    dest => dest.IncludeBootstrapIcons,
                    opt => opt.MapFrom(src => src.IncludeBootstrapIcons)
                    )
                .ForMember(
                    dest => dest.IncludeBootstrapJS,
                    opt => opt.MapFrom(src => src.IncludeBootstrapJS)
                    )
                .ForAllMembers(
                    opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }

        private void MapPages()
        {
            CreateMap<Page, PageDto>()
                .ForMember(
                    dest => dest.ElementsCount,
                    opt => opt.MapFrom(
                        src => src.Elements != null ? src.Elements.Count() : 0)
                    )
                .ForMember(
                    dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.UtcNow)
                    );

            CreateMap<CreatePageRequest, Page>()
                .ForMember(dest => dest.PageName, opt => opt.MapFrom(src => src.PageName))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.Elements, opt => opt.Ignore());

            CreateMap<UpdatePageRequest, Page>()
                .ForMember(dest => dest.PageName, opt => opt.MapFrom(src => src.PageName))
                .ForAllMembers(
                    opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Page, PageDetailsDto>()
                .ForMember(dest => dest.StyleRules, opt => opt.MapFrom(src =>
                    src.Elements != null ? src.Elements
                    .SelectMany(e => e.StyleRules ?? new List<StyleRule>())
                     .ToList() : new List<StyleRule>())
                );
        }

        private void MapElements()
        {
            CreateMap<ElementDto, Element>()
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
                .ForMember(dest => dest.StyleRules, opt => opt.MapFrom(src => src.StyleRules))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src =>
                    src.Attributes != null ? src.Attributes.Select(a => new Attributes
                    {
                        Key = a.Key,
                        Value = a.Value
                    }).ToList() : null
                ));

            CreateMap<Element, ElementDto>()
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
                .ForMember(dest => dest.StyleRules, opt => opt.MapFrom(src => src.StyleRules))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src =>
                    src.Attributes != null ? src.Attributes.ToDictionary(a => a.Key, a => a.Value) : null
                ));

            CreateMap<CreateElementRequest, Element>()
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
                .ForMember(dest => dest.StyleRules, opt => opt.MapFrom(src => src.StyleRules))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src =>
                    src.Attributes != null ? src.Attributes.Select(a => new Attributes
                    {
                        Key = a.Key,
                        Value = a.Value
                    }).ToList() : null
                ));

            CreateMap<UpdateElementRequest, Element>()
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
                .ForMember(dest => dest.StyleRules, opt => opt.MapFrom(src => src.StyleRules))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src =>
                    src.Attributes != null ? src.Attributes.Select(a => new Attributes
                    {
                        Key = a.Key,
                        Value = a.Value
                    }).ToList() : null
                ));
        }

        private void MapStyleRules()
        {
            CreateMap<CreateStyleRuleRequest, StyleRule>()
                .ForMember(dest => dest.Selector, opt => opt.MapFrom(src => src.Selector))
                .ForMember(dest => dest.Rule, opt => opt.MapFrom(src =>
                    string.Join("; ", src.Rules.Select(r => $"{r.Key}: {r.Value}"))))
                .ForMember(dest => dest.Rules, opt => opt.MapFrom(src =>
                    string.Join("; ", src.Rules.Values)))
                .ForMember(dest => dest.RuleType, opt => opt.MapFrom(src => src.RuleType));

            CreateMap<UpdateStyleRuleRequest, StyleRule>()
                .ForMember(dest => dest.StyleRuleId, opt => opt.MapFrom(src => src.StyleRuleId))
                .ForMember(dest => dest.Selector, opt => opt.MapFrom(src => src.Selector))
                .ForMember(dest => dest.Rules, opt => opt.MapFrom(src =>
                    src.Rules != null ? string.Join("; ", src.Rules.Select(r => $"{r.Key}: {r.Value}")) : null))
                .ForMember(dest => dest.Rules, opt => opt.MapFrom(src =>
                    src.Rules != null ? string.Join("; ", src.Rules.Values) : null))
                .ForMember(dest => dest.RuleType, opt => opt.MapFrom(src => src.RuleType))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<StyleRuleDto, StyleRule>()
                .ForMember(dest => dest.StyleRuleId, opt => opt.MapFrom(src => src.StyleRuleId))
                .ForMember(dest => dest.Selector, opt => opt.MapFrom(src => src.Selector))
                .ForMember(dest => dest.Rule,
                    opt => opt.MapFrom(src => src.Rules ?? new Dictionary<string, string>())
                    )
                .ForMember(dest => dest.RuleType, opt => opt.MapFrom(src => src.RuleType));

            CreateMap<StyleRule, StyleRuleDto>()
                .ForMember(dest => dest.StyleRuleId, opt => opt.MapFrom(src => src.StyleRuleId))
                .ForMember(dest => dest.Selector, opt => opt.MapFrom(src => src.Selector))
                .ForMember(dest => dest.Rules,
                    opt => opt.MapFrom(src => src.Rule ?? new Dictionary<string, string>())
                    )
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}