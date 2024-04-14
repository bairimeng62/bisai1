using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 大配件管理系统.NewFolder1;
using 大配件管理系统.ViewModel;


namespace 大配件管理系统.models
{
    public class CarPartsProvider : IProvider<CarParts>
    {
        private 大配件管理系统Entities db = new 大配件管理系统Entities();

        public int Delete(CarParts t)
        {
            if (t == null) return 0;
            var model = db.CarParts.ToList().FirstOrDefault(item => t.CarID == item.CarID &&
                                                                    t.PartsID == item.PartsID &&
                                                                    t.PartsName == item.PartsName &&
                                                                    t.ChangeDate == item.ChangeDate &&
                                                                    t.Life == item.Life);
            if (model == null) return 0;
            db.CarParts.Remove(model);
            int count = db.SaveChanges();
            return count;
        }

        public int Insert(CarParts t)
        {
            if (t == null) return 0;
            var model = db.CarParts.ToList().FirstOrDefault(item => t.CarID == item.CarID &&
                                                                    t.PartsID == item.PartsID &&
                                                                    t.PartsName == item.PartsName);
            if (model != null) return 0;
            db.CarParts.Add(t);
            int count = db.SaveChanges();
            return count;
        }

        public List<CarParts> Select()
        {
            return db.CarParts.ToList();

        }

        public int Update(CarParts t)
        {
            using (var db = new 大配件管理系统Entities())
            {
                var model = db.CarParts.FirstOrDefault(item => t.CarID == item.CarID &&
                                                                 t.PartsID == item.PartsID &&
                                                                 t.PartsName == item.PartsName);

                if (model != null)
                {
                    if (t.Life == null)
                    {
                        // 更新实体的特定属性
                        model.NextChangeDate = t.NextChangeDate;
                        model.ChangeDate = t.ChangeDate;
                    }
                    if (t.NextChangeDate == null)
                    {
                        // 更新实体的特定属性
                        model.Life = t.Life;
                    }
                    model.EarlyWarning = t.EarlyWarning;
                    model.EarlyWarningColor = t.EarlyWarningColor;
                    // 将实体的状态标记为修改
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;

                    // 保存更改并返回受影响的行数
                    return db.SaveChanges();
                }
                else
                {
                    // 如果未找到要更新的实体，则返回 0 表示没有更新任何内容
                    return 0;
                }
            }

        }
    }
}
