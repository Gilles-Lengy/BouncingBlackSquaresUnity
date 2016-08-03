using UnityEngine;
using System.Collections;

public class LinearSpritesSpawner2D : MonoBehaviour {

    public GameObject spriteToDuplicate;
    public float startPositionX;
    public float StartPositionY;
    public float gapAnchorX;
    public float gapAnchorY;
    public int iteration;

    // Use this for spawning
    void Start()
    {
        // The position of the first generated sprite
        Vector2 spritePosition = new Vector2(startPositionX, StartPositionY);
        // Spawning the sprites
        for (int i = 0; i < iteration; i++)
        {
            GameObject.Instantiate(spriteToDuplicate, spritePosition, Quaternion.identity);
            spritePosition += new Vector2(gapAnchorX, gapAnchorY);
        }
        // Destroying the original sprite
        Destroy(spriteToDuplicate);
    }
}
