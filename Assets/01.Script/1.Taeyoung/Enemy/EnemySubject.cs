using System.Collections.Generic;
using UnityEngine;

public class EnemySubject : MonoSingleTon<EnemySubject>, ISubject
{
    List<IObserver> observers = new();
    
    public void NotifyObserver()
    {
        // ERROR
        foreach (Enemy item in observers)
        {
            item.ObserverUpdate();
        }
        observers.Clear();
    }

    public void RegisterObserver(IObserver _observer)
    {
        observers.Add(_observer);
    }

    public void RemoveObserver(IObserver _observer)
    {
        observers.Remove(_observer);
    }
}
