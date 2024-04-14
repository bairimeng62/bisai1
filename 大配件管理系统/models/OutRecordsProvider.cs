using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 大配件管理系统.NewFolder1;

namespace 大配件管理系统.models
{
    public class OutRecordsProvider : IProvider<OutRecords>
    {
        private 大配件管理系统Entities db = new 大配件管理系统Entities();

        public int Delete(OutRecords t)
        {
            if (t == null) return 0;
            var model = db.OutRecords.ToList().FirstOrDefault(item => t.PartsID == item.PartsID);
            if (model == null) return 0;
            db.OutRecords.Remove(model);
            int count = db.SaveChanges();
            return count;
        }

        public int Insert(OutRecords t)
        {
            if (t == null) return 0;
            if (string.IsNullOrEmpty(t.PartsID) 
                || string.IsNullOrEmpty(t.PartsName)
                || string.IsNullOrEmpty(t.Warehouse)
                || string.IsNullOrEmpty(t.UserID) ) 
                return 0;
            db.OutRecords.Add(t);
            db.Configuration.ValidateOnSaveEnabled = false;
            int count = db.SaveChanges();
            return count;
        }

        public List<OutRecords> Select()
        {
            return db.OutRecords.ToList();

        }

        public int Update(OutRecords t)
        {
            db.Entry(t).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }


    }
}
