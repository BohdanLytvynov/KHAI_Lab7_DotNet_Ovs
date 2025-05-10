using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillianSearcher.DAL.Models
{
    public class Villain : IModelBase<int>
    {
        #region Properties

        public int Id { get; set; }

        public string Surename { get; set; }

        public float Height { get; set; }

        public float HeadWidth { get; set; }

        public float HeadHeight { get; set; }

        public float ArmLength { get; set; }

        public float EyeDistance { get; set; }

        #endregion

        #region Ctor
        public Villain()
        {
            
        }

        public Villain(int id, 
            string surename, 
            float height,
            float headWidth,
            float headHeight,
            float armLenght,
            float eyeDistance)
        {
            Id = id;
            Surename = surename;
            Height = height;
            HeadWidth = headWidth;
            HeadHeight = headHeight;
            ArmLength = armLenght;
            EyeDistance = eyeDistance;
        }

        public void UpdateValues(Villain entity)
        {
            Surename = entity.Surename;
            Height = entity.Height;
            HeadWidth = entity.HeadWidth;
            HeadHeight = entity.HeadHeight;
            ArmLength = entity.ArmLength;
            EyeDistance = entity.EyeDistance;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{Surename} | Height: {Height} \n" +
                $"| Head:( W: {HeadWidth}, H: {HeadHeight} ) \n" +
                $"| ArmLength:{ArmLength} | EyeDistance:{EyeDistance}";
        }

        #endregion

    }
}
