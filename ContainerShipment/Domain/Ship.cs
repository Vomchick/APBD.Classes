using System.Text;
using ContainerShipment.Core.AbstractClasses;
using Console = System.Console;

namespace ContainerShipment.Domain;

public class Ship
{
    private double _currentContainerWeight = 0.0;

    public List<Container> Containers { get; }
    public double MaxSpeed { get; set; }
    public int MaxContainersCount { get; set; }
    public double MaxContainersWeight { get; set; }

    public Ship(List<Container> containers)
    {
        Containers = containers;

        if (containers.Count == 0)
            return;
        
        foreach (var container in containers)
        {
            _currentContainerWeight += container.TareWeight + container.CargoMass;
        }
    }

    public bool Load(Container container)
    {
        if (MaxContainersCount == Containers.Count)
        {
            Console.WriteLine("Ship is already filled");
            return false;
        }

        if (_currentContainerWeight + container.GetCompleteWeight() > MaxContainersWeight)
        {
            Console.WriteLine("Container is too heavy");
            return false;
        }

        Containers.Add(container);
        _currentContainerWeight += container.GetCompleteWeight();
        return true;
    }

    public void Load(List<Container> containers)
    {
        if (containers.Select(Load).All(loaded => loaded)) 
            return;

        Console.WriteLine("Container was not loaded, process stopped");
    }

    public bool Remove(Container container)
    {
        var removed = Containers.Remove(container);

        if (!removed)
        {
            Console.WriteLine("Such container was not found");
            return false;
        }

        _currentContainerWeight -= container.GetCompleteWeight();
        return true;
    }

    public bool Remove(string serialNumber)
    {
        var container = Containers.Find(x => x.SerialNumber == serialNumber);

        if (container == null)
        {
            Console.WriteLine("Such container was not found");
            return false;
        }

        return Remove(container);
    }

    public void Replace(string serialNumber, Container newContainer)
    {
        var removed = Remove(serialNumber);

        if (removed)
        {
            Load(newContainer);
        }
    }

    public bool TransferContainer(Container container, Ship otherShip)
    {
        var removed = Remove(container);

        if (!removed)
            return removed;

        var loaded = otherShip.Load(container);

        return loaded;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append($"Ship information:\n");
        builder.Append($"Container capacity: {MaxContainersCount}\n");
        builder.Append($"Current amount of containers: {Containers.Count}\n");
        builder.Append($"Maximum weight capacity: {MaxContainersWeight}\n");
        builder.Append($"Current weight of containers: {_currentContainerWeight}\n");
        builder.Append($"Maximum speed: {MaxSpeed}\n");

        foreach (var container in Containers)
        {
            builder.Append(container + "\n-----------------------------------------------------------------\n");
        }

        return builder.ToString();
    }
}
