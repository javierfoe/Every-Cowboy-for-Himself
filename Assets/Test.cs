using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EveryCowboyForHimself everyCowboyForHimself = new EveryCowboyForHimself(4);
        Character character;
        for(int i = 0; i < 4; i++)
        {
            character = everyCowboyForHimself.Characters[i];
            Debug.Log(character.Role + " " + character.CharacterName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
