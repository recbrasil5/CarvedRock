using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarvedRock.Api.Data;
using GraphQL.Types;

namespace CarvedRock.Api.GraphQL.Types
{
    public class ProductTypeEnumType : EnumerationGraphType<ProductTypeEnum>
    {
        public ProductTypeEnumType()
        {
            Name = "Type";
            Description = "The type of product";
        }
    }
}
