using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticsManager : MonoBehaviour
{

    // Each ID should begin with a #

    // CH = Cosmetic Head (So the type of cosmetic)
    // _TEMP_ = TEMP COLLECTION (This cosmetic belongs to the Temp Collection)
    // DEF / LIME1 = DEFAULT / LIME 1 (The cosmetic within the collection)

    /* Current cosmetics
     
    HEAD ACCESSORIES
    * Default Cosmetic (Temp)        #CH_TEMP_DEF
    * Pretty Pink Bow  (Temp)        #CH_TEMP_PINK1
    * Shallow Shores Bow (Temp)      #CH_TEMP_BLUE1

    BODY COSMETICS
    * Default Cosmetic (Temp)        #CB_TEMP_DEF
    * Agitating Red Shirt (Temp)     #CB_TEMP_RED1
    * Backstage Shirt (Temp)         #CB_TEMP_PURPLE1
    * Biting The Blue (Temp)         #CB_TEMP_BLUE1
    * Lime Shirt (Temp)              #CB_TEMP_LIME1
    * Mellow Yellow Shirt (Temp)     #CB_TEMP_YELLOW1

    LEG COSMETICS
    * Default Cosmetic (Temp)        #CL_TEMP_DEF
    * Calm Red Pants (Temp)          #CL_TEMP_RED1
    * Lime and Lime Pants (Temp)     #CL_TEMP_LIME1
    * Deep End Pants (Temp)          #CL_TEMP_BLUE1
    
    */

    public NewDressup my_new_dressup;

    public string current_cosmetics = "#DEFAULT";

    public string[] cosmetics_to_earn;
    public string[] cosmetic_names;

    public bool give_random;

    public string RewardRandomCosmetic()
    {
        string chosen = "nothinghere";
        int chosen_int = 0;
        int limit_break = 0;

        Debug.Log("Tried award");

        while (chosen == "nothinghere" && limit_break < 500)
        {
            chosen_int = Random.Range(0, cosmetics_to_earn.Length);
            chosen = cosmetics_to_earn[chosen_int];
            limit_break += 1;
        }

        if (limit_break == 500) { Debug.Log("LIMIT BROKEN"); }

        string itsname = cosmetic_names[chosen_int];
        cosmetics_to_earn[chosen_int] = "nothinghere";
        current_cosmetics += "#" + chosen;

        my_new_dressup.ReadCosmetics();

        return itsname;
    }

    // Start is called before the first frame update
    void Start()
    {
        current_cosmetics += "#DEFAULT";
    }

    // Update is called once per frame
    void Update()
    {

        if (Mind.current_tp_frames > 0) { Mind.current_tp_frames -= 1; }

        if (give_random)
        {
            give_random = false;
            RewardRandomCosmetic();
        }
    }
}
