using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 大配件管理系统.NewFolder1;

namespace 大配件管理系统.models
{
    
    public class UserInforProvider : IProvider<UserInfor>
    {
        private 大配件管理系统Entities db = new 大配件管理系统Entities();
        public int Delete(UserInfor t)
        {
            throw new NotImplementedException();
        }

        public int Insert(UserInfor t)
        {
            throw new NotImplementedException();
        }

        public List<UserInfor> Select()
        {
            return db.UserInfor.ToList();

        }

        public int Update(UserInfor t)
        {
            throw new NotImplementedException();
        }
    }
}
