using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DomainModel;
using WebMvc.Models;

namespace WebMvc.Service
{
    public class BusService : IBusService
    {
        var db = new BusContext();

        public List<Bus> GetAllBuses()
        {
            var db = new BusContext();
            return db.Buses.Select(b 
            => new BusService(b.ID, b.BusNumber)).toList();
        }

        public Bus? FindBusByID(int id)
        {
            return this.GetAllBuses().Find(b => b.ID == id);
        }

        public void UpdateBusByID(int id)
        {
            var db = new TaskContext(); 

            List<MyTask> tasks = this.GetAllTasks();

            var existingTask = tasks.Find(t => t.Id == id);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            existingTask.Update(title, content, dueDate);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var task = db.Tasks.SingleOrDefault(t => t.Id == existingTask.Id);

            if(task!=null)
            {
                task.Title = title;
                task.Content = content;
                task.DueDate = dueDate;
                db.SaveChanges();
            }
        }
        }

        public int GetNumBuses()
        {

        }

        public void CreateNewBus(int id, int BusNumber)
        {

        }
    }
}