using ContainerShipment.Core.AbstractClasses;

namespace ContainerShipment.Domain;

public class GasContainer : HazardousContainer
{
    public double Pressure { get; }

    public GasContainer(double height, double depth, double tareWeight, double maxPayload, double pressure)
        : base(height, depth, tareWeight, maxPayload, 'G')
    {
        Pressure = pressure > 0 ? pressure : throw new ArgumentOutOfRangeException("Pressure must be greater than zero");
    }

    public override void Unload() => CargoMass *= 0.05;
}