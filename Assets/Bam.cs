using System.Collections.Generic;

public class Bam
{
    public Character[] Characters { get; private set; }
    public Board Board { get; private set; }

    public Bam(int players, Role[] roles, CharacterName[] allowedCharacters = null)
    {
        Characters = new Character[players];

        //Set selection of characters
        List<CharacterName> possibleCharacters = new List<CharacterName>();
        if (allowedCharacters == null)
        {
            for (int i = 0; i < Character.Amount; i++)
            {
                possibleCharacters.Add((CharacterName)i);
            }
        }
        else
        {
            int length = allowedCharacters.Length;
            for (int i = 0; i < length; i++)
            {
                possibleCharacters.Add(allowedCharacters[i]);
            }
        }

        //Choose random characters and roles
        for (int i = 0; i < players; i++)
        {

        }
    }
}
