using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerControler{

    protected readonly GameObject character;
    protected float maxMoveSpeed;
    protected float maxTurnSpeed;

    public PlayerControler(GameObject character)
    {
        this.character = character;
    }

    public abstract void moveInput();
}
