using System;
using System.Threading.Tasks;
using CarvedRock.Web.Models;
using GraphQL.Client;
using GraphQL.Common.Request;
using GraphQL.Common.Response;

namespace CarvedRock.Web.Clients
{
    public class ProductGraphClient
    {
        private readonly GraphQLClient _client;

        public ProductGraphClient(GraphQLClient client)
        {
            _client = client;
        }

        public async Task<ProductModel> GetProduct(int id)
        {
            var query = new GraphQLRequest
            {
                Query = @" 
                query productQuery($productId: ID!)
                { product(id: $productId) 
                    { id name price rating photoFileName description stock introducedAt 
                      reviews { title review }
                    }
                }",
                Variables = new { productId = id }
            };
            var response = await _client.PostAsync(query);
            return response.GetDataFieldAs<ProductModel>("product");
        }

        public async Task<ProductReviewModel> AddReview(ProductReviewInputModel review)
        {

            var query = new GraphQLRequest
            {
                Query = @" 
                mutation($review: reviewInput!)
                {
                    createReview(review: $review)
                    {
                        id
                    }
                }",
                Variables = new { review }
            };
            var response = await _client.PostAsync(query);
            return response.GetDataFieldAs<ProductReviewModel>("createReview");

        }

        public async Task SubscribeToUpdates()
        {
            //https://www.nuget.org/packages/GraphQL.Client/2.0.0-alpha.3
            var result = await _client.SendSubscribeAsync("subscription { reviewAdded { title productId } }");
            result.OnReceive += Receive;
        }

        private void Receive(GraphQLResponse resp)
        {
            var review = resp.Data["reviewAdded"];
        }
    }
}
