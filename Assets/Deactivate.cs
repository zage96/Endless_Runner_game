using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    bool deactivateScheduled = false;
    void CancelDeactivation()
    {
        CancelInvoke();
    }


    //@@private void OnEnable()
    //@@{
    //@@    PlayerController.OnPlayerDeath += CancelDeactivation;
    //@@}
    //@@
    //@@private void OnDisable()
    //@@{
    //@@    PlayerController.OnPlayerDeath -= CancelDeactivation;
    //@@}


    void OnCollisionExit(Collision player)
    {
        if (PlayerController.isDead)
        {
            deactivateScheduled = false;
            return;
        }   
        if (player.gameObject.tag == "Player" && !deactivateScheduled)
        {
            Invoke("SetInactive", 5.0f);
            deactivateScheduled = true;
        }

    }

    void SetInactive()
    {
        deactivateScheduled = false;
        this.gameObject.SetActive(false);
    }
}
