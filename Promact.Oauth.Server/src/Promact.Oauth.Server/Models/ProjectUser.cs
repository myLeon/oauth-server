﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Promact.Oauth.Server.Models
{
    public class ProjectUser : OAuthBase
    {
        public Project Project { get; set; }
        public ApplicationUser User { get; set; }
    }
}