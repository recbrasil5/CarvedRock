﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarvedRock.Web.Models
{
    public class ProductReviewInputModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Review { get; set; }
    }
}
