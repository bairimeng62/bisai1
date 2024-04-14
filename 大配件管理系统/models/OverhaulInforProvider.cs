using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 大配件管理系统.NewFolder1;

namespace 大配件管理系统.models
{
    public class OverhaulInforProvider : IProvider<OverhaulInfor>
    {
        private 大配件管理系统Entities db = new 大配件管理系统Entities();

        public int Delete(OverhaulInfor t)
        {
            if (t == null) return 0;
            var model = db.OverhaulInfor.ToList().FirstOrDefault(item => t.PartsID == item.PartsID );
            if (model == null) return 0;
            db.OverhaulInfor.Remove(model);
            int count = db.SaveChanges();
            return count;
        }

        public int Insert(OverhaulInfor t)
        {
            if (t == null) return 0;
            var ad = db.CarParts.ToList().FirstOrDefault(item => t.CarID == item.CarID);
            if (ad == null) return -1;
            db.OverhaulInfor.Add(t);
            int count = db.SaveChanges();
            return count;
        }

        public List<OverhaulInfor> Select()
        {
            return db.OverhaulInfor.ToList();

        }

        public int Update(OverhaulInfor t)
        {
            throw new NotImplementedException();
        }
    }
}
