using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RoomVisibility : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fogOverlay;

    private bool _discovered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_discovered) return;
        if (!collision.TryGetComponent<PlayerMovement>(out _)) return;

        _discovered = true;
        fogOverlay.enabled = false;
    }
}
