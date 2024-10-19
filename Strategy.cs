using System;
public interface IPaymentStrategy
{
    void Pay(double amount);
}

public class CreditCardPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата {amount} с использованием банковской карты.");
    }
}
public class PayPalPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата {amount} через PayPal.");
    }
}

public class CryptocurrencyPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата {amount} с использованием криптовалюты.");
    }
}

public class PaymentContext
{
    private IPaymentStrategy _paymentStrategy;

    public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public void ExecutePayment(double amount)
    {
        _paymentStrategy.Pay(amount);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var paymentContext = new PaymentContext();

        Console.WriteLine("Выберите способ оплаты: 1 - Карта, 2 - PayPal, 3 - Криптовалюта");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                paymentContext.SetPaymentStrategy(new CreditCardPayment());
                break;
            case "2":
                paymentContext.SetPaymentStrategy(new PayPalPayment());
                break;
            case "3":
                paymentContext.SetPaymentStrategy(new CryptocurrencyPayment());
                break;
            default:
                Console.WriteLine("Некорректный выбор.");
                return;
        }

        Console.WriteLine("Введите сумму для оплаты:");
        double amount = Convert.ToDouble(Console.ReadLine());
        paymentContext.ExecutePayment(amount);
    }
}
