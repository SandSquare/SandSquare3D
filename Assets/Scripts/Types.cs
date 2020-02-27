using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Get access to enum states by 'using Types;'
namespace Types
{
    public enum PlayerState
    {
        Idle,
        Jab,
        Hold,
    }

    public enum Movement
    {
        Stunned,
        Moving,
        Idle,
    }

    public enum GameState
    {
        MainMenu,
        JoinGame,
        Game
    }

}
