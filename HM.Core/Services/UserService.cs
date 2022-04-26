﻿using HM.Core.Contracts;
using HM.Core.Models;
using HM.Infrastructure.Data;
using HM.Infrastructure.Data.Identity;
using HM.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HM.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepository repo;

        public UserService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await repo.GetByIdAsync<ApplicationUser>(id);
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await repo.All<ApplicationUser>()
                .Select(u => new UserListViewModel()
                {
                    Id = u.Id,
                    Name = $"{u.FirstName} {u.LastName}",
                    Email = u.Email
                })
                .ToListAsync();
        }

        //IMPORTANT FOR DOCTOR AND PATIENT
        public async Task PopulateEntities(string role, ApplicationUser user)
        {
            if (role == "Patient")
            {
                await repo.AddAsync<Patient>(new Patient()
                {
                    Id=user.Id,
                    FirstName= user.FirstName,
                    LastName= user.LastName,
                    Email= user.Email,
                    PhoneNumber = user.PhoneNumber
                });
            }

            if (role == "Doctor")
            {
                await repo.AddAsync<Doctor>(new Doctor()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                });
            }

            else
            {
                throw new ArgumentException("Invalid role.");
            }
        }

        public async Task<bool> UpdateUser(UserEditViewModel model)
        {
            bool result = false;
            var user = await repo.GetByIdAsync<ApplicationUser>(model.Id);

            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                await repo.SaveChangesAsync();

                result = true;
            }

            return result;
        }

        public async Task<UserEditViewModel> UserEdit(string id)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);

            return new UserEditViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
