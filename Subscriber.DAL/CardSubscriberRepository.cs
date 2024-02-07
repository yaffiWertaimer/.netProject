
using homeSchool.core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using School.model.Response;
using Subsciber.CORE.DTO;
using Subsciber.CORE.interfaceDAL;
using Subscriber.Data;
using Subscriber.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.DAL
{
    public class CardSubscriberRepository : ICardSubscriberRepository
    {
        readonly WeightWatcherContext _w;
        public CardSubscriberRepository(WeightWatcherContext w)
        {
            _w = w;
        }
        public async Task<BaseResponseGeneral<int>> Login(string email, string password)
        {
            try
            {
                Subscribers subscriber = _w.Subscribers.Where(p => p.Email == email && p.Password == password).FirstOrDefault();

                if (subscriber != null)
                {
                    return new BaseResponseGeneral<int>
                    {
                        Success = true,
                        Message = "התחברות הצליחה",
                        Data = subscriber.Id
                    };
                }
                else
                {
                    return new BaseResponseGeneral<int>
                    {
                        Success = false,
                        Message = "שם משתמש או סיסמה שגויים",
                        Data = -1
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" error 401 Login Failed");
            }
        }
        public async Task<BaseResponseGeneral<SubscriberResponse>> GetSubscriberById(int id)
        {

            try
            {
                BaseResponseGeneral<SubscriberResponse> response = new BaseResponseGeneral<SubscriberResponse>();
                Card card = _w.Card.Where(p => p.Id == id).FirstOrDefault();
                Subscribers subscribers = _w.Subscribers.Where(p => p.Id == card.SubscriberId).FirstOrDefault();
                response.Success = true;
                response.Message = "התחברות הצליחה";
                response.Data = new SubscriberResponse();
                response.Data.FirstName = subscribers.FirstName;
                response.Data.LastName = subscribers.LastName;
                response.Data.weight = card.Weight;
                response.Data.height = card.Height;
                response.Data.BMI = card.BMI;
                return response;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve subscriber");
            } 

        }
        public async Task<BaseResponseGeneral<bool>> Register(Subscribers subscibers, double height)
        {
            try
            {
                BaseResponseGeneral<bool> res = new BaseResponseGeneral<bool>();
                _w.Subscribers.AddAsync(subscibers);
               await _w.SaveChangesAsync();
                Card newCard = new Card
                {
                    OpenDate = DateTime.Now,
                    UpDate = DateTime.Now,
                    BMI = 0,
                    Height = height,
                    SubscriberId= subscibers.Id,

                };
                 _w.Card.AddAsync(newCard);
                await _w.SaveChangesAsync();
                res.Success = true;
                res.Message = "המנוי נוסף בהצלחה";
                res.Data = true;

                return res;
            }
            catch (Exception)
            {
                throw new Exception("הרשום נכשל");
            }
        }
        public bool IsEmailExists(string email)
        {
            return  _w.Subscribers.Any(p => p.Email == email);
        }
        public bool IsIdCardExist(int id)
        {
            return _w.Subscribers.Any(p => p.Id == id);
        }
    }
}

