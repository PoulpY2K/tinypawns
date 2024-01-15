using UnityEngine;

public class LayerTrigger : MonoBehaviour
{
    //when object exit the trigger, put it to the assigned layer and sorting layers
    //used in the stair objects for player to travel between layers

    public string layer;
    public string sortingLayer;

    private void OnTriggerExit2D(Collider2D col)
    {
        col.gameObject.layer = LayerMask.NameToLayer(layer);

        col.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;
        var srs = col.gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in srs)
        {
            sr.sortingLayerName = sortingLayer;
        }
    }
}