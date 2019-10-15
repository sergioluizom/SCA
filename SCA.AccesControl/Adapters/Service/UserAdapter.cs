﻿using Microsoft.Extensions.Configuration;
using SCA.AccesControl.Adapters.Interface;
using SCA.Model.Entities;
using SCA.Repository.Interfaces;
using SCA.Service.Adapters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.AccesControl.Adapters.Service
{
    public class UserAdapter : IUserAdapter
    {
        private readonly IUserRepository userRepository;
        private readonly IRabbitMQ rabbitMQ;
        private const string QueueUserAdd = "QueueUserAdd";
        public UserAdapter(IUserRepository userRepository, IRabbitMQ rabbitMQ)
        {
            this.userRepository = userRepository;
            this.rabbitMQ = rabbitMQ;
        }

        public async Task<bool> Add()
        {
            var user = await rabbitMQ.RetrieveSingleMessage<User>(QueueUserAdd);
            var result = await userRepository.Add(user);
            rabbitMQ.Dispose();
            return result;
        }
    }
}