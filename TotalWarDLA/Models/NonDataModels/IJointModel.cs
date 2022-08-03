using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalWarDLA.Models.NonDataModels
{
    public abstract class IJointModel
    {
        //public abstract int IdLeft { get; set; }
        //public abstract int IdRight { get; set; }
        //public IJointModel(){}   
        //public IJointModel(IModel navigateLeft, IModel navigateRight)
        //{
        //    this.IdNavigationLeftModel = navigateLeft;
        //    this.IdNavigationRightModel = navigateRight;
        //    this.IdLeft = navigateLeft.Id;
        //    this.IdRight = navigateRight.Id;
        //}
        //public abstract IModel IdNavigationLeftModel { get; set; }
        //public abstract IModel IdNavigationRightModel { get; set; }

        public abstract void saveYourself(TotalWarWanaBeContext context);       
    }
}
