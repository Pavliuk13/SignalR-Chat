using System;
using System.Linq;
using CreateUsers.Models;

namespace CreateUsers
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isContinue = true;
            while(isContinue)
            {
                Console.Write("Choose action (1 - create user, 2 - exit): ");
                try
                {
                    var choose = Int32.Parse(Console.ReadLine());
                    if (choose < 1 || choose > 2)
                        throw new ArgumentException("Wrong input value");

                    if (choose == 1)
                    {
                        var userName = GetUserName();
                        using (ApplicationContext context = new ApplicationContext())
                        {
                            context.Users.Add(new User()
                            {
                                UserName = userName
                            });
                            context.SaveChanges();
                        }

                        Console.WriteLine("User successfully added");
                    }
                    else
                        isContinue = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static string GetUserName()
        {
            string userName = "";
            
            while (string.IsNullOrEmpty(userName))
            {
                Console.Write("Enter user name (can't be empty): ");
                userName = Console.ReadLine();
                if (UserIsAlreadyExist(userName))
                {
                    Console.WriteLine("User is already exist! Please enter another user name!");
                    userName = "";
                }
            }
            
            return userName;
        }

        static bool UserIsAlreadyExist(string userName)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var obj = context.Users.FirstOrDefault(el => el.UserName == userName);
                if (obj == null)
                    return false;
                return true;
            }
        }
    }
}