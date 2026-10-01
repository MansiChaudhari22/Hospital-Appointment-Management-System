// Define the namespace containing application models.
namespace CIE_2.Models
{
   // Define the Post class to represent data received from the API.
   public class Post
   {
       // Store the post ID returned by the API.
       public int Id { get; set; }


       // Store the user ID returned by the API.
       public int UserId { get; set; }


       // Store the title of the post.
       public string Title { get; set; } = string.Empty;


       // Store the body of the post.
       public string Body { get; set; } = string.Empty;
   }
}
