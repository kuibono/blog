using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Core.Dto
{
    public interface IDataOrder
    {
        /// <summary>
        /// order field
        /// </summary>
        string By { get; set; }

        /// <summary>
        /// asc or desc
        /// </summary>
        string Order { get; set; }
    }


    //public interface IDataOrder
    //{
    //    /// <summary>
    //    /// order field
    //    /// </summary>
    //    IDataOrderField[] OrderFields { get; }
    //}

    //public interface IDataOrderField
    //{
    //    /// <summary>
    //    /// Field Name
    //    /// </summary>
    //    string Name { get; set; }

    //    /// <summary>
    //    /// Order Direction(ASC Or DESC)
    //    /// </summary>
    //    DataOrderFieldDirection Direction { get; set; }

    //    /// <summary>
    //    /// Priority
    //    /// </summary>
    //    int Priority { get; set; }
    //}

    //public enum DataOrderFieldDirection
    //{
    //    ASC,
    //    DESC
    //}
}
