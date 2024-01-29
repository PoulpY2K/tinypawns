using UnityEngine;

public class LayerTrigger : MonoBehaviour
{
    // Quand l'objet sort du trigger, on le met dans le bon layer et sorting layer
    // Utilisé sur les escaliers quand les personnages traversent les layers

    [Header("Layers")] public string layer;
    public string sortingLayer;

    private void OnTriggerExit2D(Collider2D col)
    {
        UpdateLayer(col);

        // On tente d'obtenir le SpriteRenderer enfant et on assigne la variable s'il existe
        if (!col.gameObject.TryGetComponent<SpriteRenderer>(out var colSr)) return;
        UpdateSortingLayer(col, colSr);
    }

    private void UpdateLayer(Collider2D col)
    {
        col.gameObject.layer = LayerMask.NameToLayer(layer);
        foreach (Transform tr in col.transform)
        {
            tr.gameObject.layer = LayerMask.NameToLayer(layer);
        }
    }

    private void UpdateSortingLayer(Collider2D col, SpriteRenderer colSr)
    {
        colSr.sortingLayerName = sortingLayer;
        var srs = col.gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in srs)
        {
            sr.sortingLayerName = sortingLayer;
        }
    }
}