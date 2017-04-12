using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController
{

    protected readonly GameObject character;
    protected float maxMoveSpeed;
    protected float maxTurnSpeed;

    public PlayerController(GameObject character)
    {
        this.character = character;
    }

    public abstract void moveInput();
}
