using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Interfaces.Services;
using ErpDashboard.Application.Models;
using ErpDashboard.Application.Reports.RptModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ErpDashboard.Application.Reports.RptEnums.ClssRptEnums;

namespace ErpDashboard.Application.Reports
{
    public class AllKetchenReports
    {
        ICustomIUnitOfWork<int> db;
        ICurrentUserService _userService;

        public AllKetchenReports(ICustomIUnitOfWork<int> _db, ICurrentUserService userService)
        {
            db = _db;
            _userService = userService;
        }

        public List<KetchenProcedureItems> RptKitchenProcedureRows(List<TbSubscrbtionDetail> q)
        {
            var ItemsList = new List<KetchenProcedureItems>();

            foreach (var line in q)
            {
                foreach (var item in line.Meal.TbMealsLines)
                {

                    if (item.ItemType != 4 && item.ItemType != 5)
                    {

                        var KetItem = new KetchenProcedureItems()
                        {
                            ItemName = item.ItemName,
                            Qty = item.Qty,
                            UnitName = item.UnitName
                        };
                        ItemsList.Add(KetItem);
                    }
                    else
                    {
                        if (item.Item.TbItemComponentsHdrs.Count != 0)
                        {
                            foreach (var item_hdr in item.Item.TbItemComponentsHdrs.FirstOrDefault().TbItemComponentsLines)
                            {
                                var KetItem = new KetchenProcedureItems()
                                {
                                    ItemName = item_hdr.ItemName,
                                    Qty = item_hdr.Qty,
                                    UnitName = item_hdr.UnitName
                                };
                                ItemsList.Add(KetItem);
                            }
                        }
                    }
                }
            }
            return ItemsList.OrderBy(c => c.ItemName).ToList();
        }
        public List<KetchenProcedureItems> RptKitchenProcedureItems(List<TbSubscrbtionDetail> q , ItemsOrProcessRpt itemsOrProcess)
        {
            var meals_lines = new List<KetchenProcedureItems>();
            foreach (var item in q)
            {
                item.Meal.TbMealsLines.ToList().ForEach(l =>
                {
                    var KetchenProcedureItems = new KetchenProcedureItems()
                    {
                        ItemName = l.ItemName,
                        Qty = l.Qty,
                        UnitName = l.UnitName,
                        ItemId = l.ItemId,
                    };
                    meals_lines.Add(KetchenProcedureItems);
                });
            }

            if (itemsOrProcess == ItemsOrProcessRpt.Process)// 0- item name (salamon) , 1- meal line name (salamon 1 pcs)
            {
                meals_lines.ForEach(m => m.ItemName = m.ItemName + " " + m.Qty.ToString("0") + m.UnitName);
            }
            return meals_lines;
        }

        public List<KetchenProcedureItems> RptKitchenProcedureDept(List<TbMealsLine> meals_lines)
        {
            var Lines = new List<KetchenProcedureItems>();
            var items_dept = getAllItemDept();
            foreach (var dept in GetDepartments().Where(c => c.Name != "").ToList())
            {
                foreach (var line in meals_lines)
                {
                    if (items_dept.Any(c => c.ItemId == line.ItemId && c.DeptId == dept.Id))
                    {

                        var line_item = new KetchenProcedureItems()
                        {

                            ItemName = line.ItemName,
                            Qty = line.Qty,
                            UnitName = line.UnitName,
                            DepartmentName = dept.Name
                        };
                        Lines.Add(line_item);
                    }
                }
            }
            return Lines;
        }
        public List<TbItemsDepartment> getAllItemDept()
        {
            return db.Repository<TbItemsDepartment>().Entities.ToList();
        }
        public List<TbDepartment> GetDepartments()
        {
            return db.Repository<TbDepartment>().Entities.Where(c => c.ComId == _userService.CompanyID).ToList();
        }
    }
}
