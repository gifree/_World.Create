//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MenuLogicController : MonoBehaviour
//{
//    private BattleCellType CellType;

//    private static MenuLogicController _self;
//    public static MenuLogicController Self => _self;

//    private void Awake()
//    {
//        _self = this;
//        curCell = Cells[index];
//    }

//    public List<CellBase> Cells;

//    private CellBase curCell;

//    public void SetChoose(BattleCellType type)
//    {
//        Debug.Log($"sss2 {type}");
//        CellType = type;
//    }

//    private int index = 1;
//    public void Switch(int dir = 0)
//    {
//        if (dir == 0) return;
//        curCell.CancelChoose();
//        index = (index + dir + 6) % 6;
//        curCell = Cells[index];
//        curCell.SetChoose();
//    }

//}

//public enum BattleCellType
//{
//    Basic,
//    Skill,
//    Magic,
//    Runner,
//    Item,
//    Setting
//}
