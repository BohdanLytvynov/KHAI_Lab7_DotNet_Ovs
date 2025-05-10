using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillainSearcher.ViewModels.Model;
using VillianSearcher.DAL.Models;

namespace VillainSearcher.Mappings.Villains
{
    internal class VillainProfile : Profile
    {
        public VillainProfile()
        {
            CreateMap<Villain, VillainViewModel>()
                .ForMember(dest => dest.Error, c=>c.Ignore())
                .ForMember(dest => dest.ShowNumber, c=>c.Ignore())
                .ForMember(dest => dest.Action, c=>c.Ignore())
                .ForMember(dest => dest.Dispatcher, c=>c.Ignore())                
                .ForMember(dest => dest.Surename, c=>c.MapFrom(src => src.Surename))
                .ForMember(dest => dest.Height, c=>c.MapFrom(src => src.Height.ToString()))
                .ForMember(dest => dest.HeadWidth, c=>c.MapFrom(src => src.HeadWidth.ToString()))
                .ForMember(dest => dest.HeadHeight, c=>c.MapFrom(src => src.HeadHeight.ToString()))
                .ForMember(dest => dest.ArmLength, c => c.MapFrom(src => src.ArmLength.ToString()))
                .ForMember(dest => dest.EyeDistance, c => c.MapFrom(src => src.EyeDistance.ToString()));

            CreateMap<VillainViewModel, Villain>()
                .ForMember(dest => dest.Surename, c => c.MapFrom(src => src.Surename))
                .ForMember(dest => dest.Height, c => c.MapFrom(src => float.Parse(src.Height)))
                .ForMember(dest => dest.HeadWidth, c=>c.MapFrom(src => float.Parse(src.HeadWidth)))
                .ForMember(dest => dest.HeadHeight, c=> c.MapFrom(src => float.Parse(src.HeadHeight)))
                .ForMember(dest => dest.ArmLength, c=>c.MapFrom(src => float.Parse(src.ArmLength)))
                .ForMember(dest => dest.EyeDistance, c => c.MapFrom(src => float.Parse(src.EyeDistance)))
                .ForSourceMember(src => src.ShowNumber, c=>c.DoNotValidate())
                .ForSourceMember(src => src.Action, c=>c.DoNotValidate())
                .ForSourceMember(src => src.Dispatcher, c=> c.DoNotValidate())
                .ForSourceMember(src => src.Error, c=> c.DoNotValidate());
                
        }
    }
}
