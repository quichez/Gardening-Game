using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Money : Singleton<Money>
{
    public int Balance { get; private set; } = 1000;
    public UnityEvent OnBalanceChange;

    protected override void Awake()
    {
        base.Awake();
        OnBalanceChange.Invoke();
    }

    public void AddToBalance(int amt)
    {
        Balance += amt;
        OnBalanceChange.Invoke();
    }

    public void RemoveFromBalance(int amt)
    {
        Balance -= amt;
        OnBalanceChange.Invoke();

    }

}
