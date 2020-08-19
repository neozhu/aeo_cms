using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WebApp.Models;
using WebApp.Models.Dto;

namespace WebApp
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      //var map = CreateMap<Order, Order>();
      //CreateMap<Order, OrderDetail>()
      //  .ForMember(x => x.OrderId, opt => opt.MapFrom(x => x.Id));
      CreateMap<CreateProductViewModel, Product>();
      CreateMap<Attachment, ProductPricture>();
      CreateMap<Company, CompanyTreeItem>();
      CreateMap<CreateAeoQuestionTestDto, AeoAuthTest>();
      CreateMap<QuestionTpl, AeoQuestion>();
      CreateMap<InquiryTask, Inquiry>();
      CreateMap<InquiryTaskProduct, InquiryProduct>();
    }
  }
   
}