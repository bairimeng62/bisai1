using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 大配件管理系统.NewFolder1;
using 大配件管理系统.ViewModel;

namespace 大配件管理系统.models
{
    public class PartsInforProvider : IProvider<PartsInfor>     
    {
        private 大配件管理系统Entities db = new 大配件管理系统Entities();

        public int Delete(PartsInfor t)
        {
            if (t == null) return 0;
            var model = db.PartsInfor.ToList().FirstOrDefault(item => t.PartsID == item.PartsID);
            if (model == null) return 0;
            db.PartsInfor.Remove(model);
            int count = db.SaveChanges();
            return count;
        }

        

        public int Insert(PartsInfor t)
        {
            if (t == null || string.IsNullOrEmpty(t.PartsID) || string.IsNullOrEmpty(t.PartsName) || string.IsNullOrEmpty(t.Position) || string.IsNullOrEmpty(t.Manufacturer))
            {
                return 0; // 如果基本信息为空，直接返回
            }

            var existingPart = db.PartsInfor.FirstOrDefault(item => item.PartsID == t.PartsID );
            if (existingPart != null)
            {
                return 0; // 如果已存在相同 PartsID 的记录，直接返回
            }
            db.PartsInfor.Add(t);
            int count = db.SaveChanges();
            return count; // 返回成功添加的记录数
            
            
        }


        public List<PartsInfor> Select()
        {
            return db.PartsInfor.ToList();

        }

        public int Update(PartsInfor t)
        {
            using(大配件管理系统Entities db =new 大配件管理系统Entities())
            {
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }

        
    }
}
