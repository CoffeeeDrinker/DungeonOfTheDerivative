using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder(10)]
public class CharacterScreenMaster : MonoBehaviour
{
    [SerializeField] GameObject StatusEffectIcon;
    [SerializeField] Sprite StatusEffectSprite;
    [SerializeField] GameObject PlayerField;
    StatusEffect Status = StatusEffects.PARALYZED;
    ICombatant player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        StatusEffectIcon.GetComponent<Image>().sprite = StatusEffectSprite;
        if(player.GetStatus() == null){
          StatusEffectIcon.SetActive(false);
        }
          
     }

    // Update is called once per frame
    void Update()
    {
        if (player.GetStatus() != null)
        {
            StatusEffectIcon.SetActive(true);
            StatusEffectIcon.GetComponent<Image>().sprite = StatusEffectSprite;
        }
    }
}
