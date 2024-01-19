using CreateCustomAttributes;

var user = new User() { Age = 45, Name = "John" };
Console.WriteLine("User is under 18: " + MinValueValidator.Validate(user));