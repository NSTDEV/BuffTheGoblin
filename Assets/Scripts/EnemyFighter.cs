using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighter : Fighter
{
    void Awake()
    {
        this.stats = new Stats(23, 30, 70, 45, 20 );
    }
    public override void InitTurn()
    {

    }
}
