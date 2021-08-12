using System;
using System.Threading;

namespace CSharpThreads {
    class Program
    {
        private static readonly object Monitor = new object();

        private static int _numLimit;
        static void Main() {
            Console.WriteLine("Enter a number till which odd and even numbers would be printed using Monitor: ");
            _numLimit = int.Parse(Console.ReadLine() ?? string.Empty);
            PrintNumbers();
            Console.ReadKey();
        }

        private static void PrintNumbers()
        {
            var evenThread = new Thread(PrintEven);
            var oddThread = new Thread(PrintOdd);

            evenThread.Start();
            Thread.Sleep(100);
            oddThread.Start();
        }

        private static void PrintEven()
        {
            try
            {
                System.Threading.Monitor.Enter(Monitor);

                for (var i = 0; i <= _numLimit; i += 2)
                {
                    Console.WriteLine(i);
                    System.Threading.Monitor.Pulse(Monitor);
                    Console.WriteLine("Comes after pulse");
                    var isLastItem = i >= (_numLimit);
                    if (!isLastItem)
                    {
                        System.Threading.Monitor.Wait(Monitor);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                System.Threading.Monitor.Exit(Monitor);
            }
        }

        private static void PrintOdd()
        {

            try {

                System.Threading.Monitor.Enter(Monitor);
                for (int i = 1; i <= _numLimit; i += 2) {
                    Console.WriteLine(i);
                    System.Threading.Monitor.Pulse(Monitor);

                    var isLastItem = i >= (_numLimit);
                    if (!isLastItem) {
                        System.Threading.Monitor.Wait(Monitor);
                    }
                }
            } catch (Exception) {

            } finally {
                System.Threading.Monitor.Exit(Monitor);
            }
        }
    }
}
