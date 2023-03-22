using System;
using System.Collections.Generic;
using System.Text;
using Project.abznotebook.Entities.Interfaces;

namespace Project.abznotebook.Data.Interfaces
{
    public interface IGenericDal<Table> where Table : class, ITable, new()
    {
        void Save(Table table);
        void Delete(Table table);
        void Update(Table table);
        Table GetOrderWithId(int id);
        List<Table> GetAllOrders();
    }
}
