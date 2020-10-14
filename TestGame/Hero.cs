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
    public class Hero :ITransform
    {
        private Texture2D heroTexture;
        private Animatie animatie;
       
        private Vector2 snelheid;
        private Vector2 versnelling;
        private Vector2 mouseVector;
        public Vector2 Position { get; set; }


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
           // positie = new Vector2(10, 10);
            snelheid = new Vector2(1, 1);
            versnelling = new Vector2(0.1f, 0.1f);


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
      

        //private Vector2 GetMouseState()
        //{
        //    MouseState state = Mouse.GetState();
        //    mouseVector = new Vector2(state.X, state.Y);
        //    return mouseVector;
        //}

        private void Move(Vector2 mouse) 
        {
            moveToCommand.Execute(this, mouse);
           


            //if (positie.X > 600 || positie.X < 0)
            //{
            //    snelheid.X *= -1;
            //    versnelling.X *= -1;
               
            //}
            //if (positie.Y > 400 || positie.Y < 0)
            //{
            //    snelheid.Y *= -1;
            //    versnelling *= -1;
               
            //}

        }

   

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(heroTexture, Position, animatie.CurrentFrame.SourceRectangle, Color.White,0, new Vector2(0,0),0.5f, SpriteEffects.None,0);
        }

      

      

       

    }
}
