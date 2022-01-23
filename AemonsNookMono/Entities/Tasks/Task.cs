using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Entities.Tasks
{
    public class Task
    {
        #region Constructor
        public Task(Humanoid entity, int updateinterval)
        {
            Random ran = new Random();
            this.UpdateInterval = updateinterval;
            this.UpdateTimer = 1;
            this.Finished = false;
            this.Entity = entity;
        }
        #endregion

        #region Public Properties
        public Humanoid Entity { get; set; }
        public int UpdateInterval { get; set; }
        public int UpdateTimer { get; set; }
        public bool Finished { get; set; }
        public Task CurrentChildTask { get; set; }
        public Stack<Task> ChildrenTasks { get; set; }
        #endregion

        #region Interface
        public void AddChildTask(Task task)
        {
            this.ChildrenTasks.Push(task);
        }
        public virtual void Draw()
        {
            if (this.CurrentChildTask != null)
            {
                this.CurrentChildTask.Draw();
            }
        }
        public virtual void Update()
        {

        }

        public virtual void UpdateChildrenTasks()
        {
            if (this.CurrentChildTask == null && ChildrenTasks.Count > 0)
            {
                CurrentChildTask = ChildrenTasks.Pop();
            }
            if (this.CurrentChildTask != null)
            {
                this.CurrentChildTask.Update();
                if (this.CurrentChildTask.Finished)
                {
                    this.CurrentChildTask = null;
                }
            }
        }

        #endregion
    }
}
