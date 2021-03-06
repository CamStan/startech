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
        IEnumerable<Member> NewMembers { get; }
        IEnumerable<Member> ExpiringMembers { get; }
        IEnumerable<Member> ExpiredMembers { get; }
        IEnumerable<SelectListItem> GetLevels { get; }
        Member CreateMember();
        Member Find(int? id);
        Member FindByIdentityID(string id);
        Member FindByIPG_ID(string id);
        Member InsertorUpdate(Member member);
        void Delete(int id);
        void Save();
        string getCountry(int id);
        string setMemberNumber(Member memb);
        string setMemberNumber(Member memb, ContactInfo info);
        int GetMemberLevelID(string level);
        int[] GetActiveMemberIDs();
        int GetActiveMemberCount();
        int GetNewMemberCount();
    }
}