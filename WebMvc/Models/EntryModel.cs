using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;

namespace WebMvc.Models
{
    public class EntryDataModel
    {
        public int Id {get;set;}
        public DateTime Timestamp {get;set;}
        public int Boarded {get;set;}
        public int LeftBehind {get;set;}
    }

    public class EntryViewModel
    {
        public int Id {get;set;}
        public DateTime Timestamp {get;set;}
        public int Boarded {get;set;}
        public int LeftBehind {get;set;}

        public static EntryViewModel FromEntry(Entry entry)
        {
            return new EntryViewModel{
                Id = entry.Id,
                Timestamp = entry.Timestamp,
                Boarded = entry.Boarded,
                LeftBehind = entry.LeftBehind
            };
        }
    }

    public class EntryCreateModel
    {
        public int Id {get;set;}
        public DateTime Timestamp {get;set;}
        public int Boarded {get;set;}
        public int LeftBehind {get;set;}

        public static EntryCreateModel CreateEntry(int id, DateTime timestamp, int boarded, int leftBehind)
        {
            return new EntryCreateModel{
                Id = id,
                Timestamp = timestamp,
                Boarded = boarded,
                LeftBehind = leftBehind
            };
        }
    }

    public class EntryUpdateModel
    {
        public int Id {get;set;}
        public DateTime Timestamp {get;set;}
        public int Boarded {get;set;}
        public int LeftBehind {get;set;}

        public static EntryUpdateModel UpdateEntry(Entry entry)
        {
            return new EntryUpdateModel{
                Id = entry.Id,
                Timestamp = entry.Timestamp,
                Boarded = entry.Boarded,
                LeftBehind = entry.LeftBehind
            };
        }
    }

    public class EntryDeleteModel
    {
        public int Id {get;set;}

        public static EntryDeleteModel DeleteEntry(int id)
        {
            return new EntryDeleteModel{
                Id = id
            };
        }
    }
}