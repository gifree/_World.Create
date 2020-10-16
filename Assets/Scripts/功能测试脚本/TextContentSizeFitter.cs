using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 只计算宽
/// </summary>
public class TextContentSizeFitter : MonoBehaviour
{
    public Text m_targetText;

    private string m_old;

    void Update()
    {
        if (null == m_targetText) return;

        if (m_old != m_targetText.text)
        {
            m_old = m_targetText.text;
            UpdateSize();
        }
    }

    public void UpdateSize()
    {
        if (null == m_targetText) return;

        var rtf = transform as RectTransform;
        var old = rtf.sizeDelta;
        old.x = m_targetText.preferredWidth;

        if (old.x >= m_targetText.rectTransform.sizeDelta.x) old.x = m_targetText.rectTransform.sizeDelta.x;

        rtf.sizeDelta = old;
    }
}