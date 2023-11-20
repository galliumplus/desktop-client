using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalliumPlusApi.CompatibilityHelpers
{
    internal class UserIdMapper
    {
        private static UserIdMapper? current;

        public static UserIdMapper Current
        {
            get
            {
                if (current == null)
                {
                    current = new UserIdMapper();
                }
                return current;
            }
        }

        private Dictionary<string, int> ids;

        private UserIdMapper()
        {
            ids = new Dictionary<string, int>();
        }

        public int GetIdFor(string username)
        {
            lock (ids)
            {
                if (ids.ContainsKey(username))
                {
                    return ids[username];
                }
                else
                {
                    Span<byte> binaryData = new();
                    Base64.DecodeFromUtf8(Encoding.UTF8.GetBytes(username), binaryData, out _, out _);
                    int id = BitConverter.ToInt32(binaryData);
                    ids[username] = id;
                    return id;
                }
            }
        }

        public string FindUsernameOf(int numericId)
        {
            lock (ids)
            {
                return ids.First(kvp => kvp.Value == numericId).Key;
            }
        }
    }
}
