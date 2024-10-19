using System;
using System.Collections.Generic;
public interface IObserver
{
    void Update(string currency, double rate);
}
public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}

public class CurrencyExchange : ISubject
{
    private readonly List<IObserver> _observers = new List<IObserver>();
    private double _usdRate;

    public double UsdRate
    {
        get => _usdRate;
        set
        {
            _usdRate = value;
            Notify(); 
        }
    }

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update("USD", _usdRate);
        }
    }
}
public class User : IObserver
{
    public string Name { get; }

    public User(string name)
    {
        Name = name;
    }

    public void Update(string currency, double rate)
    {
        Console.WriteLine($"Пользователь {Name} получил обновление: Курс {currency} - {rate}");
    }
}
public class Application : IObserver
{
    public void Update(string currency, double rate)
    {
        Console.WriteLine($"Приложение получило обновление: Курс {currency} - {rate}");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var currencyExchange = new CurrencyExchange();

        var user1 = new User("Лидия");
        var user2 = new User("Мария");
        var application = new Application();

        currencyExchange.Attach(user1);
        currencyExchange.Attach(user2);
        currencyExchange.Attach(application);

        Console.WriteLine("Изменение курса USD:");
        currencyExchange.UsdRate = 75.5;

        currencyExchange.Detach(user1);
        Console.WriteLine("Изменение курса USD:");
        currencyExchange.UsdRate = 76.0;
    }
}
