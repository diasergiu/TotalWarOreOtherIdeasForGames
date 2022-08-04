using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalWarDLA.Models.NonDataModels
{
    public interface IJointModel
    {
        public int IdLeft { get; set; }
        public int IdRight { get; set; }
        public IModel_ GetIdNavigationLeftModel();
        public void SetIdNavigationLeftModel(IModel_ modelLeft);
        public IModel_ GetIdNavigationRightModel();
        public void SetIdNavigationRightModel(IModel_ modelRight);

        public void saveYourself(TotalWarWanaBeContext context);

        //public IJointModel() { }
        //public IJointModel(IModel navigateLeft, IModel navigateRight)
        //{
        //    this.IdNavigationLeftModel = navigateLeft;
        //    this.IdNavigationRightModel = navigateRight;
        //    this.IdLeft = navigateLeft.Id;
        //    this.IdRight = navigateRight.Id;
        //}
    }
}

