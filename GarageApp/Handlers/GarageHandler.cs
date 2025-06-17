using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Handlers;

public class GarageHandler : IHandler
{
    private Garage<Vehicle> garage;

    public GarageHandler(int capacity)
    {
        garage = new Garage<Vehicle>(capacity);
    }

    public ParkVehicleFeedback ParkVehicle(Vehicle vehicle)
    {
        if (garage.Any(v => v.RegistrationNumber == vehicle.RegistrationNumber))
            return ParkVehicleFeedback.DuplicateRegNumber;

        if (!garage.ParkVehicle(vehicle))
            return ParkVehicleFeedback.GarageFull;

        return ParkVehicleFeedback.Success;
    }

    public bool RemoveVehicle(string registrationNumber)
    {
        return garage.RemoveVehicle(registrationNumber);
    }


    public IEnumerable<Vehicle> ListVehicles()
    {
        return garage;
    }

    // Andra metoder: Remove, Search osv
}
