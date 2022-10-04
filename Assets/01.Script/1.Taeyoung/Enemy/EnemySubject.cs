using System.Collections.Generic;
using UnityEngine;

public class EnemySubject : MonoBehaviour, ISubject
{
    public static EnemySubject instance;

    List<IObserver> observers = new List<IObserver>();
    public void Awake()
    {
        instance = this;
    }
    public void NotifyObserver()
    {
        // ERROR
        /*foreach (LivingEntity item in observers)
        {
            item.ObserverUpdate();
        }*/
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
