using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ENB.Test.Logistics.Entities;
using ENB.Logistics.Mvc.Models;

namespace ENB.Logistics.Mvc.App_Start
{
    public class LogisticsProfiles : Profile
    {
        public LogisticsProfiles()
        {
            
            

                #region Operator 
                CreateMap<Operator, DisplayOperator>();

                CreateMap<CreateAndEditOperator, Operator>()
                  .ForMember(d => d.DateCreated, t => t.Ignore())
                  .ForMember(d => d.DateModified, t => t.Ignore());                
                CreateMap<Operator, CreateAndEditOperator>();
                #endregion


               

                #region E-mail addresses

                CreateMap<EmailAddress, DisplayEmailAddress>()
                      .ForMember(d => d.OperatorId, t => t.MapFrom(y => y.OwnerId));
                CreateMap<EmailAddress, CreateAndEditEmailAddress>()
                      .ForMember(d => d.OperatorId, t => t.MapFrom(y => y.OwnerId));
                CreateMap<CreateAndEditEmailAddress, EmailAddress>()
                      .ForMember(d => d.OwnerId, t => t.Ignore())
                      .ForMember(d => d.Owner, t => t.Ignore());

                #endregion


                #region Phone number

                CreateMap<PhoneNumber, DisplayPhoneNumber>()
                 .ForMember(d => d.OperatorId, t => t.MapFrom(y => y.OwnerId));
                CreateMap<PhoneNumber, CreateAndEditPhoneNumber>()
                          .ForMember(d => d.OperatorId, t => t.MapFrom(y => y.OwnerId));
            CreateMap<CreateAndEditPhoneNumber, PhoneNumber>()
                      .ForMember(d => d.OwnerId, t => t.Ignore())
                      .ForMember(d => d.Owner, t => t.Ignore());

                #endregion

                #region Order Picking
               CreateMap<OrderPicking, DisplayOrderPicking>()
                    .ForMember(d => d.OperatorId, t => t.MapFrom(y => y.OwnerId));
                CreateMap<OrderPicking, CreateAndEditOrderPicking>()
                            .ForMember(d => d.OperatorId, t => t.MapFrom(y => y.OwnerId));
                CreateMap<CreateAndEditOrderPicking, OrderPicking>()
                         .ForMember(d => d.DateCreated, t => t.Ignore())
                         .ForMember(d => d.DateModified, t => t.Ignore());

                #endregion
                
            
           
        }

       
    }
}