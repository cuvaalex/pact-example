using APIConsumer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIConsumer
{
    interface IUserClient
    {
        User GetUserById(Guid id);
        Task<User> CreateUser(User receivedUser);
        Task<User> UpdateUser(Guid id, User updatedUser);
    }
}
