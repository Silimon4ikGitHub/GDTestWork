using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBoss : Enemie
{
    public GameObject[] childrens;

    public override void Die()
    {
        foreach (var child in childrens)
        {
           var newChild = Instantiate(child,transform.position, transform.rotation);
        }
        SceneManager.Instance.RemoveEnemie(this);
        isDead = true;
        AnimatorController.enabled = false;
        foreach (var rb in rigidbodys)
        {
            rb.AddForce(transform.position * PushForse);
        }
    }
}
