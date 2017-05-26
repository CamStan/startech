using IPGMMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPGMMS.ViewModels
{
    public class MemberReports
    {
        public int MemberCount { get; set; }
        public int NewMemberLast30Count { get; set; }
        public int ActiveMemberCount { get; set; }
        public int ExpiredMembersCount { get; set; }
        public int ExpiringMembersCount { get; set; }
        public int NewMembersCount { get; set; }
        public IEnumerable<Member> ExpiredMembers { get; set; }
        public IEnumerable<Member> ExpiringMembers { get; set; }
        public IEnumerable<Member> NewMembers { get; set; }
    }
}