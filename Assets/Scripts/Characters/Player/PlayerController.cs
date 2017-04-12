using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController
{

    protected readonly GameObject character;


    protected float maxMoveSpeed;

    public float MaxMoveSpeed
    {
        get { return maxMoveSpeed; }
        set { maxMoveSpeed = value; }
    }

    protected float maxTurnSpeed;

    public float MaxTurnSpeed
    {
        get { return maxTurnSpeed; }
        set { maxTurnSpeed = value; }
    }

    public PlayerController(GameObject character)
    {
        this.character = character;
    }

    public abstract void moveInput();
}
