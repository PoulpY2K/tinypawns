using UnityEngine;

public class LayerTrigger : MonoBehaviour
{
    //when object exit the trigger, put it to the assigned layer and sorting layers
    //used in the stair objects for player to travel between layers

    public string layer;
    public string sortingLayer;

    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.layer = LayerMask.NameToLayer(layer);

        other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;
        var srs = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in srs)
        {
            sr.sortingLayerName = sortingLayer;
        }
    }
}