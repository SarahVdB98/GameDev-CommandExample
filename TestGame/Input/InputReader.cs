using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;


namespace TestGame.Input
{
    public interface IInputReader
    {
        Vector2 ReadInput();
        bool ReadUndo();
    }


    public class KeyBoardReader : IInputReader
    {
        Vector2 move;
        public KeyBoardReader()
        {
            move = Vector2.Zero;
        }
        KeyboardState state;
        public Vector2 ReadInput()
        {
            state = Keyboard.GetState();
            move.X = 0;
            if (state.IsKeyDown(Keys.Left))
                move.X = -1;
          
            if (state.IsKeyDown(Keys.Right))
                move.X = 1;
           

            return move;
           
        }

        public bool ReadUndo()
        {
            state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.A))
                return true;

            return false;
        }
    }
}
