using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EfNewsDz
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User
            {
                Login = "anuar.temirbolat",
                Password = "asd123",
                Id=1
            };
            //using (var context = new NewsContext())
            //{
            //    var news = new News
            //    {
            //        Header = "Разбился самолет",
            //        Content = "24.12.2018 в  19:10 часов  разбился самолет летевший в Монголию," +
            //        " пострадавших нет, следствие продолжаются"
            //    };
            //    context.Users.Add(user);
            //    context.News.Add(news);
            //    context.Comments.Add(new Comment
            //    {
            //        Text = "Жалко самолета",
            //        News = news,
            //        User = user
            //    });
            //    //context.SaveChanges();
            //}

            ConsoleKeyInfo key;
            int y = 1;

            while (true)
            {
                Show(ref y);
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.DownArrow) { if (y < 3) y++; }
                else if (key.Key == ConsoleKey.UpArrow) { if (y > 1) y--; }
                else if (key.Key == ConsoleKey.Enter)
                {
                    switch (y)
                    {
                        case 1:
                            {
                                Console.Clear();
                                using (var context = new NewsContext())
                                {
                                    var news = context.News.ToList();
                                    int index = 0;
                                    foreach (var n in news)
                                    {
                                        Console.WriteLine(n.Id + " . " + n.Header);
                                        //index++;
                                    }
                                    Console.WriteLine("введите id новости");
                                    bool isTrue = false;
                                    while (isTrue == false)
                                    {
                                        try
                                        {
                                            index = int.Parse(Console.ReadLine());
                                            isTrue = true;
                                        }
                                        catch (FormatException e)
                                        {
                                            Console.WriteLine(e.Message);
                                        }
                                        catch (OverflowException e)
                                        {
                                            Console.WriteLine(e.Message);
                                        }
                                        catch (ArgumentNullException e)
                                        {
                                            Console.WriteLine(e.Message);
                                        }
                                    }
                                    Console.WriteLine("введите комментарий: ");
                                    var text = Console.ReadLine();
                                    
                                    context.Comments.Add(new Comment
                                    {
                                        Text = text,
                                        News = news[index - 1],
                                        User = user
                                    });
                                    context.SaveChanges();
                                }
                                Console.Clear();
                            }
                            break;
                        case 2:
                            {
                                Console.Clear();
                                Console.WriteLine("введите заголовок новости");
                                var head = Console.ReadLine();
                                Console.WriteLine("введите контент для новости");
                                var content = Console.ReadLine();
                                using (var context = new NewsContext())
                                {
                                    context.News.Add(new News
                                    {
                                        Header = head,
                                        Content = content
                                    });
                                    context.SaveChanges();
                                }
                                Console.Clear();
                            }
                            break;
                        case 3:
                            {
                                Console.Clear();
                                using (var context = new NewsContext())
                                {
                                    List<News> news1 = context.News.Include(c => c.Comments).ToList();
                                    foreach(var n in news1)
                                    {
                                        Console.WriteLine(n.Header);
                                        Console.WriteLine("----------------");
                                        foreach(var c in n.Comments)
                                        {
                                            var userComment = context.Users.Where(q => q.Id == c.User.Id).FirstOrDefault();                                            
                                            Console.WriteLine(userComment.Login+" : "+c.Text);                                            
                                        }
                                    }
                                }
                                Console.WriteLine("нажмите чтонибудь!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                    }
                }
            }
        }
        public static void Show(ref int y)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            if (y == 1) Console.ForegroundColor = ConsoleColor.DarkRed;
            else Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(0, 1);
            Console.Write("Создать комментарий");
            if (y == 2) Console.ForegroundColor = ConsoleColor.DarkRed;
            else Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(0, 2);
            Console.Write("Создать новость");
            if (y == 3) Console.ForegroundColor = ConsoleColor.DarkRed;
            else Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(0, 3);
            Console.Write("Показать всё");
        }
    }
}

















