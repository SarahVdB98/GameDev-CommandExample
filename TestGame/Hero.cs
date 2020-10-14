using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TestGame.Animation;
using TestGame.Commands;
using TestGame.Input;
using TestGame.Interfaces;

namespace TestGame
{
    public class Hero :ITransform, ICollision
    {
        private Texture2D heroTexture;

        private Animatie animatie;       
       
        public Vector2 Position { get; set; }
        public Rectangle CollisinRectangle { get ; set ; }

        private IInputReader inputReader;
        private IInputReader mouseReader;

        private IGameCommand moveCommand;
        private IGameCommand moveToCommand;

       
        public Hero(Texture2D texture, IInputReader reader)
        {            
            heroTexture = texture;
            animatie = new Animatie();
            animatie.AddFrame(new AnimationFrame(new Rectangle(0, 0, 280, 385)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(280, 0, 280, 385)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(560, 0, 280, 385)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(840, 0, 280, 385)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(1120, 0, 280, 385)));    


            //Read input for my hero class
            this.inputReader = reader;
            mouseReader = new MouseReader();

            moveCommand = new MoveCommand();
            moveToCommand = new MoveToCommando();         

        }

        public void Update(GameTime gameTime)
        {

            var direction = inputReader.ReadInput();

            MoveHorizontal(direction);
         
            if(inputReader.ReadFollower())
                Move(mouseReader.ReadInput());


            animatie.Update(gameTime);

        }

        private void MoveHorizontal(Vector2 _direction)
        {
            moveCommand.Execute(this, _direction);
        }    

       

        private void Move(Vector2 mouse) 
        {
            moveToCommand.Execute(this, mouse);

        }

   

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(heroTexture, Position, animatie.CurrentFrame.SourceRectangle, Color.White,0, new Vector2(0,0),0.5f, SpriteEffects.None,0);
        }

      

      

       

    }
}
