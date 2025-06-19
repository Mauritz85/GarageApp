using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp;

public enum ParkVehicleFeedback
{
    Success,
    DuplicateRegNumber,
    GarageFull,
    InvalidVehicle
}

public enum RemoveVehicleFeedback
{
    Success,
    NotFound
}
