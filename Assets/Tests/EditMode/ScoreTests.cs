using NUnit.Framework;

public class ScoreTests
{
    [Test]
    public void AddScore()
    {
        Score.InitializeStatic();
        
        var previousScore = Score.GetScore();
        Score.AddScore(100);
        var newScore = Score.GetScore();
        
        Assert.True(newScore > previousScore);
    }
}
