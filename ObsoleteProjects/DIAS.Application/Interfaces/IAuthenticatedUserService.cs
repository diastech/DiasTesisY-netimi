﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DIAS.Application.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
    }
}
