using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestClient.DataRepositoryServiceReference;
using Common.model;
using Common;
using Common.Model;


namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            DataRepositoryServiceClient client = new DataRepositoryServiceClient();
           

            client.Open();


            //bool retVal = false;

           // client.AddEntity(new User("admin", "admin"));

            //do
            //{
               
            //} while (retVal);

            int counter = 0;

            string c = string.Empty;
            string id = string.Empty;
            do
            {
                Console.WriteLine("-----Menu-----");
                Console.WriteLine("1) Add new crew member");
                Console.WriteLine("2) Get all crew members");
                Console.WriteLine("3) Add new crew");
                Console.WriteLine("4) Get all crews");
                Console.WriteLine("5) Get crew memeber by id");
                Console.WriteLine("6) Get crew by id");
                Console.WriteLine("7) Remove crew member by id");
                Console.WriteLine("8) Remove crew by id");
                Console.WriteLine("9) Get all users");
                Console.WriteLine("10) Create days");
                Console.WriteLine("11) Get day test");
                Console.WriteLine("q) Exit");
                c = Console.ReadLine();



                if (c == "1")
                {
                    CrewMember crm = new CrewMember("Pera" + (++counter), "Pap", "nja@nja.nja", "332423");
                    client.AddEntity(crm);
                    Console.WriteLine("\tEntity {0} added.", crm.ToString());
                    id = crm.GlobalId;

                }
                if (c == "2")
                {
                    List<IdentifiedObject> lista = new List<IdentifiedObject>(1);
                    lista = client.GetEntityByType(EntityType.CrewMember).ToList();
                    foreach (var item in lista)
                        Console.WriteLine(item.ToString());
                }
                if (c == "3")
                {
                    Crew crm = new Crew("Ekipa" + (++counter));
                    client.AddEntity(crm);
                    Console.WriteLine("\tEntity {0} added.", crm.ToString());
                    id = crm.GlobalId;
                }
                if (c == "4")
                {
                    List<IdentifiedObject> lista = new List<IdentifiedObject>(1);
                    lista = client.GetEntityByType(EntityType.Crew).ToList();
                    foreach (var item in lista)
                        Console.WriteLine(item.ToString());
                }
                if (c == "5")
                {
                    Console.WriteLine(client.GetEntityById(EntityType.CrewMember, id).ToString());
                }
                if (c == "6")
                {
                    Console.WriteLine(client.GetEntityById(EntityType.Crew, id).ToString());
                }
                if (c == "7")
                {
                    IdentifiedObject a = client.GetEntityById(EntityType.CrewMember, id);
                    client.RemoveEntity(a);
                    Console.WriteLine("Removed: {0}", a.ToString());
                    List<IdentifiedObject> lista = new List<IdentifiedObject>(1);
                    lista = client.GetEntityByType(EntityType.CrewMember).ToList();
                    foreach (var item in lista)
                        Console.WriteLine(item.ToString());
                }

                if (c == "8")
                {
                    IdentifiedObject a = client.GetEntityById(EntityType.Crew, id);
                    client.RemoveEntity(a);
                    Console.WriteLine("Removed: {0}", a.ToString());
                    List<IdentifiedObject> lista = new List<IdentifiedObject>(1);
                    lista = client.GetEntityByType(EntityType.Crew).ToList();
                    foreach (var item in lista)
                        Console.WriteLine(item.ToString());
                }
                if (c == "9")
                {
                    List<IdentifiedObject> lista = new List<IdentifiedObject>(1);
                    lista = client.GetEntityByType(EntityType.User).ToList();
                    foreach (var item in lista)
                        Console.WriteLine(item.ToString());
                }
                if (c == "10") 
                {
                    Console.WriteLine("Succesfully added {0} days", client.CreateWorkingDays());
                }

                if (c == "11")
                {
                    Console.WriteLine("Day:\n\t{0}", client.GetDayOfYear(new DateTime(2015,7,23)));
                }

            } while (c != "q");


            client.Close();
        }
    }
}
