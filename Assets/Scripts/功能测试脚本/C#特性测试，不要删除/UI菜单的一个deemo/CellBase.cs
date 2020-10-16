//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class CellBase : MonoBehaviour
//{
//    private Button _btn;
//    private MenuLogicController _controller;
//    public BattleCellType CellType;
//    private UICircle _uiCircle;


//    private void Awake()
//    {
//        _btn = GetComponent<Button>();
//        _btn?.onClick.AddListener(OnBtnClick);
//        _controller = _controller != null ? _controller : (_controller = GetComponentInParent<MenuLogicController>());
//        _uiCircle = GetComponentInChildren<UICircle>();
//    }

//    public void SetChoose()
//    {
//        var color = _uiCircle.color;
//        _uiCircle.color = new Color(color.r, color.g, color.b, 255f);
//    }

//    public void CancelChoose()
//    {
//        Debug.Log($"cancel choose");
//        var color = _uiCircle.color;
//        _uiCircle.color = new Color(color.r, color.g, color.b, 125);
//    }

//    protected void OnBtnClick() => MenuLogicController.Self.SetChoose(CellType);

//    public void RotDuring(int dir, float velocity) => transform.Rotate(Vector3.forward, dir * velocity);

//    public void RefreshPos(Vector2 pos) => transform.localPosition = pos;
//}
