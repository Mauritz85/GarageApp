using GarageApp.Helpers;
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
        => garage.AddVehicle(vehicle);


    public RemoveVehicleFeedback RemoveVehicle(string regNumber)
    {
        if (string.IsNullOrWhiteSpace(regNumber))
            return RemoveVehicleFeedback.NotFound;

        bool removed = garage.RemoveVehicle(regNumber);

        return removed
            ? RemoveVehicleFeedback.Success
            : RemoveVehicleFeedback.NotFound;
    }



    public IEnumerable<Vehicle> GetVehicles()
    {
        return garage;
    }

    public IEnumerable<(string TypeName, int Count)> GetVehicleTypeCounts()
    {
        return garage
            .GroupBy(v => v.GetType().Name)
            .Select(g => (g.Key, g.Count()));
    }

    public IEnumerable<Vehicle> SearchVehicles(string? type = null, string? color = null, int? wheels = null)
    {
        return garage.Where(v =>
            (string.IsNullOrEmpty(type) || v.GetType().Name.Equals(Translate.SweToEng(type), StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(color) || v.Color.Equals(color, StringComparison.OrdinalIgnoreCase)) &&
            (!wheels.HasValue || v.NumberOfWheels == wheels.Value)
        );
    }

    public IEnumerable<Vehicle> SearchRegNumber(string regNumber)
    {
        return garage.Where(v =>
            v.RegistrationNumber.Equals(regNumber)
        );
    }


}
