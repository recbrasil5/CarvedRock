using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using CarvedRock.Api.Data.Entities;

namespace CarvedRock.Api.GraphQL.Messaging
{
    public class ReviewMessageService
    {
        private readonly ISubject<ReviewAddedMessage> _messageStream = new ReplaySubject<ReviewAddedMessage>(1);

        public ReviewAddedMessage AddReviewAddedMessage(ProductReview review)
        {
            var message = new ReviewAddedMessage
            {
                ProductId = review.ProductId,
                Title = review.Title
            };
            _messageStream.OnNext(message); //sends message to all subscribers
            return message;
        }

        public IObservable<ReviewAddedMessage> GetMessages()
        {
            return _messageStream.AsObservable();
        }
    }
}
