using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestGame.interfaces;

namespace TestGame.Commands
{
    public class MoveToCommand : IGameCommand
    {
        Vector2 speed;
        public void Execute(ITransform transform, Vector2 direction)
        {
            var directionToGo = Vector2.Add(direction, -transform.Position);
            directionToGo.Normalize();
            directionToGo = Vector2.Multiply(directionToGo, 0.1f);

            speed += directionToGo;
            speed = Limit(speed, 10);
            transform.Position += speed;
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        private Vector2 Limit(Vector2 v, float max)
        {
            if (v.Length() > max)
            {
                var ratio = max / v.Length();
                v.X *= ratio;
                v.Y *= ratio;
            }
            return v;
        }
    }
}
