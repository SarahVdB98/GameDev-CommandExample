using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace TestGame.interfaces
{
    interface IGameObject
    {

      
        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}
