﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_V2
{

    public class EntityManager
    {


        //list of entity turns (entities and their ability to take turns) on the map
        private List<EntityTurn> entityTurns;
        
        
        /// <summary>
        /// Constructor method for an Entity Manager
        /// </summary>
        public EntityManager()
        {
            entityTurns = new List<EntityTurn>();
            

        }
    

        /// <summary>
        /// Mutator method that adds an entity to the list of EntityTurns in EntityManager
        /// </summary>
        /// <param name="entity">the target entity to add</param>
        public void AddEntity(Entity entity)
        {
            if (entity == null)
            {
                return;
            }

            entityTurns.Add(new EntityTurn(entity));

        }

        /// <summary>
        /// Mutator method that removes an entity to the list of EntityTurns in EntityManager
        /// </summary>
        /// <param name="entity">the target entity to remove</param>
        public void RemoveEntity(Entity entity)
        {
            foreach (EntityTurn entityTurn in entityTurns)
            {
                if (entityTurn.entity == entity)
                {
                    entityTurns.Remove(entityTurn);
                }
            }
        }

        /// <summary>
        /// Method to initialize entities from a character input (for reading entities from a map)
        /// </summary>
        /// <param name="input">the character used to represent an entity on the map</param>
        /// <returns>The Initialized Entity</returns>
        public Entity InitializeEntity(char input)
        {
            switch (input)
            {
                case '@':
                    return new Player();

                case 'm':
                    return new Mage();

                case 'f':
                    return new Warrior();

                case 's':
                    return new Skeleton();

                default:
                    return null;
            }
        }

        /// <summary>
        /// Method that instructs the EntityManager to update all entities in the EntityTurn list.
        /// </summary>
        /// <param name="map">the map the game is on</param>
        /// <param name="uIManager">the manager for the game UI</param>
        /// <param name="itemManager">the manager for the items on the map</param>
        /// <returns></returns>
        public bool UpdateEntities(Map map, UIManager uIManager, ItemManager itemManager)
        {
            for (int i = 0; i < entityTurns.Count; i++)
            {
                if (entityTurns[i].Update(map, uIManager, itemManager, this))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Accessor method that returns the player from the EntityManager.
        /// </summary>
        /// <returns>Returns the "Player" Entity</returns>
        public Entity GetPlayer()
        {
            foreach(EntityTurn entityTurn in entityTurns)
            {
                if (entityTurn.entity.GetName() == "Player")
                {
                    return entityTurn.entity;
                }
            }

            return null;
        }

        /// <summary>
        /// Method that checks Entities in the EntityManager to check if they are dead.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="uIManager"></param>
        public void CheckDeadEntities(Map map, UIManager uIManager)
        {
            for(int i=0; i<entityTurns.Count; i++)
            {
                if (entityTurns[i].entity.health.GetHp() <= 0)
                {
                    uIManager.AddEventToLog(entityTurns[i].entity.GetName() + " died.");
                    
                    map.RemoveEntity(entityTurns[i].entity);
                    entityTurns.Remove(entityTurns[i]);

                }
            }
        }
       
    }
}
