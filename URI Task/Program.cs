using UriValidation;

namespace URI_Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Valid Uniform Resource Identifiers
            Console.WriteLine("***** Valid URIs *****");
            testValidator("visma-identity://login?source=severa");
            testValidator("visma-identity://confirm?source=netvisor&paymentnumber=102226");
            testValidator("visma-identity://sign?source=vismasign&documentid=105ab44");

            // Invalid Uniform Resource Identifiers
            Console.WriteLine("***** Invalid URIs *****");
            testValidator("HelloWorld!");
            testValidator("https://www.google.com/");
            testValidator("visx-idtity://login?source=severa");
            testValidator("visma-identity://grass?source=severa");
            testValidator("visma-identity://login");
            testValidator("visma-identity://confirm?source=netvisor&paymentnumber=1xx26");
            testValidator("visma-identity://sign?source=vismasign&documentid=");
        }

        static void testValidator(String uri)
        {
            Console.WriteLine("Uri to be valitated: " + uri);
            UriValidator validator = new UriValidator();
            if (validator.Validate(uri))
            {
                Console.WriteLine("Uri is valid.");
                Console.WriteLine("Path: " + validator.GetPath());
                Console.WriteLine("Parameters:");
                if (validator.GetParameters().Count > 0)
                {
                    foreach (KeyValuePair<String, String> kvp in validator.GetParameters())
                    {
                        Console.WriteLine("\t" + kvp.Key + ": " + kvp.Value);
                    }
                } else
                {
                    Console.WriteLine("None");
                }
                
            } else
            {
                Console.WriteLine("Uri is invalid.");
            }
            Console.WriteLine("");
        }
    }
}