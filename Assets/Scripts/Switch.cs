using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject ToggledObject;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // when the player collides with the switch, enable/disable ToggledObject in the scene
            ToggledObject.SetActive(!ToggledObject.activeInHierarchy);
        }
    }

}
