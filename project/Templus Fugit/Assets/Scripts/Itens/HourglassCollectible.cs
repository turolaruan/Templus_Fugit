using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourglassCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // se conseguiu colocar no inventário, destrói só este pick-up
        if (GameManager.Instance.AddInventoryItem(ItemType.Hourglass))
            Destroy(gameObject);
    }
}
