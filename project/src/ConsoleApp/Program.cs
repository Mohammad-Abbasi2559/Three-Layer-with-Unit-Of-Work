using BLRazor.Connection;
using Models;

IConnectorBase connectorBase = new ConnectorBase();

Person person = new();
Console.WriteLine("Please Enter Your Name: ");
person.Name = Console.ReadLine();

connectorBase.Create(person);
connectorBase.Commit();

Person person1 = connectorBase.FindFirstByCondition<Person>(i => i.Name == person.Name);


if (person1 != null){
    Console.WriteLine("True");
}else
    Console.WriteLine("False");
    