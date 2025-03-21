using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add the following priority items and confirm they are added to 
    // the back of the queue: Ezio (3), Desmond (2), Conner (1) 
    // Expected Result: [Ezio (Pri:3), Desmond (Pri:2), Connor (Pri:1)]
    // Defect(s) Found: none
    public void TestPriorityQueue_Enqueue()
    {
        // store expecetd outcomes
        string expectedOutcome = "[Ezio (Pri:3), Desmond (Pri:2), Connor (Pri:1)]";
        
        // initialise priority queue
        var priorityQueue = new PriorityQueue();        
        // Add priority items
        priorityQueue.Enqueue("Ezio", 3);
        priorityQueue.Enqueue("Desmond", 2);
        priorityQueue.Enqueue("Connor", 1);

        Assert.AreEqual(expectedOutcome, priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Add the following priority items and confirm the dequeue method removes
    // and returns the item with the highest priority: Ezio (3), Desmond (2), Conner (1) 
    // Expected Result: Ezio
    // Defect(s) Found: none
    public void TestPriorityQueue_Dequeue()
    {
        // store expecetd outcomes
        string expectedOutcome = "Ezio";
        
        // initialise priority queue
        var priorityQueue = new PriorityQueue();        
        // Add priority items
        priorityQueue.Enqueue("Desmond", 2);
        priorityQueue.Enqueue("Edward", 3);
        priorityQueue.Enqueue("Anno", 3);
        priorityQueue.Enqueue("Connor", 1);
        priorityQueue.Enqueue("Ezio", 3);
        priorityQueue.Enqueue("Bayek", 2);

        // pop out Edward and Anno
        priorityQueue.Dequeue();
        priorityQueue.Dequeue();

        Assert.AreEqual(expectedOutcome, priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: If there are more than one item with the highest priority, 
    // then the item closest to the front of the queue will be removed and its value returned.
    // Expected Result: Anno
    // Defect(s) Found: highest stroed priority index is updating even when the
    //  priority is equal to current stored priority
    // Fix: changed the if check in Dequeue function to if(_queue[index].Priority > _queue[highPriorityIndex].Priority)
    public void TestPriorityQueue_PriorityDequeue()
    {
        // store expecetd outcomes
        string expectedOutcome = "Edward";
        
        // initialise priority queue
        var priorityQueue = new PriorityQueue();        
        // Add priority items
        priorityQueue.Enqueue("Desmond", 2);
        priorityQueue.Enqueue("Edward", 3);
        priorityQueue.Enqueue("Anno", 3);
        priorityQueue.Enqueue("Connor", 1);
        priorityQueue.Enqueue("Ezio", 3);
        priorityQueue.Enqueue("Bayek", 2);

        Assert.AreEqual(expectedOutcome, priorityQueue.Dequeue());
    }
    
    [TestMethod]
    // Scenario: If the queue is empty, then an error exception shall be thrown.
    // Expected Result: an error exception
    // Defect(s) Found: None
    public void TestPriorityQueue_EmptyQueue()
    {
        // initialise priority queue
        var priorityQueue = new PriorityQueue();        

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }

    // Add more test cases as needed below.
}