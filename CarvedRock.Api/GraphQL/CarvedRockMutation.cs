using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarvedRock.Api.Data.Entities;
using CarvedRock.Api.GraphQL.Messaging;
using CarvedRock.Api.GraphQL.Types;
using CarvedRock.Api.Repositories;
using GraphQL.Types;

namespace CarvedRock.Api.GraphQL
{
    public class CarvedRockMutation : ObjectGraphType
    {
        public CarvedRockMutation(ProductReviewRepository reviewRepository, 
            ReviewMessageService messageService)
        {
            FieldAsync<ProductReviewType>(
                "createReview",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProductReviewInputType>> { Name = "review" }),
                resolve: async context =>
                {
                    var review = context.GetArgument<ProductReview>("review");
                    //return await context.TryAsyncResolve(
                    //    async c => await reviewRepository.AddReview(review));
                    await reviewRepository.AddReview(review);
                    messageService.AddReviewAddedMessage(review); //notify the messagingService of new review
                    return review;
                });
        }
    }
}
