using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  VIDE_Data;

public class DialogueScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameObject.FindWithTag("Player"))
        {
            gameObject.GetComponent<Template_UIManager>().Interact(GetComponent<VIDE_Assign>());
        }
    }
    public void Hobbies()
    {
        string newText;
        Dictionary<string, object> options = VD.GetExtraVariables(VD.assigned.assignedDialogue, 17);
        List<string> keys = new List<string>(options.Keys);
        int randomPick = Random.Range(0, keys.Count);
        newText = (string)options[keys[randomPick]];
        VD.SetComment(VD.assigned.assignedDialogue, 17, 0, newText);

    }

}
