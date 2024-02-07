using AutoMapper;
using homeSchool.core;
using School.model.Response;
using Subsciber.CORE.interfaceBL;
using Subsciber.CORE.interfaceDAL;
using Subscriber.Data.DTO;
using Subscriber.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Subsciber.CORE.model
{
    public class CardSubscriberService : ICardSubscriberService
    {
        readonly ICardSubscriberRepository _csr;
        readonly IMapper _mapper;
        public CardSubscriberService(ICardSubscriberRepository csr, IMapper mapper)
        {
            _csr = csr;
            _mapper = mapper;
        }

        public async Task<BaseResponseGeneral<int>> Login(string email, string password)
        {

            if (ValidateEmail(email) && ValidatePassword(password))
            {
                return await _csr.Login(email, password);
            }
            return new BaseResponseGeneral<int> { Data = 0, Success = false, Message = "invalid email or password" };
        }
        public async Task<BaseResponseGeneral<SubscriberResponse>> GetSubscriberById(int id)
        {
            if (!_csr.IsIdCardExist(id))
            {
                return new BaseResponseGeneral<SubscriberResponse>
                {
                    Success = false,
                    Message = "card id doesn't exist",
                    Data = null
                };
            }
            return await _csr.GetSubscriberById(id);

        }
        public async Task<BaseResponseGeneral<bool>> Register(SubscriberDTO subscriberDTO)
        {
            BaseResponseGeneral<bool> response = new BaseResponseGeneral<bool>();
            Subscribers subscriber = _mapper.Map<Subscribers>(subscriberDTO);
            double height = subscriberDTO.Height;
            try
            {
                if (!ValidateEmail(subscriberDTO.Email) || !ValidatePassword(subscriberDTO.Password) ||_csr.IsEmailExists(subscriberDTO.Email))
                {
                    response.Success = false;
                    response.Message = "הנתונים שהזנת לא חוקיים";
                    response.Data = false;
                    return response;
                }
                return await _csr.Register(subscriber, height);
            }
            catch (Exception)
            {

                throw new Exception("subscriber failed");
            }

        }
        static bool ValidateEmail(string email)
        {
            string pattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";
            return Regex.IsMatch(email, pattern);
        }
        static bool ValidatePassword(string password)
        {
            string pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_\-+=]).{8,}$";
            return Regex.IsMatch(password, pattern);
        }
    }
}
