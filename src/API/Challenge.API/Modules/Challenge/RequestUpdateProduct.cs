namespace Challenge.API.Modules.Challenge
{
    public class RequestUpdateProduct
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public int Status { get; set; }
    }
}
