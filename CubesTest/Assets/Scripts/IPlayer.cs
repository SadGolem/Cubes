using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public interface IPlayerController
{
    public void Move();
    public void Interaction();
}

public interface IPlayer
{
    //�� ������ ������������� ���������� IPlayer
}

public interface IInterectable 
{
    public void UseServerRpc(PlayerController player);
}
