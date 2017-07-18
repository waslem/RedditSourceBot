using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedditSharp;
using RedditSourceBot.Models;

namespace RedditSourceBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var reddit = new Reddit();
            var user = reddit.LogIn("waslem", "");

            var subreddit = reddit.GetSubreddit("/r/multicopter");

            subreddit.Subscribe();

            var data = new RedditContext();

            foreach (var post in subreddit.New.Take(25)) // subreddit.New.Take(25);
            {
                Post newPost = new Post{ Title = post.Title, Created = post.Created, Author = post.Author.Name };

                foreach (var comment in post.Comments)
                {
                    newPost.Comments.Add(new Comment { Content = comment.Body, Author = comment.AuthorName, Created = comment.Created });

                    foreach (var childComment in comment.Comments)
                    {
                        newPost.Comments.Add(new Comment { Content = childComment.Body, Author = childComment.AuthorName, Created = childComment.Created });
                    }
                }
                data.Posts.Add(newPost);

                // not sure where this should sit?
                data.SaveChanges();
            }
        }
    }
}
