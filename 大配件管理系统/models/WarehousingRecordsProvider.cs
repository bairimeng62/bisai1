using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 大配件管理系统.NewFolder1;

namespace 大配件管理系统.models
{
    public class WarehousingRecordsProvider : IProvider<WarehousingRecords>
    {
        private 大配件管理系统Entities db = new 大配件管理系统Entities();

        public int Delete(WarehousingRecords t)
        {
            if (t == null) return 0;
            var model = db.WarehousingRecords.ToList().FirstOrDefault(item => t.PartsID == item.PartsID);
            if (model == null) return 0;
            db.WarehousingRecords.Remove(model);
            int count = db.SaveChanges();
            return count;
        }

        public int Insert(WarehousingRecords t)
        {
            if (t == null) return 0;
            var model = db.WarehousingRecords.ToList().FirstOrDefault(item => t.PartsID == item.PartsID&&
                                                                              t.PartsName == item.PartsName &&
                                                                              t.Warehouse == item.Warehouse &&
                                                                              t.WarehouseLocation == item.WarehouseLocation&&
                                                                              t.SafetyStockLevel == item.SafetyStockLevel &&
                                                                              t.Remarks == item.Remarks );
            if(model!=null)
            {
               if (int.TryParse(model.Number, out int existingNumber) &&
                 int.TryParse(t.Number, out int newNumber))
               {
                   // 将字符串转换为整数，然后进行加法操作
                   int totalNumber = existingNumber + newNumber;
                   model.Number = totalNumber.ToString();
                   db.SaveChanges();
               }
                return 0;
            }
            else
            {
                db.WarehousingRecords.Add(t);
                
            }
            db.Configuration.ValidateOnSaveEnabled = false;
            int count = db.SaveChanges();
            return count;
        }

        public List<WarehousingRecords> Select()
        {
            return db.WarehousingRecords.ToList();

        }

        public int Update(WarehousingRecords t)
        {
            using (var db = new 大配件管理系统Entities())
            {
                //配件出库进行调用
                if (t == null) return 0;
                var model = db.WarehousingRecords.ToList().FirstOrDefault(item => t.PartsID == item.PartsID &&
                                                                                  t.PartsName == item.PartsName &&
                                                                                  t.Warehouse == item.Warehouse &&
                                                                                  t.WarehouseLocation == item.WarehouseLocation);
                if (model != null)
                {
                    if (int.TryParse(model.Number, out int existingNumber) &&
                      int.TryParse(t.Number, out int newNumber))
                    {
                        // 将字符串转换为整数，然后进行减法操作
                        int totalNumber = existingNumber - newNumber;
                        if (totalNumber < 0) return -1;
                        model.Number = totalNumber.ToString();
                        db.SaveChanges();

                    }
                }
                else
                {
                    return -2;
                }
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges(); ;
            }

        }
    }
}
