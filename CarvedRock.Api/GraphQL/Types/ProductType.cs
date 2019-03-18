using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarvedRock.Api.Data.Entities;
using GraphQL.Types;

namespace CarvedRock.Api.GraphQL.Types
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType()
        {
            Field(t => t.Id);
            Field(t => t.Name).Description("The name of the product");
            Field(t => t.Description);
        }
    }
}
