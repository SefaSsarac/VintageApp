using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageApp.Business.DataProtection;
using VintageApp.Business.Operations.User.Dtos;
using VintageApp.Business.Types;
using VintageApp.Data.Entities;
using VintageApp.Data.Enums;
using VintageApp.Data.Repositories;
using VintageApp.Data.UnitOfWork;

namespace VintageApp.Business.Operations.User
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtection _protector;

        public UserManager(IUnitOfWork unitOfWork, IRepository<UserEntity> userRepository, IDataProtection protector)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _protector = protector;
        }

        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == user.Email.ToLower());

            if (hasMail.Any())
            {
                return new ServiceMessage
                {
                    IsSuccess = false,
                    Message = "Email adresi mevcut"
                };
            }

            var userEntity = new UserEntity()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = _protector.Protect(user.Password),
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
                UserType = UserType.Customer,                
            };

            _userRepository.Add(userEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Inner exception'ı fırlat
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                throw new Exception($"Kullanıcı kaydı sırasında bir hata oluştu: {innerExceptionMessage}");
            }

            return new ServiceMessage
            {
                IsSuccess = true,
            };
        }

        public ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user)
        {
            var userEntity = _userRepository.Get(x => x.Email.ToLower() == user.Email.ToLower());

            if (userEntity is null)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSuccess = false,
                    Message = "Kullanıcı adı ya da şifre hatalı"
                };
            }
            
            var unprotectedText = _protector.UnProtect(userEntity.Password);

            if (unprotectedText == user.Password)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSuccess = true,
                    Data = new UserInfoDto
                    {
                        Email = userEntity.Email,
                        FirstName = userEntity.FirstName,
                        LastName = userEntity.LastName,
                        UserType = userEntity.UserType,
                    }
                };
            }
            else
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSuccess = false,
                    Message = "Kullanıcı adı ya da şifre hatalı"
                };
            }              
        }
    }
}
