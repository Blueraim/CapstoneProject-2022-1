using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DashButton : MonoBehaviour//, IPointerDownHandler, IPointerUpHandler 
{
    private MovementObject player;

    private void Start() {
        Invoke("FindPlayer", 0.5f);
    }

    private void FindPlayer(){
        player = FindObjectOfType<MovementObject>();
    }

    public void ButtonDown(){
       /* Debug.Log("Button Down");*/
        player.TryRun();
    }

    public void ButtonUp(){
        /*Debug.Log("Button Up");*/
        player.RunningCancle();
    }

    // private void start() {
    //     player = FindObjectOfType<MovementObject>();
    // }

    // public void OnPointerDown (PointerEventData eventData)
    // {
    //     player.TryRun();
    // }
 
    // public void OnPointerUp (PointerEventData eventData)
    // {
    //     player.RunningCancle();
    // }
}
