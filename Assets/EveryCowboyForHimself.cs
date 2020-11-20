using System.Collections.Generic;
using UnityEngine;

public static class EveryCowboyForHimself
{
    public static Character[] Characters { get; private set; }
    public static Board Board { get; private set; }

    public static void Setup(int players, Role[] roles = null, CharacterName[] allowedCharacters = null)
    {
        //Initialize board
        Board = new Board();

        Characters = new Character[players];

        //Set selection of roles
        List<Role> possibleRoles;
        if(roles == null)
        {
            possibleRoles = GetRolesFromPlayers(players);
        }
        else
        {
            possibleRoles = new List<Role>();
            int length = roles.Length;
            for (int i = 0; i < length; i++)
            {
                possibleRoles.Add(roles[i]);
            }
        }

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
        int randomCharacter, randomRole;
        CharacterName character;
        Role role;
        for (int i = 0; i < players; i++)
        {
            randomCharacter = Random.Range(0, possibleCharacters.Count);
            randomRole = Random.Range(0, possibleRoles.Count);
            role = possibleRoles[randomRole];
            character = possibleCharacters[randomCharacter];
            possibleCharacters.RemoveAt(randomCharacter);
            possibleRoles.RemoveAt(randomRole);
            Characters[i] = new Character(role, character);
        }
    }

    private static List<Role> GetRolesFromPlayers(int players)
    {
        List<Role> roles = new List<Role>();
        switch (players)
        {
            case 8:
                roles.Add(Role.Renegade);
                goto case 7;
            case 7:
                roles.Add(Role.Deputy);
                goto case 6;
            case 6:
                roles.Add(Role.Outlaw);
                goto case 5;
            case 5:
                roles.Add(Role.Deputy);
                goto case 4;
            case 4:
                roles.Add(Role.Outlaw);
                roles.Add(Role.Outlaw);
                roles.Add(Role.Renegade);
                roles.Add(Role.Sheriff);
                break;
        }
        return roles;
    }
}
