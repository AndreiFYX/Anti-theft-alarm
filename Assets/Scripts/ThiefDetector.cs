using System;
using UnityEngine;

public class ThiefDetector : MonoBehaviour
{
    public event Action ThiefEntered;
    public event Action ThiefExited;

    private int _thievesCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Thief>(out Thief thief) == false)
            return;

        _thievesCount++;

        if(_thievesCount == 1)
            ThiefEntered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Thief>(out Thief thief) == false)
            return;

        _thievesCount--;

        if(_thievesCount == 0)
            ThiefExited?.Invoke();
    }
}
