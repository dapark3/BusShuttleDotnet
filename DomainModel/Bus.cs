using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel;

public class Bus
{
    public int Id {get; set;}

    public int BusNumber {get; set;}
    public Bus(int id, int busNumber)
    {
        Id = id;
        BusNumber = busNumber;
    }

    public void Update(int newBusNumber)
    {
        BusNumber = newBusNumber;
    }
}
