using IPGMMS.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPGMMS.Models;

namespace IPGMMS.DAL.Repositories
{
    public class EFMemberRepository : IMemberRepository
    {
        private IPGMMS_Context db;

        public EFMemberRepository(IPGMMS_Context context)
        {
            db = context;
        }

        public IEnumerable<Member> GetAllMembers()
        {
            return db.Members.ToList();
        }
    }
}