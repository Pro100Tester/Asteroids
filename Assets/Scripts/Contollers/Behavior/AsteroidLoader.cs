using System.Collections.Generic;

using UnityEngine;

[ExecuteAlways]
public class AsteroidLoader : MonoBehaviour
{
    [SerializeField, Header("–азмер колайдера")]
    public const float forBig = 2.8f, forMedium = 1f, forSmall = 0.33f;

    public const string big = "BIG", medium = "medium", small = "small";
    public static Sprite[] Sprites { get; private set; }

#if UNITY_EDITOR
    [SerializeField]
    private List<Sprite> spritesZ1, spritesZ2, spritesZ3;
#endif

    private void Awake()
    {
        Sprites = Resources.LoadAll<Sprite>("Models/Asteroids");

        //ќпределение по имени спрайт, и добавление его в лист по категории
        for (int i = 0; i < Sprites.Length; i++)
        {
            if (Sprites[i].name.Contains(big))
            {
                AsteroidSpriteKeeper.bigSprites.Add(Sprites[i]);
            }
            else if (Sprites[i].name.Contains(medium))
            {
                AsteroidSpriteKeeper.mediumSprites.Add(Sprites[i]);
            }
            else if (Sprites[i].name.Contains(small))
            {
                AsteroidSpriteKeeper.smallSprites.Add(Sprites[i]);
            }
        }
#if UNITY_EDITOR
        spritesZ1 = AsteroidSpriteKeeper.bigSprites;
        spritesZ2 = AsteroidSpriteKeeper.mediumSprites;
        spritesZ3 = AsteroidSpriteKeeper.smallSprites;
#endif
    }
}
public static class AsteroidSpriteKeeper
{
    public static List<Sprite> bigSprites = new List<Sprite>();
    public static List<Sprite> mediumSprites = new List<Sprite>();
    public static List<Sprite> smallSprites = new List<Sprite>();

}

