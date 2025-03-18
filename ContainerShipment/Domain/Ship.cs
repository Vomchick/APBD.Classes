using ContainerShipment.Core.AbstractClasses;
using Console = System.Console;

namespace ContainerShipment.Domain;

public class Ship
{
    private double CurrentContainerWeight = 0.0;

    public List<Container> Containers { get; }
    public double MaxSpeed { get; set; }
    public int MaxContainersCount { get; set; }
    public double MaxContainersWeight { get; set; }

    public Ship(List<Container> containers)
    {
        Containers = containers;

        if (!containers.Any())
            return;

        foreach (var container in containers)
        {
            CurrentContainerWeight += container.TareWeight + container.CargoMass;
        }
    }

    public void AddContainer(Container container)
    {
        if (MaxContainersCount == Containers.Count)
        {
            Console.WriteLine("Ship is already filled");
            return;
        }

        Containers.Add(container);
    }
}
