using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 大配件管理系统.NewFolder1;

namespace 大配件管理系统.models
{
    public class ReplaceInforProvider : IProvider<ReplaceInfor>
    {
        private 大配件管理系统Entities db = new 大配件管理系统Entities();

        public int Delete(ReplaceInfor t)
        {
            if (t == null) return 0;
            var model = db.ReplaceInfor.ToList().FirstOrDefault(item => t.PartsID == item.PartsID);
            if (model == null) return 0;
            db.ReplaceInfor.Remove(model);
            int count = db.SaveChanges();
            return count;
        }

        public int Insert(ReplaceInfor t)
        {
            if (t == null) return 0;
            var ad = db.CarParts.ToList().FirstOrDefault(item => t.CarID == item.CarID);
            if (ad == null) return -1;
            db.ReplaceInfor.Add(t);
            int count = db.SaveChanges();
            return count;
        }

        public List<ReplaceInfor> Select()
        {
            return db.ReplaceInfor.ToList();

        }

        public int Update(ReplaceInfor t)
        {
            using (大配件管理系统Entities db = new 大配件管理系统Entities())
            {
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }
    }
}
