using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GardeningGame
{
    namespace Items
    {
        public abstract class Item
        {
            public override abstract string ToString();
        }

        public interface IStackable
        {
            int quantity { get; }
            int maxStack { get; }
            bool IsFull => quantity >= maxStack;
            int RemainingValue => maxStack - quantity;
            void AddToStack(int amount);
            bool RemoveFromStack(int amount);
            void Fill();
        }

        public interface IQuality
        {
            int quality { get; }
        }

        public interface IQualityDegrade
        {
            float timeToDegrade { get; }
            float degradeTimer { get; }

            public void AddToDegradeTimer();
        }
    }

}