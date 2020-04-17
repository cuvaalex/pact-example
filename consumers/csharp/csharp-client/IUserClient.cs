using APIConsumer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIConsumer
{
    interface IUserClient
    {
        Task<User> GetUser(long id);
        Task<User> CreateUser(User receivedUser);
        Task<User> UpdateUser(long id, User updatedUser);
    }
}
