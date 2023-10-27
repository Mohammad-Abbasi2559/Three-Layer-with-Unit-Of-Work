using BLRazor.Connection;
using Models;

IUnitOfWork unitOfWork = new UnitOfWork();

Person person = new();
Console.WriteLine("Please Enter Your Name: ");
person.Name = Console.ReadLine();

await unitOfWork.RepositoryBase<Person>().CreateAsync(person);
await unitOfWork.CommitAsync();

Person person1 = await unitOfWork.RepositoryBase<Person>().FindFirstByConditionAsync(i => i.Name == person.Name);

unitOfWork.Dispose();


if (person1 != null)
{
    Console.WriteLine("True");
}
else Console.WriteLine("False");
