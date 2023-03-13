using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireworkCannon : FireworkCannon
{
    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if(Input.GetMouseButtonDown(1)) {
            Shoot();
        }
    }
}
