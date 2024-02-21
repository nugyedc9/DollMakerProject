using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public TileBase tile;
    public Sprite image;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);


    
}
    public enum ItemType
    {
        Corss,
        Doll,
        ClothRed,
        ClothBlue,
        ClothGreen,
        ClothYellow,
        PieceClothRed,
        PieceClothBlue,
        PieceClothGreen,
        PieceClothYellow,
        FinishClothRed,
        FinishClothBlue,
        FinishClothGreen,
        FinishClothYellow,
        scisser,

    }

    public enum ActionType 
    { 
    }