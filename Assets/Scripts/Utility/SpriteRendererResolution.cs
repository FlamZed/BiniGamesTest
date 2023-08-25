using UnityEngine;

namespace Utility
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRendererResolution : MonoBehaviour
    {
        private void Update()
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            float worldScreenHeight = Camera.main.orthographicSize * 2;
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
            transform.localScale = new Vector3(worldScreenWidth / sr.sprite.bounds.size.x,
                worldScreenHeight / sr.sprite.bounds.size.y);
        }

        private Vector2 CalculateResolution(Camera mainCamera, float cameraHeight, SpriteRenderer spriteRenderer)
        {
            Vector2 cameraSize = new Vector2(mainCamera.aspect * cameraHeight, cameraHeight);
            Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
            Vector2 scale = transform.localScale;

            if (cameraSize.x >= cameraSize.y)
                scale *= cameraSize.x / spriteSize.x;
            else
                scale *= cameraSize.y / spriteSize.y;
            return scale;
        }
    }
}
