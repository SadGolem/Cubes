using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerController
{
    public void Move();
    public void Interaction();
}

public interface IPlayer
{
    //����� �������������� �����-�� 
}

public interface IInterectable
{
    public void Use(Transform transform);
}
