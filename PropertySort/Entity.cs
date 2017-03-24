namespace PropertySort {
    class Entity {

        [Sort(1)]
        public string LastName { get; set; }

        [Sort(0)]
        public string FirstName { get; set; }

        [Sort(2)]
        public int Age { get; set; }
    }
}
