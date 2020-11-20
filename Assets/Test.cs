using UnityEngine;

public class Test : MonoBehaviour
{
    public int players;

    // Start is called before the first frame update
    void Start()
    {
        EveryCowboyForHimself.Setup(players);
        Character character;
        for(int i = 0; i < players; i++)
        {
            character = EveryCowboyForHimself.Characters[i];
            Debug.Log(character.Role + " " + character.CharacterName);
        }
    }
}
