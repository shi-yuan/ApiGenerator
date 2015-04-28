using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGenerator
{
    partial class Api
    {
        private IList<dynamic> parameters;
        public Api(IList<dynamic> parameters)
        {
            this.parameters = parameters;
        }
    }
}
