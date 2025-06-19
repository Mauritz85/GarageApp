using System;
using System.Collections;


namespace GarageApp;

public class Garage<T> : IEnumerable<T> where T : Vehicle
{
    private readonly T[] parkedVehicles;
    public int Capacity { get; }
    public int Count { get; private set; }

    public Garage(int capacity)
    {
        Capacity = capacity;
        parkedVehicles = new T[capacity];
    }

 public ParkVehicleFeedback AddVehicle(T vehicle)
{
    if (vehicle == null)
        return ParkVehicleFeedback.InvalidVehicle;

    if (Count >= Capacity)
        return ParkVehicleFeedback.GarageFull;

    if (this.Any(v => v.RegistrationNumber.Equals(vehicle.RegistrationNumber, StringComparison.OrdinalIgnoreCase)))
        return ParkVehicleFeedback.DuplicateRegNumber;

    parkedVehicles[Count++] = vehicle;
    return ParkVehicleFeedback.Success;
}


    public bool RemoveVehicle(string registrationNumber)
    {
        for (int i = 0; i < Count; i++)
        {
            if (parkedVehicles[i].RegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase))
            {
                for (int j = i; j < Count - 1; j++)
                {
                    parkedVehicles[j] = parkedVehicles[j + 1];
                }

                parkedVehicles[--Count] = null!;
                return true;
            }
        }
        return false;
    }


    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return parkedVehicles[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
