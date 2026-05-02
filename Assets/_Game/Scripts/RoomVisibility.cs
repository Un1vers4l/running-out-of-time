using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RoomVisibility : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fogOverlay;
    [SerializeField] private float fadeDuration = 1f;

    private bool _discovered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_discovered) return;
        if (!collision.TryGetComponent<PlayerMovement>(out _)) return;

        _discovered = true;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color color = fogOverlay.color;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            fogOverlay.color = color;
            yield return null;
        }

        fogOverlay.enabled = false;
    }
}
