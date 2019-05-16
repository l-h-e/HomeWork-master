using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : Manager<MapController>
{
    public Sprite OutdoorSprite;
    public Sprite Floor1Sprite;
    public Sprite Floor2Sprite;

    SpriteRenderer MapSpriteRenderer;
    Sprite[] MapSprites;
    int nowMap;

    void Start() {
        MapSpriteRenderer        = GetComponent<SpriteRenderer>();
        MapSprites               = new Sprite[] {OutdoorSprite, Floor1Sprite, Floor2Sprite};
        nowMap                   = 0;   

        MapSpriteRenderer.sprite = MapSprites[nowMap];
    }

    void Update() {
    }

    public void nextMap() {
        nowMap += 1;
        MapSpriteRenderer.sprite = MapSprites[nowMap];
        Debug.Log("Map ID" + nowMap);
    }
}
