using ContainerShipment.Core.AbstractClasses;
using ContainerShipment.Domain;
using ContainerShipment.Domain.Enums;

var gasContainer = new GasContainer(200, 1000, 250, 2000, 10);
var liquidContainer = new LiquidContainer(200, 1000, 250, 2000, false);
var refrigiratedContainer = new RefrigeratedContainer(200, 1000, 250, 2000, ProductType.Bananas, 14);

gasContainer.LoadCargo(900);
liquidContainer.LoadCargo(900);
refrigiratedContainer.LoadCargo(900);

var ship = new Ship(100, 1000, 1_000_000);
ship.Load(gasContainer);

var containerList = new List<Container>() { liquidContainer, refrigiratedContainer };
ship.Load(containerList);

ship.Remove(refrigiratedContainer);

liquidContainer.Unload();

var serialNumberOfGasContainer = gasContainer.SerialNumber;
ship.Replace(serialNumberOfGasContainer, refrigiratedContainer);

var ship2 = new Ship(100, 10, 10_000);

ship.TransferContainer(refrigiratedContainer, ship2);

Console.WriteLine(gasContainer);
Console.WriteLine(liquidContainer);
Console.WriteLine(refrigiratedContainer);

Console.WriteLine(ship);
Console.WriteLine(ship2);
