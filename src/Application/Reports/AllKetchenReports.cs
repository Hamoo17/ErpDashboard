using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Application.Reports.RptModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Reports
{
    public class AllKetchenReports
    {
        ICustomIUnitOfWork<int> _unitofWork;

        public AllKetchenReports(ICustomIUnitOfWork<int> unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public List<KetchenProcedureItems> RptKitchenProcedureItems(List<TbSubscrbtionDetail> q)
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
    }
}
