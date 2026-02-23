using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
[CreateAssetMenu(fileName = "StatusEffectSprites", menuName = "Status Effect Sprites")]
public class StatusEffectSpriteHolder : MonoBehaviour
{
    [SerializeField] public Sprite CAFFEINECRASHField;
    [SerializeField] public Sprite CAFFEINATEDField;
    [SerializeField] public Sprite BURNINGField;
    [SerializeField] public Sprite FROSTBITEField;
    [SerializeField] public Sprite PARALYZEDField;
    [SerializeField] public Sprite POISONEDField;
    [SerializeField] public Sprite ASLEEPField;
    [SerializeField] public Sprite CONFUSEDField;
    public static Sprite CAFFEINECRASH;
    public static Sprite CAFFEINATED;
    public static Sprite BURNING;// = BURNINGField;
    public static Sprite FROSTBITE;// = FROSTBITEField;
    public static Sprite PARALYZED;// = PARALYZEDField;
    public static Sprite POISONED;// = POISONEDField;
    public static Sprite ASLEEP;// = ASLEEPField;
    public static Sprite CONFUSED;
    void Start()
    {
        CAFFEINECRASH = CAFFEINECRASHField;
        CAFFEINATED = CAFFEINATEDField;
        BURNING = BURNINGField;
        FROSTBITE = FROSTBITEField;
        PARALYZED = PARALYZEDField;
        POISONED = POISONEDField;
        ASLEEP = ASLEEPField;
        CONFUSED = CONFUSEDField;
    }
}
