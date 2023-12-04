using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ILogic 
{
    public InfScript InfOwner;
    
    public ILogic(InfScript NewOwner) 
    {
        InfOwner = NewOwner;
    }

    public bool DoISeeEnemy()
    {
        return InfOwner.CanISeeEnemy();
    }
    public void States()
    {
        if (DoISeeEnemy())
        {


            
        }
    }
}
public class TestLogicScript : ILogic
{
    public TestLogicScript(InfScript NewOwner):base(NewOwner)
    {


    }
    


}
