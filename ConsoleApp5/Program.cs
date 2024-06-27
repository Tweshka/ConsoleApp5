
using System;

class CustomException : Exception
{
    public CustomException(string message) : base(message) { }
}

class Program
{
    static void Main()
    {
        try
        {
            Random rand = new Random();
            int randomNumber = rand.Next(1, 6);
            switch (randomNumber)
            {
                case 1:
                    throw new DivideByZeroException();
                case 2:
                    throw new IndexOutOfRangeException();
                case 3:
                    throw new ArgumentNullException();
                case 4:
                    throw new FormatException();
                case 5:
                    throw new CustomException("Custom Exception occurred");
                default:
                    Console.WriteLine("No exception occurred");
                    break;
            }
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("DivideByZeroException caught: " + ex.Message);
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine("IndexOutOfRangeException caught: " + ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine("ArgumentNullException caught: " + ex.Message);
        }
        catch (FormatException ex)
        {
            Console.WriteLine("FormatException caught: " + ex.Message);
        }
        catch (CustomException ex)
        {
            Console.WriteLine("CustomException caught: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Finally block executed");
        }
    }
}