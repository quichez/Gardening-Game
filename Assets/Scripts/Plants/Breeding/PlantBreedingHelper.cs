using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GardeningGame.Plants
{
    public static class PlantBreedingHelper 
    {
        private static List<PlantChildren<Plant>> _listOfAllPotentailPlantChildren;
        public static IList<PlantChildren<Plant>> GetListOfPlantChildren()
        {
            return _listOfAllPotentailPlantChildren.AsReadOnly();
        }

        public static void AddToListOfPlantChildren(PlantChildren<Plant> plantChildren) => _listOfAllPotentailPlantChildren.Add(plantChildren);

    }

    public readonly struct PlantChildren<T> where T : Plant
    {
        public readonly Type firstParent;
        public readonly Type secondParent;
        public readonly List<Type> listOfPotentialChildren;
        public readonly int[] weightsForPotentialChildren;

        public PlantChildren(T firstParent, T secondParent, List<T> listOfPotentialChildren, int[] weightsForPotentialChildren)
        {
            if(weightsForPotentialChildren.Length != listOfPotentialChildren.Count)
            {
                throw new System.ArgumentException("The count of children does not match the count of weights.");
            }
                        
            this.firstParent = firstParent.GetType();
            this.secondParent = secondParent.GetType();
            this.listOfPotentialChildren = listOfPotentialChildren as List<Type>;
            this.weightsForPotentialChildren = weightsForPotentialChildren;
        }
    }

    // This is temp below this line
    public class PlantGenotype
    {

    }

    public struct ColorGene
    {
        public Color color;
        public bool IsRecessive;

        public ColorGene(Color color, bool IsRecessive = true)
        {
            this.color = color;
            this.IsRecessive = IsRecessive;
        }
    }
}
