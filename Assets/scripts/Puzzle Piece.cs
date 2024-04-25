using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public void SetupPiece(Sprite fullImage, Rect spriteRect)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = Sprite.Create(fullImage.texture, spriteRect, new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
}
