using homeSchool.core;
using School.model.Response;
using Subscriber.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subsciber.CORE.interfaceDAL
{
    public interface ICardSubscriberRepository
    {
        Task<BaseResponseGeneral<int>>Login(string email, string password);
        Task<BaseResponseGeneral<SubscriberResponse>>GetSubscriberById(int id);
        Task<BaseResponseGeneral<bool>>Register(Subscribers subscibers,double height);
        bool IsEmailExists(string email);
        bool IsIdCardExist(int id);

    }
}
