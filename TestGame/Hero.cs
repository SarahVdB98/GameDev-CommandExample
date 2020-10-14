using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TestGame.Animation;
using TestGame.Commands;
using TestGame.Input;
using TestGame.interfaces;

namespace TestGame
{
    public class Hero : IGameObject, ITransform
    {
        private Texture2D heroTexture;
        private Animatie animatie;
     
     
        private IInputReader inputReader;
        private IInputReader mouseReader;
        private IGameCommand moveCommand;
        private IGameCommand moveToCommand;
       

        public Vector2 Position { get; set; }

        public Hero(Texture2D texture, IInputReader inputReader)
        {
            
            heroTexture = texture;
            animatie = new Animatie();
            animatie.AddFrame(new AnimationFrame(new Rectangle(0, 0, 280, 385)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(280, 0, 280, 385)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(560, 0, 280, 385)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(840, 0, 280, 385)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(1120, 0, 280, 385)));

            //Initialize commands
            Position = new Vector2(10, 10); 
            moveCommand = new MoveCommand();
            moveToCommand = new MoveToCommand();

            //Initialize input
            this.inputReader = inputReader;
            this.mouseReader = new MouseReader();


        }

        public void Update(GameTime gameTime)
        {
            Vector2 direction = inputReader.ReadInput();

            if (inputReader.ReadUndo())
            {
                var mouseVector = mouseReader.ReadInput();
                Move(mouseVector);
            }

            if(direction !=Vector2.Zero)
                MoveHero(direction);
          
            animatie.Update(gameTime);

        }
        public void MoveHero(Vector2 direction)
        {         
            moveCommand.Execute(this,direction);   
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
