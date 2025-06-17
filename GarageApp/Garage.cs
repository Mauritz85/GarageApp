using System;
using System.Collections;


namespace GarageApp;

public class Garage<T> : IEnumerable<T> where T : Vehicle
{
    private T[] parkedVehicles;
    private int capacity;
    private int amountOfParkedVehicles = 0;

    public Garage(int capacity)
    {
        this.capacity = capacity;
        parkedVehicles = new T[capacity];
    }

    public bool ParkVehicle(T vehicle)
    {
        if (amountOfParkedVehicles >= capacity || vehicle == null)
            return false;

        parkedVehicles[amountOfParkedVehicles++] = vehicle;
        return true;
    }

    public bool RemoveVehicle(string registrationNumber)
    {
        for (int i = 0; i < amountOfParkedVehicles; i++)
        {
            if (parkedVehicles[i].RegistrationNumber == registrationNumber)
            {
                for (int j = i; j < amountOfParkedVehicles - 1; j++)
                {
                    parkedVehicles[j] = parkedVehicles[j + 1];
                }

                parkedVehicles[--amountOfParkedVehicles] = null!;
                return true;
            }
        }
        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < amountOfParkedVehicles; i++)
        {
            yield return parkedVehicles[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
