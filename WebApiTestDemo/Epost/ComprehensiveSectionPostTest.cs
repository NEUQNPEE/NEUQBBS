using WebApiDemo.Entities.EPost;

namespace WebApiTestDemo.EPost;

public class ToComprehensiveSectionPostMapperTests
{
    [Fact]
    public void ToComprehensiveSectionPost_Should_Map_Post_To_ComprehensiveSectionPost()
    {
        // Arrange
        var post = new Post
        {
            Id = 1,
            UserId = 2,
            Content = "Sample content",
            PublishTime = DateTime.Now,
            UpVote = 10,
            DownVote = 2,
            MainPostId = 3,
            IsMainPost = true,
            Type = "Type1",
            Title = "Sample Title",
            ViewNum = 100,
            ReplyNum = 20,
            IsDeleted = false
        };

        // Act
        var result = post.ToComprehensiveSectionPost();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ComprehensiveSectionPost>(result);
        Assert.Equal(post.Id, result.Id);
        Assert.Equal(post.UserId, result.UserId);
        Assert.Equal(post.Content, result.Content);
        Assert.Equal(post.PublishTime, result.PublishTime);
        Assert.Equal(post.UpVote, result.UpVote);
        Assert.Equal(post.DownVote, result.DownVote);
        Assert.Equal(post.MainPostId, result.MainPostId);
        Assert.Equal(post.IsMainPost, result.IsMainPost);
        Assert.Equal(post.Type, result.Type);
        Assert.Equal(post.Title, result.Title);
        Assert.Equal(post.ViewNum, result.ViewNum);
        Assert.Equal(post.ReplyNum, result.ReplyNum);
        Assert.Equal(post.IsDeleted, result.IsDeleted);
    }
}
