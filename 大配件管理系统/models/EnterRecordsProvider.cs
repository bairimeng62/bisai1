using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 大配件管理系统.NewFolder1;

namespace 大配件管理系统.models
{
    public class EnterRecordsProvider : IProvider<EnterRecords>
    {
        private 大配件管理系统Entities db = new 大配件管理系统Entities();

        public int Delete(EnterRecords t)
        {
            if (t == null) return 0;
            var model = db.EnterRecords.ToList().FirstOrDefault(item => t.PartsID == item.PartsID);
            if (model == null) return 0;
            db.EnterRecords.Remove(model);
            int count = db.SaveChanges();
            return count;
        }

        public int Insert(EnterRecords t)
        {
            if (t == null) return 0;
            if (string.IsNullOrEmpty(t.PartsID) || string.IsNullOrEmpty(t.Warehouse) || string.IsNullOrEmpty(t.PartsName)) return 0;

            
            db.EnterRecords.Add(t);
            db.Configuration.ValidateOnSaveEnabled = false;
            int count = db.SaveChanges();
            return count;
        }

        public List<EnterRecords> Select()
        {
            return db.EnterRecords.ToList();

        }

        public int Update(EnterRecords t)
        {
            using (大配件管理系统Entities db = new 大配件管理系统Entities())
            {
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }
    }
}
