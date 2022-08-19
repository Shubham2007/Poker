using Newtonsoft.Json;
using PokerGame.Contracts;

namespace PokerGame.Core.Cloners
{
    class SerializerCloning<TObject> : ICloningStrategy<TObject>
    {
        /// <summary>
        /// Perform a deep Copy of the object, using Json as a serialisation method. NOTE: Private members are not cloned using this method.
        /// </summary>
        /// <typeparam name="TObject">The type of object being copied.</typeparam>
        /// <param name="originalObject">The object instance to copy.</param>
        /// <returns>The deep copied object.</returns>
        public TObject DeepClone(TObject originalObject)
        {
            // Don't serialize a null object, simply return the default for that object
            if (originalObject is null)
                return default;

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            string serializedData = JsonConvert.SerializeObject(originalObject);

            return JsonConvert.DeserializeObject<TObject>(serializedData, deserializeSettings);
        }
    }
}
