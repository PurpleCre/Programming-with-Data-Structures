/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: add and diplay a customer
        // Expected Result: the customer that was added should be displayed
        Console.WriteLine("Test 1");

        var cs = new CustomerService(4);
        cs.AddNewCustomer();
        cs.ServeCustomer();
        
        // Defect(s) Found: 
        //  1. out of range error in ServeCustomers()... customer must be stored before being removed from queue

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Add a customer to a full queue
        // Expected Result: An error should be displayed
        Console.WriteLine("Test 2");

        cs.AddNewCustomer();
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        cs.AddNewCustomer();

        // Defect(s) Found: 
        //  1. Error encountered after adding 2 more customers than the queue can support... changed evaluater to the inclusive ">=" operator

        Console.WriteLine("=================");

        // Test 3
        // Scenario: Serve customer with an empty queue
        // Expected Result: An error should be displayed
        Console.WriteLine("Test 3");

        cs = new CustomerService(4);
        cs.ServeCustomer();

        // Defect(s) Found:
        //  1. need to add a check for queue length and an error report

        Console.WriteLine("=================");

        // Test 4
        // Scenario: create customer service with a capacity of zero or less
        // Expected Result: capacity defaults to 10
        Console.WriteLine("Test 4");
        
        cs = new CustomerService(0);
        Console.WriteLine(cs._maxSize);
        // Defect(s) Found: none

        Console.WriteLine("=================");

        // Add more Test Cases As Needed Below
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        // Need to check if there are customers in our queue
        if (_queue.Count <= 0) // Test 3 Defect - Need to check queue length
        {
            Console.WriteLine("No Customers in the queue");
        }
        else {
            var customer = _queue[0]; // Test 1 defect: Need to read and save the customer before it is deleted from the queue
            _queue.RemoveAt(0);
            Console.WriteLine(customer);
        }
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}