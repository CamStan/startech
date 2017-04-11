﻿using IPGMMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPGMMS.Abstract
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetAllMembers { get; }
        IEnumerable<SelectListItem> GetLevels { get; }
        Member CreateMember();
        Member Find(int? id);
        void InsertorUpdate(Member member);
        void Delete(int id);
        void Save();
    }
}