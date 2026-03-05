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
    public static Sprite BURNING;
    public static Sprite FROSTBITE;
    public static Sprite PARALYZED;
    public static Sprite POISONED;
    public static Sprite ASLEEP;
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
