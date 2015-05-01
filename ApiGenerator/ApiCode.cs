using System;
using System.Collections.Generic;

namespace ApiGenerator
{
    partial class Api
    {
        private IList<Parameter> parameters;
        public Api(IList<Parameter> parameters)
        {
            this.parameters = parameters;
        }
    }

    public class Parameter
    {
        /// <summary>
        /// 参数类型
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// 参数名
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 定义
        /// </summary>
        public String Definition { get; set; }

        /// <summary>
        /// 是否必须
        /// </summary>
        public String Required { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public String Comment { get; set; }
    }
}