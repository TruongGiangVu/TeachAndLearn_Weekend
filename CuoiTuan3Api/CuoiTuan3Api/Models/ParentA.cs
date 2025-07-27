namespace CuoiTuan3Api.Models
{
    public class Parent<T> // generic
    {
        public string RequestId { get; set; }
        // ...
        public T Data { get; set; }
    }
    // --------------------A
    public class ParentA
    {
        public string RequestId { get; set; }
        // ...
        public ChildA Data { get; set; }
    }

    public class ChildA
    {
        public string Id { get; set; }
    }

    // ---------B
    public class ParentB<T>
    {
        public string RequestId { get; set; }
        // ...
        public T Data { get; set; }
    }

    public class ChildB
    {
        public string Id { get; set; }
    }


    // A: req: parentA  childA |  res: parentB childB  : 4
    // B: req: parentA  childA |  res: parentB childB  : 4
    // C: req: parentC  childC |  res: parentC childC  : 4

    // generic parent  req: parent<T>  res: parent<T> : 2
    // req ChildA, req ChildA, ChildATransaction : 3
    // req ChildB, req ChildB : 2
    // req ChildC, req ChildC : 2
}
