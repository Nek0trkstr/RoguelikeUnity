using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] m_HeartImages;
    public Sprite m_FullHeartSprite;
    public Sprite m_HalfHeartSprite;
    public Sprite m_EmptyHeartSprite;
    public FloatValue m_InitialPlayerHealth;
    public FloatValue m_PlayerHealth;
    
    private void Start()
    {
        for (int i = 0; i < m_InitialPlayerHealth.m_RuntimeValue / 2; i++)
        {
            m_HeartImages[i].sprite = m_FullHeartSprite;
            m_HeartImages[i].enabled = true;
        }
    }

    public void OnHealthChange()
    {
        int fullHeartsAmount = (int)m_PlayerHealth.m_RuntimeValue / 2;
        int halfHeartsAmount = (int)m_PlayerHealth.m_RuntimeValue - fullHeartsAmount * 2;
        for (int i = 0; i < (int)m_InitialPlayerHealth.m_RuntimeValue / 2; i++)
        {
            if (fullHeartsAmount > 0)
            {
                m_HeartImages[i].sprite = m_FullHeartSprite;
                fullHeartsAmount--;
            }
            else if (halfHeartsAmount > 0)
            {
                m_HeartImages[i].sprite = m_HalfHeartSprite;
                halfHeartsAmount--;
            }
            else
            {
                m_HeartImages[i].sprite = m_EmptyHeartSprite;
            }
        }
    }
}
