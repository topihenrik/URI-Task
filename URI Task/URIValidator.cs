using System.Collections.Specialized;

namespace UriValidation
{
    public class UriValidator
    {
        readonly private String validScheme = "visma-identity";
        readonly private String[] validHosts = { "login", "confirm", "sign" };

        private String path = "";
        private Dictionary<String, String> parameters = new Dictionary<String, String>();

        // If uri is valid returns true
        public bool Validate(String unknown_string)
        {
            try
            {
                // Clear parameters and path for new validation
                parameters.Clear(); 
                path = "";

                Uri uri = new Uri(unknown_string);

                if (uri.Scheme != validScheme) // Schema is not valid
                {
                    Console.WriteLine("Invalid scheme");
                    return false;
                }

                if (!validHosts.Contains(uri.Host)) // Path is not valid
                {
                    Console.WriteLine("Invalid path");
                    return false;
                } else
                {
                    path = uri.Host;
                }

                
                NameValueCollection queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);

                // Check validity of parameters based on path
                if (uri.Host == "login" && queryParams.Count == 1)
                {
                    if (queryParams.AllKeys[0] == "source" && queryParams[0] != null && queryParams[0] != "")
                    {
                        parameters["source"] = queryParams[0];
                    }
                    else
                    {
                        Console.WriteLine("Invalid parameters");
                        return false;
                    }
                }
                else if (uri.Host == "confirm" && queryParams.Count == 2)
                {
                    if (queryParams.AllKeys[0] == "source" && queryParams[0] != null && queryParams[0] != "")
                    {
                        parameters["source"] = queryParams[0];
                    }
                    else
                    {
                        Console.WriteLine("Invalid parameters");
                        return false;
                    }

                    if (queryParams.AllKeys[1] == "paymentnumber" && int.TryParse(queryParams[1], out _))
                    {
                        parameters["paymentnumber"] = queryParams[1];
                    }
                    else
                    {
                        Console.WriteLine("Invalid parameters");
                        return false;
                    }
                }
                else if (uri.Host == "sign" && queryParams.Count == 2)
                {
                    if (queryParams.AllKeys[0] == "source" && queryParams[0] != null && queryParams[0] != "")
                    {
                        parameters["source"] = queryParams[0];
                    }
                    else
                    {
                        Console.WriteLine("Invalid parameters");
                        return false;
                    }

                    if (queryParams.AllKeys[1] == "documentid" && queryParams[1] != null && queryParams[1] != "")
                    {
                        parameters["documentid"] = queryParams[1];
                    }
                    else
                    {
                        Console.WriteLine("Invalid parameters");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid parameters");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return false;
            }
        }

        public String GetPath()
        {
            return path;
        }

        public Dictionary<String, String> GetParameters()
        {
            return parameters;
        }
    }
}