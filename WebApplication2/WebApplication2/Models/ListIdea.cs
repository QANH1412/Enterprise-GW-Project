using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ListIdea
    {

        public List<Idea> ListOrderBy { get; set; }
        public int ListIdeaId { get; set; }
        public string ListIdeaText { get; set; }
        public string ListIdeaFilePath { get; set; }
        public DateTime ListIdeaDateTime { get; set; }
        public string ListIdeaUserId { get; set; }
        public string ListIdeaCategoryId { get; set; }
        public string ListIdeaTopicId { get; set; }
        public int ListIdeaReact1 { get; set; }


    }

   /* public class ListIdeaLatest
    {

        public List<Idea> ListOrderBy { get; set; }
        public int ListIdeaId { get; set; }
        public string ListIdeaText { get; set; }
        public string ListIdeaFilePath { get; set; }
        public DateTime ListIdeaDateTime { get; set; }
        public string ListIdeaUserId { get; set; }
        public string ListIdeaCategoryId { get; set; }
        public string ListIdeaTopicId { get; set; }
        public DateTime ListIdeaLatest { get; set; }


    }*/
}