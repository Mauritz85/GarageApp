using System;

namespace GarageApp;

public interface IVehicle
{
    string RegistrationNumber { get; }
    string Color { get; }
    int NumberOfWheels { get; }
}
public interface IHandler
{
    ParkVehicleFeedback ParkVehicle(Vehicle vehicle);
    RemoveVehicleFeedback RemoveVehicle(string regNumber);
    IEnumerable<Vehicle> GetVehicles();
    IEnumerable<(string TypeName, int Count)> GetVehicleTypeCounts();
    IEnumerable<Vehicle> SearchVehicles(string? type = null, string? color = null, int? wheels = null);
    IEnumerable<Vehicle> SearchRegNumber(string regNumber);
}
public interface IUI
{
    void Start();
    void ParkVehicle();
    void RemoveVehicle();
    void ListAllVehiclesAndTypes();
    void SearchVehicles();
    void SearchForRegNumber();
}
