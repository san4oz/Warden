﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.Business.Managers
{
    public class PostManager : EntityManager<Post, IPostProvider>
    {
        public PostManager(IPostProvider provider) : base(provider)
        {
        }
    }
}
