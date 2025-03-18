using ContainerShipment.Core.AbstractClasses;
using ContainerShipment.Services;

namespace ContainerShipment.Domain;

public class RefrigeratedContainer : Container
{
    public double MaintainedTemperature { get; }
    public ProductType ProductType { get; }

    public RefrigeratedContainer(double height, double depth, double tareWeight, double masPayload, ProductType productType, double maintainedTemperature)
        : base(height, depth, tareWeight, masPayload, 'C')
    {
        if (TemperatureValidator.IsValid(productType, maintainedTemperature))
        {
            ProductType = productType;
            MaintainedTemperature = maintainedTemperature;
        }
    }
}