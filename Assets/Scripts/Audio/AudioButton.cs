using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : MonoBehaviour
{

    public void playSound(string type)
    {
        if (type == "hover")
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interface/General/UI_Main_Mouse_Hover", GetComponent<Transform>().position);
        if (type == "confirm")
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interface/General/UI_Main_Button_Confirm", GetComponent<Transform>().position);
        if (type == "decline")
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interface/General/UI_Main_Button_Decline", GetComponent<Transform>().position);
        if (type == "shop_click")
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interface/Shop/UI_Main_Shop_Item_Select", GetComponent<Transform>().position);
        if (type == "shop_buy")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interface/Shop/UI_Main_Shop_BuySell", GetComponent<Transform>().position);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Characters/Sendarr/Sendarr_Shop_Dialogue_Buying", GetComponent<Transform>().position);
        }
        if (type == "shop_sell")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interface/Shop/UI_Main_Shop_BuySell", GetComponent<Transform>().position);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Characters/Sendarr/Sendarr_Shop_Dialogue_Selling", GetComponent<Transform>().position);
        }
        if (type == "shop_leave")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interface/General/UI_Main_Button_Decline", GetComponent<Transform>().position);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Characters/Sendarr/Sendarr_Shop_Dialogue_Goodbye", GetComponent<Transform>().position);
        }
        if (type == "shop_hover")
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interface/Shop/UI_Main_Shop_Item_Hover", GetComponent<Transform>().position);
        else
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interface/General/UI_Main_Button_Click", GetComponent<Transform>().position);
    }
}
