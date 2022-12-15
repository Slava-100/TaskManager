using FluentAssertions;
using TaskManager;

namespace TaskManager.Tests
{
    public class DataStorageTests
    {
        [TestCaseSource(typeof(DataStorageTestCaseSource), nameof(DataStorageTestCaseSource.AddBoardTestSource))]
        public void AddBoardTest(Dictionary<int, Board> baseBoards, string idAdmin, Dictionary<int, Board> expectedBoards, int expectedNumberBoard)
        {
            //Given
            DataStorage dataStorage = new DataStorage();
            dataStorage.Boards = baseBoards;

            //When
            int actualNumberBoard = dataStorage.AddBoard(idAdmin);
            Dictionary<int, Board> actualBoards = dataStorage.Boards;

            //Then
            Assert.That(actualNumberBoard, Is.EqualTo(expectedNumberBoard));
            actualBoards.Should().BeEquivalentTo(expectedBoards);
        }
    }
}
