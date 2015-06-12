using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Core.Dto
{
    public class PagerSortDto
    {
        private int _pageindex = 0;
        /// <summary>
        /// 页码
        /// </summary>
        public int pageIndex
        {
            get
            {
                return _pageindex;
            }
            set
            {
                _pageindex = value;
            }
        }

        private int _pagesize = 65535;
        /// <summary>
        /// 分页大小
        /// </summary>
        public int pageSize
        {
            get
            {
                return _pagesize;
            }
            set
            {
                _pagesize = value;
            }
        }


        private string _sortField = "Id";
        /// <summary>
        /// 排序字段
        /// </summary>
        public string sortField
        {
            get
            {
                return _sortField;
            }
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                {
                    _sortField = value;
                }
            }
        }

        private string _sortOrder = "ASC";
        /// <summary>
        /// 排序类型： asc desc
        /// </summary>
        public string sortOrder
        {
            get
            {
                return _sortOrder;
            }
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                {
                    _sortOrder = value;
                }
            }
        }

        /// <summary>
        /// 查询关键词
        /// </summary>
        public string key { get; set; }
    }
}
