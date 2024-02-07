using homeSchool.core;
using School.model.Response;
using Subscriber.Data.DTO;
using Subscriber.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subsciber.CORE.interfaceBL
{
    public interface ICardSubscriberService
    {
        Task<BaseResponseGeneral<int>> Login(string email, string password);
        Task<BaseResponseGeneral<SubscriberResponse>> GetSubscriberById(int id);
        Task<BaseResponseGeneral<bool>> Register(SubscriberDTO subscriberDTO);

    }
}
