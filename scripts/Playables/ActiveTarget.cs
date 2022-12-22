using UnityEngine;

public class ActiveTarget : MonoBehaviour
{
    public string target_name;

    public virtual void Activate()
    {

    }

    public virtual string ActiveTargetName()
    {
        return target_name;
    }
}
