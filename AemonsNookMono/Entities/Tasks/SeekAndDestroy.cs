using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Entities.Tasks
{
    public class SeekAndDestroy : Task
    {
        #region Constructor
        public SeekAndDestroy(Entity entity, Entity targetEntity, int updateinterval) : base(entity, updateinterval)
        {
            this.TargetEntity = targetEntity;
        }
        #endregion

        #region Public Properties
        Entity TargetEntity { get; set; }
        #endregion

        #region Interface
        #endregion

    }
}
