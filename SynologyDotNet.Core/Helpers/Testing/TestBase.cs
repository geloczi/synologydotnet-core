using System;
using System.IO;
using Newtonsoft.Json;

namespace SynologyDotNet.Core.Helpers.Testing
{
    /// <summary>
    /// TestBase
    /// </summary>
    public abstract class TestBase
    {
        /// <summary>
        /// The folder to store test related artifacts.
        /// </summary>
        public static string TestingFolder { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{nameof(SynologyDotNet)}.Testing");

        /// <summary>
        /// Configuration to setup a connection for an integration test.
        /// </summary>
        public static TestClientConfig CoreConfig { get; } = LoadJsonFile("config.core.json", c =>
            {
                // Validation
                if (string.IsNullOrEmpty(c.Server))
                    throw new ArgumentException("Value cannot be null or empty", nameof(c.Server));
                if (string.IsNullOrEmpty(c.Username))
                    throw new ArgumentException("Value cannot be null or empty", nameof(c.Username));
                if (string.IsNullOrEmpty(c.Password))
                    throw new ArgumentException("Value cannot be null or empty", nameof(c.Password));

                // Automatically fix minor things
                c.Server = c.Server.TrimEnd('/'); // Do not use '/' suffix
                if (!c.Server.StartsWith("http"))
                    c.Server = "http://" + c.Server; // Always use protool prefix
            },
            new TestClientConfig());

        /// <summary>
        /// Loads the json file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="validateAction">The validate action.</param>
        /// <param name="saveTemplate">Example configuration values to save on failure.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static T LoadJsonFile<T>(string fileName, Action<T> validateAction, T saveTemplate = null)
            where T : class
        {
            string path = Path.Combine(TestingFolder, fileName);
            try
            {
                var obj = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                validateAction(obj);
                Console.WriteLine($"Loaded {path}");
                return obj;
            }
            catch
            {
                Console.Error.WriteLine($"Failed to load {path}");
                try
                {
                    if (!(saveTemplate is null) && !File.Exists(path))
                    {
                        // Save an example configuration file to the disk
                        File.WriteAllText(path, JsonConvert.SerializeObject(saveTemplate, Formatting.Indented));
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.ToString());
                }
                var msg = $"Please edit {path}";
                Console.Error.WriteLine(msg);
                throw new Exception(msg);
            }
        }
    }
}
